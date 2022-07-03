
namespace CrossCutters;

public record MID
{
    public Guid Id { get; set; }
    public int MCC { get; set; }

    public override string ToString()
    {
        return $"MID: {Id} for MCC: {MCC}";
    }
}