namespace MultFileSearch
{
    public class FolderProcessor
    {
        public FolderProcessor()
        {
            
        }

        public void Execute(string path)
        {
            if(Directory.Exists(path))
            {
                // This path is a directory
                ProcessDirectory(path);
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", path);
            }
        }

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string [] fileEntries = Directory.GetFiles(targetDirectory);
            foreach(string fileName in fileEntries)
            {
                Console.WriteLine("Processing file {0}", fileName);
            }

            // Recurse into subdirectories of this directory.
            // string [] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            // foreach(string subdirectory in subdirectoryEntries)
            //     ProcessDirectory(subdirectory);
        }
    }
}