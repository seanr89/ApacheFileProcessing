
namespace CrossCutters;

public record Customer
{
    public Guid ExternalCustomerId { get; set; }
    public Guid CustomerId { get; set; }

    // public override string ToString()
    // {
    //     return $"Transaction: {TransactionId} for MCC: {MCC} with Transaction Amount: {TransactionAmount}";
    // }
}