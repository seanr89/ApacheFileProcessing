

using CrossCutters;

namespace TransactThreader;

public class Program
{
    static void Main(string[] args)
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");

        int fileCount = 0;
        Console.WriteLine("Please enter the number of files to create:");
        fileCount = Convert.ToInt32(Console.ReadLine());

        int fileSize = 50000;
        Console.WriteLine("Please enter the number of records to add to a single file:");
        fileSize = Convert.ToInt32(Console.ReadLine());

        // int dateStart = 0;
        // Console.WriteLine("Please enter the number of to navigate back");
        // dateStart = Convert.ToInt32(Console.ReadLine());

        //Go Get Customers
        List<Customer> customers = CustomerGetter.TryGetCustomersOrGenerate().ToList();
        Console.WriteLine($"Using {customers.Count} customers");

        //Go Get Mids
        List<MID> mids = MIDGetter.TryGetMIDsOrGenerate().ToList();
        Console.WriteLine($"Using {mids.Count} MIDs");

        //TODO - time to start the fun stuff
        if(fileCount > 20){
            RecursiveProcess(fileCount, 0, 20, customers);
            return;
        }
        //RunThreadPool(fileCount, customers);

        Console.WriteLine("Job Done");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="loopCount"></param>
    /// <param name="customers"></param>
    static void RunThreadPool(int loopCount, IEnumerable<Customer> customers)
    {
        Console.WriteLine($"RunTheadPool {loopCount}");
        var doneEvents = new ManualResetEvent[loopCount];
        Thread.Sleep(2500);
        //var threadArray = new ThreadedDataGenerator[split];

        for (int i = 0; i < loopCount; ++i)
        {
        //     //set the done event, initialise the processor and work!
            doneEvents[i] = new ManualResetEvent(false);
        //     var f = new ThreadedDataGenerator(count, i, customers, doneEvents[i]);
        //     threadArray[i] = f;
        //     ThreadPool.QueueUserWorkItem(x => {
        //         f.CustomEvent();
        //         tryUpdatePrimaryList(f._transactions);
        //         doneEvents[f._threadNumber].Set();
        //     }, i);
        }
        //WaitHandler to block until all the work is done
        //WaitHandle.WaitAll(doneEvents);
    }

    static void RecursiveProcess(int totalCount, int iteration, int increment, List<Customer> customers)
    {
        Console.WriteLine($"RecursiveProcess");
        var completedCount = iteration * increment;
        //Console.WriteLine($"RecursiveProcess: completedCount {completedCount}");
        var processCount = increment;

        if(completedCount >= totalCount)
            return;

        //TODO: figure out total and remaining!
        if((totalCount - completedCount) < increment)
            processCount = totalCount - completedCount;
        
        //Console.WriteLine($"RecursiveProcess: processCount {processCount}");

        RunThreadPool(processCount, customers);

        //Increment the iteration
        iteration++;

        RecursiveProcess(totalCount, iteration, increment, customers);
    }
}