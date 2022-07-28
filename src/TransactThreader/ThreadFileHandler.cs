

namespace TransactThreader;

/// <summary>
/// 
/// </summary>
public class ThreadFileHandler
{
    private ManualResetEvent _doneEvent;

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
        //_transactions.AddRange(BogusTransactionGenerator.GenerateTransactions(_count, _customers));
        //var loopSplit = _count / splitCount;
        //Not sure if this loop is required
        //for(int i = 0; i <= splitCount; ++i)
        //{
        //    _transactions.AddRange(BogusTransactionGenerator.GenerateTransactions(loopSplit, _customers));
        //}
    }
}