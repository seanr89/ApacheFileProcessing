
using System.Globalization;
using System.Runtime.InteropServices;
using CsvHelper;
using CsvHelper.Configuration;

namespace CrossCutters;

public static class FileWriter
{
    //static string filepath = "./Output/";
    private static string windowsFolderPath = "C:/Users/Public/Downloads/ArchiveTrans";
    private static string macFolderPath = "../../../../AppFiles";
    /// <summary>
    /// PUBLIC: file ready to support plan to write a new file or append to an existing file
    /// </summary>
    /// <param name="records"></param>
    /// <param name="date"></param>
    public static void WriteFakeTransactionToFile(List<Transaction> records, DateOnly date)
    {
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
        string path = getFilePath(date);
        Console.WriteLine($"WriteNewFile: {path}");
        using (var streamWriter = new StreamWriter(path))
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    static bool doesFileExist(DateOnly date)
    {
        //Console.WriteLine("doesFileExist");
        //if(newRun) return false;
        return File.Exists(getFilePath(date)) ? true : false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    static string getFilePath(DateOnly date)
    {
        if(System.Runtime.InteropServices.RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows))
        {
            return string.Format(@"{1}/Trans_{0:ddMMMyyyy}.csv", date, windowsFolderPath);
        }
        return string.Format(@"{1}/Trans_{0:ddMMMyyyy}.csv", date, macFolderPath);
        //return string.Format(@"./Output/Trans_{0:ddMMMyyyy}.csv", date);
    }
}