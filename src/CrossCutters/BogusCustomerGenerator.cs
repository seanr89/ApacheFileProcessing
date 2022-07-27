using Bogus;

namespace CrossCutters;

public static class BogusCustomerGenerator
{
    private static string[] _genders = {"Male", "Female", "Other"};
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
            .RuleFor(t => t.CustomerId, f => Guid.NewGuid())
            .RuleFor(t => t.Gender, _genders.ElementAt(rand.Next(0, _genders.Count())))
            .RuleFor(t => t.YoB, f => f.Random.Number(1900,2022));

        return testTrans.Generate(10000);
    }
}