

using System.Globalization;
using CrossCutters;
using CsvHelper;

namespace ThreadedFakeCreate;

public static class CustomerHandler
{
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