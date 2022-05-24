

using System.Diagnostics;
using CrossCutters;

namespace FakerFile;

public class Program
{
    public static void Main(string[] args)
    {
        int recordCount = 10000;
        Console.WriteLine($"Faker Start to create: {recordCount} records");

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        List<Transaction> records = new List<Transaction>();

    }
}