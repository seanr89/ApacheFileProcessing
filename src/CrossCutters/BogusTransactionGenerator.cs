using Bogus;
using CountryData.Bogus;

namespace CrossCutters;

public static class BogusTransactionGenerator
{
    /// <summary>
    /// creates and returns an array of fake transactions
    /// </summary>
    /// <param name="count">count of list items to be returned</param>
    /// <returns></returns>
    public static List<Transaction> GenerateTransactions(int count, List<Customer> customers)
    {
        Random rand = new Random();
        var testTrans = new Faker<Transaction>()
            //Use a method outside scope.
            .RuleFor(t => t.TRANSASCTION_ID, f => Guid.NewGuid())
            .RuleFor(t => t.TOKENISED_CUSTOMER_ID, f => customers.ElementAt(rand.Next(0, customers.Count())).CustomerId)
            .RuleFor(t => t.TRANSCATION_DATE, f => f.Date.Past())
            .RuleFor(t => t.TRANSACTION_NARRATIVE, f => f.Random.String())
            .RuleFor(t => t.TRANSCATION_BILLING_AMOUNT, f => f.Finance.Amount(1, 150, 2))
            .RuleFor(t => t.TRANSACTION_BILLING_CURRENCY_CODE, f => f.Country().CurrencyCode())
            .RuleFor(t => t.MCC, f => f.Random.Number(4900,5600))
            .RuleFor(t => t.MERCHANT_ID, f => f.Random.Number(1001,3999))
            .RuleFor(t => t.TransactionAmount, f => f.Finance.Amount(1, 150, 2))
            .RuleFor(t => t.TaxRate, f => f.Finance.Amount(0, 15, 0));
        return testTrans.Generate(count);
    }
}