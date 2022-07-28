
namespace Utils;

public static class ConsoleMethods
{
    /// <summary>
    /// Provides a Yes/No selection on console apps
    /// </summary>
    /// <param name="title">The text to display on the read message</param>
    /// <returns></returns>
    public static bool Confirm(string title)
    {
        ConsoleKey response;
        do
        {
            Console.Write($"{ title } [y/n] ");
            response = Console.ReadKey(false).Key;
            if (response != ConsoleKey.Enter)
            {
                Console.WriteLine();
            }
        } while (response != ConsoleKey.Y && response != ConsoleKey.N);
        return (response == ConsoleKey.Y);
    }
}