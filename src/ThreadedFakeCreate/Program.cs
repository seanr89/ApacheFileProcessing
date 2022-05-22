using System.Diagnostics;
using CrossCutters;

namespace ThreadedFakeCreate;

public class Program
{
    async static Task Main(string[] args)
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");

        int recordCount = 10000;
        Console.WriteLine($"Faker Start to create: {recordCount} records");

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        List<Transaction> records = new List<Transaction>();

        int split = 4;
        int count = recordCount / split;

        RunThreadPool(split, count);
    }

    static void RunThreadPool(int split, int count)
    {
        var doneEvents = new ManualResetEvent[split];

        for (int i = 0; i < split; i++)
        {
            //set the done event, initialise the processor and work!
            doneEvents[i] = new ManualResetEvent(false);
            var f = new ThreadedDataGenerator(count, i, doneEvents[i]);
            ThreadPool.QueueUserWorkItem(new WaitCallback(f.ThreadPoolCallback), i);
        }

        //WaitHandler to block until all the work is done
        WaitHandle.WaitAll();
        //return "Job Done";
    }
}