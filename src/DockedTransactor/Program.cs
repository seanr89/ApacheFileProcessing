
namespace DockedTransactor;

public class Program
{
    public static async Task Main(string[] args)
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, Docker App!");

        // Display the number of command line arguments.
        Console.WriteLine(args.Length);

        string firstValue = args[0];
        Console.WriteLine($"firstValue = {firstValue}");

        //TODO: handle the files being appended
        var counter = 0;
        var max = args.Length != 0 ? Convert.ToInt32(args[0]) : -1;
        while (max == 100 || counter < max)
        {
            Console.WriteLine($"Counter: {++counter}");
            await Task.Delay(1000);
        }
    }
}