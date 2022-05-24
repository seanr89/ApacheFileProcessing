
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace CrossCutters;

public static class FileWriter
{
    static string filepath = "./Output/fakefile.csv";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="records"></param>
    /// <param name="newRun"></param>
    public static void WriteFakeTransactionToFile(List<Transaction> records, bool newRun = false)
    {
        Console.WriteLine("WriteFakeTransactionToFile");
        if(doesFileExist(newRun))
        {
            AppendToFile(records);
            return;
        }
        WriteNewFile(records);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="records"></param>
    static void WriteNewFile(List<Transaction> records)
    {
        Console.WriteLine("WriteNewFile");
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="records"></param>
    static void AppendToFile(List<Transaction> records)
    {
        Console.WriteLine("AppendToFile");
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // Don't write the header again.
            HasHeaderRecord = false,
        };
        using (var stream = File.Open("./Output/fakefile.csv", FileMode.Append))
        using (var writer = new StreamWriter(stream))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteRecords(records);
        }
    }

    static bool doesFileExist(bool newRun = false)
    {
        if(newRun) return false;
        return File.Exists(filepath) ? true : false;
    }
}