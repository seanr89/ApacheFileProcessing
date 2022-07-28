using System.Diagnostics;
using System.Globalization;
using CrossCutters;
using CsvHelper;
using CsvHelper.Configuration;
using Utils;

namespace ThreadedFakeCreate;

public class Program
{
    private static List<Transaction> _records = new List<Transaction>();
    private static int _totalCount = 0;
    static void Main(string[] args)
    {
        int iterationCount = 25000;
        int counter = 1;
        int maxFileCount = 25;
        var stopwatch = new Stopwatch();

        //TODO here create the customers! - needs a re-work!!!
        List<Customer> customers = CustomerHandler.TryGetCustomersOrGenerate().ToList();
        Console.WriteLine($"Using {customers.Count} customers");

        //Maybe create some MID's too!
        List<MID> mids = MIDHandler.TryGetMIDsOrGenerate().ToList();
        Console.WriteLine($"Using {mids.Count} MIDs");

        Console.WriteLine("How many days back do you want to start!");
        int dayCountBack = Convert.ToInt32(Console.ReadLine());

        var startDate = DateTime.Now.AddDays(-dayCountBack);
        Console.WriteLine($"Date: {startDate.ToShortDateString()}");

        if(ConsoleMethods.Confirm("Is this date correct") == false)
            Environment.Exit(0);
        
        //Check if users wants to wipe down?
        if(ConsoleMethods.Confirm("Do you want to wipe down the existing files"))
            FileWriter.DeleteFiles();

        stopwatch.Start();

        //Looping here to handle multiple date ranges!
        Console.WriteLine($"{maxFileCount} files to generate!");
        do{
            _totalCount = 0;
            var date = DateOnly.FromDateTime(startDate.AddDays(-counter));
            // Console.WriteLine($"Faker Start to create: {iterationCount} records for date: {date.ToShortDateString()}");
            Console.WriteLine($"Faker Start to create records for date: {date.ToShortDateString()}");
            ScheduleRecurringJob(iterationCount, date, customers, mids);
            //Console.WriteLine($"File Done at: {stopwatch.ElapsedMilliseconds}ms");
            counter++;
        }
        while (counter <= maxFileCount); //Simple re-loop process
      
        stopwatch.Stop();
        Console.WriteLine($"Event completed it : {stopwatch.ElapsedMilliseconds}ms");
        Console.WriteLine("App Complete");
    }

    /// <summary>
    /// TODO: Detail Notes for this method
    /// </summary>
    /// <param name="recordCount"></param>
    /// <param name="date"></param>
    /// <param name="customers"></param>
    /// <param name="mids"></param>
    static void ScheduleRecurringJob(int recordCount, DateOnly date, IEnumerable<Customer> customers, IEnumerable<MID> mids)
    {
        //Console.WriteLine($"ScheduleRecurringJob");
        int split = 10;
        int count = recordCount / split;
        //Dont know if I like this tbh!!
        //This is used to chunk across writing a single daily file!
        //each then runs across the 4 part thread pool
        for(int x = 0; x < 20; ++x)
        {
            //Clear the _record list back down after every pass to save memory!
            _records = new List<Transaction>();
            RunThreadPool(split, count, customers);
            FileWriter.WriteFakeTransactionToFile(_records, date);
            _totalCount += _records.Count;
        }
        Console.WriteLine($"Created file with {_totalCount} records");
    }

    static void RunThreadPool(int split, int count, IEnumerable<Customer> customers)
    {
        //Console.WriteLine($"RunTheadPool on {count} and split: {split}");
        var doneEvents = new ManualResetEvent[split];
        var threadArray = new ThreadedDataGenerator[split];

        for (int i = 0; i < split; ++i)
        {
            //set the done event, initialise the processor and work!
            doneEvents[i] = new ManualResetEvent(false);
            var f = new ThreadedDataGenerator(count, i, customers, doneEvents[i]);
            threadArray[i] = f;
            ThreadPool.QueueUserWorkItem(x => {
                f.CustomEvent();
                tryUpdatePrimaryList(f._transactions);
                doneEvents[f._threadNumber].Set();
            }, i);
        }
        //WaitHandler to block until all the work is done
        WaitHandle.WaitAll(doneEvents);
    }

    /// <summary>
    /// Support update work on list
    /// Appears to be happy with async/threaded calls all at once!
    /// </summary>
    /// <param name="litems"></param>
    static void tryUpdatePrimaryList(List<Transaction> litems)
    {
        try{
            //Console.WriteLine($"record {litems[0].ToString()}");
            _records.AddRange(litems); 
        }
        catch
        {
            Console.WriteLine("An exception was caught adding to list");
        }
    }
}