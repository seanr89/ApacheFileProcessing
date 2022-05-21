

using FakeFileCreate.Model;

namespace FakeFileCreate;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");

        int recordCount = 100;

        List<Transaction> records = new List<Transaction>();

        for(int i = 0; i < recordCount; i++)
        {
            records.Add(BogusTransactionGenerator.GenerateTransaction());
        }

        Console.WriteLine(records[0].ToString());
    }
}