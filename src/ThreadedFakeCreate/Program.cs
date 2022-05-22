using System.Diagnostics;

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

        //TODO: figure out a way to split this up!
        //Simple 4 way split now!
        for(int i = 0; i < 4; ++i)
        {
            Console.WriteLine($"Iteration: {i}");
            ThreadPool.QueueUserWorkItem(Job);
            Thread.Sleep(200);
        }

        static void Job(object state){
            for (int x = 0; x < 2500; x++)
            {
                // Console.WriteLine("cycle {0}, is processing by thread {1}",
                // i, Thread.CurrentThread.ManagedThreadId);
                // Thread.Sleep(350);
                //listData.Add(BogusTransactionGenerator.GenerateTransaction());
            }
        }
    }
}