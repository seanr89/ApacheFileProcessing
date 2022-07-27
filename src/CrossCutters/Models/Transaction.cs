
namespace CrossCutters;

public record Transaction
{
    public Guid TRANSASCTION_ID { get; set; }
    public Guid TOKENISED_CUSTOMER_ID { get; set; }
    public DateTime TRANSCATION_DATE { get; set; }
    public TimeOnly TRANSACTION_TIME {get; set;}
    public string TRANSACTION_NARRATIVE { get; set; }
    public decimal TRANSCATION_BILLING_AMOUNT { get; set; }
    public string TRANSACTION_BILLING_CURRENCY_CODE { get; set; }
    public double TRANSACTION_AMOUNT { get; set; }
    public string TRANSACTION_CURRENCY_CODE { get; set; }
    public string PROCESS_CODE { get; set; }
    /// <summary>
    /// integer based
    /// </summary>
    /// <value></value>
    public int MERCHANT_ID { get; set; }
    public string LOCATION_COUNTRY { get; set; }
    public int MCC { get; set; }
    public string CARDHOLDER_PRESENT { get; set; }
    public string CARD_TYPE_INDICATOR { get; set; }
    public string CARD_INPUT_MODE { get; set; }
    public string REVERSAL_INDICATOR { get; set; }

    // public override string ToString()
    // {
    //     return $"Transaction: {TransactionId} for MCC: {MCC} with Transaction Amount: {TransactionAmount}";
    // }
}