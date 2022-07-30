
namespace DockedTransactor;

public class Program
{
    public static async Task Main(string[] args)
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, Docker App!");

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