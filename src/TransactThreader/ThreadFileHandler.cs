using CrossCutters;

namespace TransactThreader;

/// <summary>
/// 
/// </summary>
public class ThreadFileHandler
{
    //public List<Transaction> _transactions { get; set; }
    private ManualResetEvent _doneEvent;
    public readonly int _threadNumber;
    private IEnumerable<Customer> _customers {get;set;}

    /// <summary>
    /// Custom event that needs to be executed!
    /// </summary>
    public ThreadFileHandler(int threadNumber, IEnumerable<Customer> customers, ManualResetEvent doneEvent)
    {
        _customers = customers;
        //_transactions = new List<Transaction>();
        this._threadNumber = threadNumber;
        this._doneEvent = doneEvent;
    }

    public void CustomEvent(int count, DateOnly date)
    {
        Console.WriteLine($"Execute Running Date {date.ToShortDateString()}");
        this.Execute(count, date);
        //this.WriteTransactionsToFile(date);
        _doneEvent.Set();
        return;
    }

    /// <summary>
    /// Method to handle the process execution events!
    /// i.e. provide a delay and read through the list of email records
    /// </summary>
    private void Execute(int count, DateOnly date)
    {
        List<Transaction> transactions;
        for(int i = 0; i < 100; i++)
        {
            transactions = new List<Transaction>();
            transactions.AddRange(BogusTransactionGenerator.GenerateTransactions((count / 100), _customers));
            WriteTransactionsToFile(transactions, date);
        }
    }

    void WriteTransactionsToFile(List<Transaction> transactions, DateOnly date)
    {
        //Console.WriteLine($"Writing {transactions.Count()} records to file");
        FileWriter.WriteFakeTransactionToFile(transactions, date);
    }
}