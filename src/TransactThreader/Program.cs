using System.Diagnostics;
using CrossCutters;
using Utils;

namespace TransactThreader;

public class Program
{
    static void Main(string[] args)
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello App!");

        int fileCount = 0;
        Console.WriteLine("Please enter the number of files to create:");
        fileCount = Convert.ToInt32(Console.ReadLine());

        int fileSize = 50000;
        Console.WriteLine("Please enter the number of records to add to a single file:");
        fileSize = Convert.ToInt32(Console.ReadLine());

        int dateStart = 0;
        Console.WriteLine("Please enter count of days to start back from");
        dateStart = Convert.ToInt32(Console.ReadLine());
        var date = DateTime.Now.AddDays(-dateStart);

        //Check if users wants to wipe down?
        if(ConsoleMethods.Confirm("Do you want to wipe down the existing files"))
            FileWriter.DeleteFiles();

        //Go Get Customers
        List<Customer> customers = CustomerGetter.TryGetCustomersOrGenerate().ToList();
        //Console.WriteLine($"Using {customers.Count} customers");

        //Go Get Mids
        //List<MID> mids = MIDGetter.TryGetMIDsOrGenerate().ToList();
        //Console.WriteLine($"Using {mids.Count} MIDs");

        var stop = new Stopwatch();
        stop.Start();
        RecursiveProcess(fileCount, 0, 5, customers, date, fileSize);
        stop.Stop();
        Console.WriteLine($"Event completed it : {stop.ElapsedMilliseconds}ms");
        Console.WriteLine("App Complete");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="loopCount"></param>
    /// <param name="customers"></param>
    static void RunThreadPool(int loopCount, IEnumerable<Customer> customers, DateTime date, int fileSize)
    {
        Console.WriteLine($"RunTheadPool Count: {loopCount}");
        var doneEvents = new ManualResetEvent[loopCount];
        var threadArray = new ThreadFileHandler[loopCount];

        for (int i = 0; i < loopCount; ++i)
        {
            //set the done event, initialise the processor and work!
            doneEvents[i] = new ManualResetEvent(false);
            var f = new ThreadFileHandler(i, customers, doneEvents[i]);
            threadArray[i] = f;
            var runDate = DateOnly.FromDateTime(date.AddDays(-i));
            ThreadPool.QueueUserWorkItem(x => {
                f.CustomEvent(fileSize, runDate);
                //tryUpdatePrimaryList(f._transactions);
                doneEvents[f._threadNumber].Set();
                //Console.WriteLine($"Thread Complete: {f._threadNumber}");
            }, i);
        }
        //WaitHandler to block until all the work is done
        WaitHandle.WaitAll(doneEvents);
    }

    static void RecursiveProcess(int totalCount, int iteration, int increment, List<Customer> customers, DateTime date, int fileSize)
    {
        Console.WriteLine($"RecursiveProcess");
        var completedCount = iteration * increment;
        Console.WriteLine($"RecursiveProcess: completedCount {completedCount}");
        var processCount = increment;

        if(completedCount >= totalCount)
            return;

        //TODO: figure out total and remaining!
        if((totalCount - completedCount) < increment)
            processCount = totalCount - completedCount;
        
        //Console.WriteLine($"RecursiveProcess: processCount {processCount}");

        RunThreadPool(processCount, customers, date, fileSize);

        //Increment the iteration
        iteration++;

        date = date.AddDays(-((iteration + 1) * processCount));
        Console.WriteLine($"RecursiveProcess: Next date: {date.ToShortDateString()}");

        RecursiveProcess(totalCount, iteration, increment, customers, date, fileSize);
        return;
    }
}