
using System.Globalization;
using System.Runtime.InteropServices;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CrossCutters;

public static class FileWriter
{
    //static string filepath = "./Output/";
    private static string windowsFolderPath = "C:/Users/Public/Downloads/ArchiveTrans";
    private static string macFolderPath = "./AppFiles";
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

    public static void DeleteFiles()
    {
        Console.WriteLine("Deleting Files");
        var path = System.Runtime.InteropServices.RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows) ? windowsFolderPath: macFolderPath;
        DirectoryInfo dir = new DirectoryInfo(path);
        foreach(FileInfo fi in dir.GetFiles())
        {
            fi.Delete();
        }
    }

    /// <summary>
    /// Create a new file and write to it!
    /// </summary>
    /// <param name="records"></param>
    static void WriteNewFile(List<Transaction> records, DateOnly date)
    {
        try
        {
            string path = getFilePath(date);
            //Console.WriteLine($"WriteNewFile: {path}");
            using (var streamWriter = new StreamWriter(path))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true, Delimiter = "|" };
                using (var csvWriter = new CsvWriter(streamWriter, csvConfig))
                {
                    var dateFormatOptions = new TypeConverterOptions { Formats = new[] { "yyyy-MM-dd" } };
                    csvWriter.Context.TypeConverterOptionsCache.AddOptions<DateTime>(dateFormatOptions);
                    csvWriter.WriteRecords(records);
                    streamWriter.Flush();
                }
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"WriteNewFile: with exception {e.Message}");
            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="records"></param>
    static void AppendToFile(List<Transaction> records, DateOnly date)
    {
        //Console.WriteLine("AppendToFile");
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false, Delimiter = "|" };
        using (var stream = File.Open(getFilePath(date), FileMode.Append))
        using (var writer = new StreamWriter(stream))
        using (var csv = new CsvWriter(writer, config))
        {
            var dateFormatOptions = new TypeConverterOptions { Formats = new[] { "yyyy-MM-dd" } };
            csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(dateFormatOptions);
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