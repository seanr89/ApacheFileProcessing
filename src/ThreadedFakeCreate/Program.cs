using System.Diagnostics;
using CrossCutters;

namespace ThreadedFakeCreate;

public class Program
{
    private static List<Transaction> _records = new List<Transaction>();
    async static Task Main(string[] args)
    {
        // See https://aka.ms/new-console-template for more information
        //Console.WriteLine("Hello, World!");

        int recordCount = 50000;
        
        Console.WriteLine($"Faker Start to create: {recordCount} records");

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        //4 appears to be the most!
        await ScheduleRecurringJob(recordCount);
      
        stopwatch.Stop();
        Console.WriteLine($"Event completed it : {stopwatch.ElapsedMilliseconds}ms");
    }

    static async Task ScheduleRecurringJob(int recordCount)
    {
        Console.WriteLine($"ScheduleRecurringJob");
        int split = 4;
        int count = recordCount / split;
        for(int x = 0; x < 10; ++x)
        {
            _records = new List<Transaction>();
            await RunThreadPool(split, count);
            tryReportListCount();
            FileWriter.WriteFakeTransactionToFile(_records);
        }
    }

    static async Task RunThreadPool(int split, int count)
    {
        Console.WriteLine($"RunTheadPool on {count} and split: {split}");
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
        //https://stackoverflow.com/questions/9930007/how-to-call-a-completion-method-everytime-threadpool-queueuserworkitem-method-is
        //https://docs.microsoft.com/en-us/dotnet/api/system.threading.threadpool.queueuserworkitem?view=net-6.0

        //WaitHandler to block until all the work is done
        WaitHandle.WaitAll(doneEvents);
        //return "Job Done";
    }

    static void tryUpdatePrimaryList(List<Transaction> litems)
    {
        //Console.WriteLine("tryUpdatePrimaryList");
        try{ 
            _records.AddRange(litems); 
        }
        catch
        {
            Console.WriteLine("An exception was caught adding to list");
        }
    }

    static void tryReportListCount(){
        Console.WriteLine($"Count of records: {_records.Count}");
    }
}