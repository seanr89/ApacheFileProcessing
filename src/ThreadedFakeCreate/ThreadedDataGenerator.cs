
using CrossCutters;
namespace ThreadedFakeCreate;

public class ThreadedDataGenerator
{
    public List<Transaction> _transactions { get; set; }
    private ManualResetEvent _doneEvent;
    private readonly int _count;
    private readonly int _threadNumber;
    public ThreadedDataGenerator(int count, int threadNumber, ManualResetEvent doneEvent)
    {
        _transactions = new List<Transaction>();
        _doneEvent = doneEvent;
        _count = count;
        _threadNumber = threadNumber;
    }

    /// <summary>
    /// ThreadPool queue event to be executed
    /// </summary>
    /// <param name="threadContext">Incoming threadContext and its parameters</param>
    public async void ThreadPoolCallback(Object threadContext)
    {
        //Request that the processor is executed and update the doneEvent once complete
        this.Execute();
        Console.WriteLine("Thread {0} complete", _threadNumber);
        _doneEvent.Set();
    }

    /// <summary>
    /// Method to handle the process execution events!
    /// i.e. provide a delay and read through the list of email records
    /// </summary>
    private void Execute()
    {
        Console.WriteLine("Execute - Thread {0} started", _threadNumber);

        for(int i = 0; i < _count; i++)
        {
            _transactions.Add(BogusTransactionGenerator.GenerateTransaction());
        }
    }
}