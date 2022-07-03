using Bogus;

namespace CrossCutters;

public static class BogusMIDGenerator
{

    /// <summary>
    /// creates and returns an array of fake MIDs
    /// </summary>
    /// <param name="count">count of list items to be returned</param>
    /// <returns></returns>
    public static List<MID> Generate(int count = 1000)
    {
        Random rand = new Random();
        var testTrans = new Faker<MID>()
            //Use a method outside scope.
            .RuleFor(t => t.Id, f => Guid.NewGuid())
            .RuleFor(t => t.MCC, f => f.Random.Number(4900,5300));

        return testTrans.Generate(count);
    }
}