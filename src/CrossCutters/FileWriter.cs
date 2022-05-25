
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace CrossCutters;

public static class FileWriter
{
    static string filepath = "./Output/";
    /// <summary>
    /// PUBLIC: file ready to support plan to write a new file or append to an existing file
    /// </summary>
    /// <param name="records"></param>
    /// <param name="date"></param>
    public static void WriteFakeTransactionToFile(List<Transaction> records, DateOnly date)
    {
        //Console.WriteLine("WriteFakeTransactionToFile");
        if(doesFileExist(date))
        {
            AppendToFile(records, date);
            return;
        }
        WriteNewFile(records, date);
    }

    /// <summary>
    /// Create a new file and write to it!
    /// </summary>
    /// <param name="records"></param>
    static void WriteNewFile(List<Transaction> records, DateOnly date)
    {
        //Console.WriteLine("WriteNewFile");
        using (var streamWriter = new StreamWriter(getFilePath(date)))
        {
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(records);
                streamWriter.Flush();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="records"></param>
    static void AppendToFile(List<Transaction> records, DateOnly date)
    {
        //Console.WriteLine("AppendToFile");
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // Don't write the header again.
            HasHeaderRecord = false,
        };
        using (var stream = File.Open(getFilePath(date), FileMode.Append))
        using (var writer = new StreamWriter(stream))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteRecords(records);
        }
    }

    static bool doesFileExist(DateOnly date)
    {
        //if(newRun) return false;
        return File.Exists(getFilePath(date)) ? true : false;
    }

    static string getFilePath(DateOnly date)
    {
        return string.Format(@"./Output/Trans_{0:ddMMMyyyy}.csv", date);
    }
}