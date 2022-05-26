using System.Diagnostics;
using CrossCutters;

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
        do{
            var date = DateOnly.FromDateTime(DateTime.Now.AddDays(-counter));
            Console.WriteLine($"Faker Start to create: {recordCount} records for date: {date.ToShortDateString()}");
            //4 appears to be the most!
            ScheduleRecurringJob(recordCount, date);
            counter++;
        }
        while (counter < 5);
      
        stopwatch.Stop();
        Console.WriteLine($"Event completed it : {stopwatch.ElapsedMilliseconds}ms");
    }

    static void ScheduleRecurringJob(int recordCount, DateOnly date)
    {
        Console.WriteLine($"ScheduleRecurringJob");
        int split = 4;
        int count = recordCount / split;
        for(int x = 0; x < 20; ++x)
        {
            //Clear the records away for now
            _records = new List<Transaction>();
            RunThreadPool(split, count);
            FileWriter.WriteFakeTransactionToFile(_records, date);
            _totalCount = _records.Count;
        }
        Console.WriteLine($"Created file with {_totalCount} records");
    }

    static void RunThreadPool(int split, int count)
    {
        //Console.WriteLine($"RunTheadPool on {count} and split: {split}");
        var doneEvents = new ManualResetEvent[split];
        var threadArray = new ThreadedDataGenerator[split];

        for (int i = 0; i < split; ++i)
        {
            //set the done event, initialise the processor and work!
            doneEvents[i] = new ManualResetEvent(false);
            var f = new ThreadedDataGenerator(count, i, doneEvents[i]);
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

    // static void tryReportListCount(){
    //     Console.WriteLine($"Count of records: {_records.Count}");
    // }
}