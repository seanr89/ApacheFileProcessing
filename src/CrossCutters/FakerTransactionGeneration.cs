
namespace CrossCutters;

public static class FakerTransactionGeneration
{
    public static Transaction GenerateTransaction()
    {
        //Set the randomizer seed if you wish to generate repeatable data sets.
        var rec = new Transaction();
        // rec.TransactionId = new Guid();
        // rec.CustomerId = new Guid();
        // rec.TransactionDate = Faker.Identification.DateOfBirth();
        return rec;
    }
}