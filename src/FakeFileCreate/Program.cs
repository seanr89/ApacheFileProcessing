

using System.Diagnostics;
using CrossCutters;

namespace FakeFileCreate;

public class Program
{
    public static void Main(string[] args)
    {
        int recordCount = 20000;
        Console.WriteLine($"Faker Start to create: {recordCount} records");

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        List<Transaction> records = new List<Transaction>();

        // This is somehow slower
        // Parallel.For(0, recordCount, index => {
        //     Console.WriteLine($"For loop: {index}");
        //     records.Add(BogusTransactionGenerator.GenerateTransaction());
        // });
        for(int i = 0; i < recordCount; ++i){
            if(i == 10000 || i == 50000 || i == 75000 || i == 100000 || i == 125000)
            {
                Console.WriteLine($"count : {i} at {stopwatch.ElapsedMilliseconds}ms");
            }
            records.Add(BogusTransactionGenerator.GenerateTransaction());
        }
        FileWriter.WriteFakeTransactionToFile(records);

        stopwatch.Stop();
        Console.WriteLine($"Event completed it : {stopwatch.ElapsedMilliseconds}ms");
    }
}