
namespace FakeFileCreate.Model;

public record Transaction
{
    public Guid TransactionId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime TransactionDate { get; set; }
    public int MCC { get; set; }
    public int MerchantId { get; set; }
    public decimal TransactionAmount { get; set; }
    public decimal TaxRate { get; set; }

    // public override string ToString()
    // {
    //     return $"Transaction: {TransactionId} for MCC: {MCC} with Transaction Amount: {TransactionAmount}";
    // }
}