using Bogus;
using CountryData.Bogus;

namespace CrossCutters;

public static class BogusTransactionGenerator
{
    private static string[] _processcodes = {"DD", "POS", "Cred POS"};
    private static string?[] _cardholderpresences = {null,"0","1","2","3","4","5","9"};
    private static string[] _cardtypes = {"CREDIT", "DEBIT"};
    private static string[] _cardinputs = {"0","1","2","A","C","N","F","6","B","M","S"};
    private static string[] _reversal = {"Y", "N"};
    /// <summary>
    /// creates and returns an array of fake transactions
    /// </summary>
    /// <param name="count">count of list items to be returned</param>
    /// <returns></returns>
    public static List<Transaction> GenerateTransactions(int count, List<Customer> customers)
    {
        Random rand = new Random();
        CountryDataSet data = new();
        var testTrans = new Faker<Transaction>()
            //Use a method outside scope.
            .RuleFor(t => t.TRANSACTION_ID, f => Guid.NewGuid())
            .RuleFor(t => t.TOKENISED_CUSTOMER_ID, f => customers.ElementAt(rand.Next(0, customers.Count())).CustomerId)
            .RuleFor(t => t.TRANSCATION_DATE, f => f.Date.Past())
            //TIME
            .RuleFor(t => t.TRANSACTION_NARRATIVE, f => f.Lorem.Text())
            .RuleFor(t => t.TRANSACTION_BILLING_AMOUNT, f => f.Finance.Amount(1, 150, 2))
            .RuleFor(t => t.TRANSACTION_BILLING_CURRENCY_CODE, f => f.Country().CountryInfo().CurrencyName)
            .RuleFor(t => t.TRANSACTION_AMOUNT, f => f.Finance.Amount(1, 150, 2))
            .RuleFor(t => t.TRANSACTION_CURRENCY_CODE, f => f.Country().CountryInfo().CurrencyName)
            .RuleFor(t => t.PROCESS_CODE, f => _processcodes.ElementAt(rand.Next(0, _processcodes.Count())))
            .RuleFor(t => t.MERCHANT_ID, f => f.Random.Number(1001,3999))
            .RuleFor(t => t.LOCATION_COUNTRY, f => f.Country().Name())
            .RuleFor(t => t.MCC, f => f.Random.Number(4900,5600))
            .RuleFor(t => t.CARDHOLDER_PRESENT, f => _cardholderpresences.ElementAt(rand.Next(0, _cardholderpresences.Count())))
            .RuleFor(t => t.CARD_TYPE_INDICATOR, f => _cardtypes.ElementAt(rand.Next(0, _cardtypes.Count())))
            .RuleFor(t => t.CARD_INPUT_MODE, f => _cardinputs.ElementAt(rand.Next(0, _cardinputs.Count())))
            .RuleFor(t => t.REVERSAL_INDICATOR, f => _reversal.ElementAt(rand.Next(0, _reversal.Count())));
        return testTrans.Generate(count);
    }
}