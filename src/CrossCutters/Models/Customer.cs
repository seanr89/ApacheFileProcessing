
namespace CrossCutters;

public record Customer
{
    public Guid CustomerId { get; set; }
    public Guid ExternalCustomerId { get; set; }

    public override string ToString()
    {
        return $"Customer: {CustomerId} for MCC: {ExternalCustomerId}";
    }
}