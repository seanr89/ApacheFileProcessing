using System.Globalization;
using CrossCutters;
using CsvHelper;

namespace MultFileSearch
{
    public class FolderProcessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isWindows"></param>
        /// <param name="customerId"></param>
        public void Execute(string path, bool isWindows, string customerId)
        {
            if(Directory.Exists(path))
            {
                // This path is a directory
                ProcessDirectory(path, customerId);
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", path);
            }
        }

        /// <summary>
        /// Process all files in the directory passed in, recurse on any directories
        /// that are found, and process the files they contain.
        /// </summary>
        /// <param name="targetDirectory"></param>
        /// <param name="customerId"></param>
        public void ProcessDirectory(string targetDirectory, string customerId)
        {
            // Process the list of files found in the directory.
            string [] fileEntries = Directory.GetFiles(targetDirectory);
            foreach(string fileName in fileEntries)
            {
                Console.WriteLine("Processing file {0}", fileName);
                if(fileName.EndsWith(".csv") == false)
                    continue;
                
                using (var reader = new StreamReader(fileName))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<Transaction>().Where(c => c.CustomerId.ToString() == customerId);

                    if(records.Any())
                        Console.WriteLine($"Found a total of {records.ToList().Count}");
                }
            }

            // Recurse into subdirectories of this directory.
            // string [] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            // foreach(string subdirectory in subdirectoryEntries)
            //     ProcessDirectory(subdirectory);
        }
    }
}