

using System.Globalization;
using CrossCutters;
using CsvHelper;

namespace TransactThreader;

/// <summary>
/// TODO: Simple step to go and create an array of example customers!
/// </summary>
public static class MIDGetter
{
    /// <summary>
    /// Search for existing customer list and return array or create an upload new list
    /// </summary>
    /// <returns></returns>
    public static List<MID> TryGetMIDsOrGenerate()
    {
        if(File.Exists("./Output/mids.csv")){
            using (var reader = new StreamReader("./Output/mids.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<MID>().ToList();
                return records;
            }
        }
        else{
            var records = BogusMIDGenerator.Generate();
            using (var streamWriter = new StreamWriter("./Output/mids.csv"))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(records);
                    streamWriter.Flush();
                }
            }
            return records;
        }
    }
}