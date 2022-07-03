using System.Diagnostics;
using System.Globalization;
using CrossCutters;
using CsvHelper;
using CsvHelper.Configuration;

namespace ThreadedFakeCreate;

public class Program
{
    private static List<Transaction> _records = new List<Transaction>();
    private static int _totalCount = 0;
    async static Task Main(string[] args)
    {
        int recordCount = 10000;
        int counter = 1;
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        //TODO here create the customers! - needs a re-work!!!
        List<Customer> customers = CustomerHandler.TryGetCustomersOrGenerate().ToList();

        //Maybe create some MID's too!

        //Looping here to handle multiple date ranges!
        do{
            _totalCount = 0;
            var date = DateOnly.FromDateTime(DateTime.Now.AddDays(-counter));
            // Console.WriteLine($"Faker Start to create: {recordCount} records for date: {date.ToShortDateString()}");
            Console.WriteLine($"Faker Start to create records for date: {date.ToShortDateString()}");
            ScheduleRecurringJob(recordCount, date, customers);
            counter++;
        }
        while (counter <= 5); //Simple re-loop process
      
        stopwatch.Stop();
        Console.WriteLine($"Event completed it : {stopwatch.ElapsedMilliseconds}ms");
    }

    /// <summary>
    /// TODO: Detail Notes
    /// </summary>
    /// <param name="recordCount"></param>
    /// <param name="date"></param>
    /// <param name="customers"></param>
    static void ScheduleRecurringJob(int recordCount, DateOnly date, IEnumerable<Customer> customers)
    {
        Console.WriteLine($"ScheduleRecurringJob");
        int split = 4;
        int count = recordCount / split;
        Console.WriteLine($"Count = {count}");
        //What is this loop for??
        for(int x = 0; x < 10; ++x)
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
            _records.AddRange(litems); 
        }
        catch
        {
            Console.WriteLine("An exception was caught adding to list");
        }
    }
}