using Bogus;

namespace CrossCutters;

public static class BogusTransactionGenerator
{
    /// <summary>
    /// Generate a single fake transaction
    /// </summary>
    /// <returns></returns>
    public static Transaction GenerateTransaction()
    {
        //Set the randomizer seed if you wish to generate repeatable data sets.
        var testTrans = new Faker<Transaction>()
            .RuleFor(t => t.TransactionId, f => Guid.NewGuid())
            .RuleFor(t => t.CustomerId, f => Guid.NewGuid())
            .RuleFor(t => t.TransactionDate, f => f.Date.Past())
            .RuleFor(t => t.MCC, f => f.Random.Number(4900,5600))
            .RuleFor(t => t.MerchantId, f => f.Random.Number(1,2999))
            .RuleFor(t => t.TransactionAmount, f => f.Finance.Amount(1, 150, 2))
            .RuleFor(t => t.TaxRate, f => f.Finance.Amount(0, 15, 0));

        //var rec = new Transaction();
        return testTrans.Generate();
    }

    /// <summary>
    /// creates and returns an array of fake transactions
    /// </summary>
    /// <param name="count">count of list items to be returned</param>
    /// <returns></returns>
    public static List<Transaction> GenerateTransactions(int count)
    {
        var testTrans = new Faker<Transaction>()
            //Use a method outside scope.
            .RuleFor(t => t.TransactionId, f => Guid.NewGuid())
            .RuleFor(t => t.CustomerId, f => Guid.NewGuid())
            .RuleFor(t => t.TransactionDate, f => f.Date.Past())
            .RuleFor(t => t.MCC, f => f.Random.Number(4900,5600))
            .RuleFor(t => t.MerchantId, f => f.Random.Number(1,2999))
            .RuleFor(t => t.TransactionAmount, f => f.Finance.Amount(1, 150, 2))
            .RuleFor(t => t.TaxRate, f => f.Finance.Amount(0, 15, 0));

        return testTrans.Generate(count);
    }
}