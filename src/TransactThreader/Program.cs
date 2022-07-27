

using CrossCutters;

namespace TransactThreader;

public class Program
{
    static void Main(string[] args)
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");

        int fileCount = 0;
        Console.WriteLine("Please enter the number of files to create:");
        fileCount = Convert.ToInt32(Console.ReadLine());

        int fileSize = 5000;
        Console.WriteLine("Please enter the number of records to add to a single file:");
        fileSize = Convert.ToInt32(Console.ReadLine());

        int dateStart = 0;
        Console.WriteLine("Please enter the number of to navigate back");
        dateStart = Convert.ToInt32(Console.ReadLine());

        //TODO here create the customers! - needs a re-work!!!
        List<Customer> customers = CustomerGetter.TryGetCustomersOrGenerate().ToList();
        Console.WriteLine($"Using {customers.Count} customers");

        //Maybe create some MID's too!
        List<MID> mids = MIDGetter.TryGetMIDsOrGenerate().ToList();
        Console.WriteLine($"Using {mids.Count} MIDs");

        //TODO - time to start the fun stuff
        if(fileCount > 60){
            //run will need to be split!
        }
    }

    static void RunThreadPool(IEnumerable<Customer> customers)
    {
        //Console.WriteLine($"RunTheadPool on {count} and split: {split}");
        // var doneEvents = new ManualResetEvent[split];
        // var threadArray = new ThreadedDataGenerator[split];

        // for (int i = 0; i < split; ++i)
        // {
        //     //set the done event, initialise the processor and work!
        //     doneEvents[i] = new ManualResetEvent(false);
        //     var f = new ThreadedDataGenerator(count, i, customers, doneEvents[i]);
        //     threadArray[i] = f;
        //     ThreadPool.QueueUserWorkItem(x => {
        //         f.CustomEvent();
        //         tryUpdatePrimaryList(f._transactions);
        //         doneEvents[f._threadNumber].Set();
        //     }, i);
        // }
        //WaitHandler to block until all the work is done
        WaitHandle.WaitAll(doneEvents);
    }
}