
using System.Globalization;
using CsvHelper;
using CrossCutters;

namespace FakeFileCreate;

public static class FileWriter
{
    public static void WriteFakeTransactionToFile(List<Transaction> records)
    {
        Console.WriteLine("WriteFakeTransactionToFile");
        using (var streamWriter = new StreamWriter("./Output/fakefile.csv"))
        {
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(records);
                streamWriter.Flush();
                //result = memoryStream.ToArray();
            }
        }
    }
}