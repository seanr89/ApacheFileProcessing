using CrossCutters;

namespace TransactThreader;

/// <summary>
/// 
/// </summary>
public class ThreadFileHandler
{
    private ManualResetEvent _doneEvent;
    public readonly int _threadNumber;

    /// <summary>
    /// Custom event that needs to be executed!
    /// </summary>
    public ThreadFileHandler(int threadNumber, IEnumerable<Customer> customers, ManualResetEvent doneEvent)
    {
        this._threadNumber = threadNumber;
        this._doneEvent = doneEvent;
        //this.Execute();
    }

    public void CustomEvent()
    {
        this.Execute();
        _doneEvent.Set();
    }

    /// <summary>
    /// Method to handle the process execution events!
    /// i.e. provide a delay and read through the list of email records
    /// </summary>
    private void Execute()
    {
        //Console.WriteLine("ThreadFileHandler:Execute");
        Thread.Sleep(1500);
        //_transactions.AddRange(BogusTransactionGenerator.GenerateTransactions(_count, _customers));
        //var loopSplit = _count / splitCount;
        //Not sure if this loop is required
        //for(int i = 0; i <= splitCount; ++i)
        //{
        //    _transactions.AddRange(BogusTransactionGenerator.GenerateTransactions(loopSplit, _customers));
        //}
        
        _doneEvent.Set();
    }
}