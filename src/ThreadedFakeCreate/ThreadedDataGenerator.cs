
using CrossCutters;
namespace ThreadedFakeCreate;

public class ThreadedDataGenerator
{
    public List<Transaction> _transactions { get; set; }
    private ManualResetEvent _doneEvent;
    private readonly int _count;
    public readonly int _threadNumber;
    private int splitCount = 10;
    private List<Customer> _customers {get;set;}
    public ThreadedDataGenerator(int count, int threadNumber, IEnumerable<Customer> customers, ManualResetEvent doneEvent)
    {
        _transactions = new List<Transaction>();
        _doneEvent = doneEvent;
        _count = count;
        _threadNumber = threadNumber;
        _customers = customers.ToList();
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
        _transactions.AddRange(BogusTransactionGenerator.GenerateTransactions(_count, _customers));
        //var loopSplit = _count / splitCount;
        //Not sure if this loop is required
        //for(int i = 0; i <= splitCount; ++i)
        //{
        //    _transactions.AddRange(BogusTransactionGenerator.GenerateTransactions(loopSplit, _customers));
        //}
    }
}