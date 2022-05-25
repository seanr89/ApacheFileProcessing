
using CrossCutters;
namespace ThreadedFakeCreate;

public class ThreadedDataGenerator
{
    public List<Transaction> _transactions { get; set; }
    private ManualResetEvent _doneEvent;
    private readonly int _count;
    public readonly int _threadNumber;
    private int splitCount = 100;
    public ThreadedDataGenerator(int count, int threadNumber, ManualResetEvent doneEvent)
    {
        _transactions = new List<Transaction>();
        _doneEvent = doneEvent;
        _count = count;
        _threadNumber = threadNumber;
    }

    /// <summary>
    /// Custom event that needs to be executed!
    /// </summary>
    public void CustomEvent()
    {
        this.Execute();
    }

    /// <summary>
    /// Method to handle the process execution events!
    /// i.e. provide a delay and read through the list of email records
    /// </summary>
    private void Execute()
    {
        var loopSplit = _count / splitCount;

        for(int i = 0; i < splitCount; ++i)
        {
            _transactions.AddRange(BogusTransactionGenerator.GenerateTransactions(loopSplit));
        }
    }
}