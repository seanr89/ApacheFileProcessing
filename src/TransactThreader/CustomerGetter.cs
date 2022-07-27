

using System.Globalization;
using CrossCutters;
using CsvHelper;

namespace TransactThreader;

/// <summary>
/// Simple step to go and create an array of example customers!
/// </summary>
public static class CustomerGetter
{
    /// <summary>
    /// Search for existing customer list and return array or create an upload new list
    /// </summary>
    /// <returns></returns>
    public static List<Customer> TryGetCustomersOrGenerate()
    {
        if(File.Exists("./Output/customers.csv")){
            using (var reader = new StreamReader("./Output/customers.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Customer>().ToList();
                return records;
            }
        }
        else{
            var records = BogusCustomerGenerator.Generate();
            using (var streamWriter = new StreamWriter("./Output/customers.csv"))
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