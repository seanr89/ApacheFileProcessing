using Bogus;

namespace CrossCutters;

public static class BogusCustomerGenerator
{

    /// <summary>
    /// creates and returns an array of fake customers
    /// </summary>
    /// <param name="count">count of list items to be returned</param>
    /// <returns></returns>
    public static List<Customer> Generate()
    {
        Random rand = new Random();
        var testTrans = new Faker<Customer>()
            //Use a method outside scope.
            .RuleFor(t => t.ExternalCustomerId, f => Guid.NewGuid())
            .RuleFor(t => t.CustomerId, f => Guid.NewGuid());

        return testTrans.Generate(10000);
    }
}