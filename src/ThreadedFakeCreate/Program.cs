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

        await RunThreadPool(split, count);
    }

    static async Task RunThreadPool(int split, int count)
    {
        var doneEvents = new ManualResetEvent[split];
        var threadArray = new ThreadedDataGenerator[split];

        for (int i = 0; i < split; i++)
        {
            //set the done event, initialise the processor and work!
            doneEvents[i] = new ManualResetEvent(false);
            var f = new ThreadedDataGenerator(count, i, doneEvents[i]);
            threadArray[i] = f;
            ThreadPool.QueueUserWorkItem(x => {
                f.CustomEvent();
                Console.WriteLine("Job Done Now!");
            }, i);
        }
        //https://stackoverflow.com/questions/9930007/how-to-call-a-completion-method-everytime-threadpool-queueuserworkitem-method-is
        //https://docs.microsoft.com/en-us/dotnet/api/system.threading.threadpool.queueuserworkitem?view=net-6.0


        //WaitHandler to block until all the work is done
        WaitHandle.WaitAll(doneEvents);
        //return "Job Done";
    }
}