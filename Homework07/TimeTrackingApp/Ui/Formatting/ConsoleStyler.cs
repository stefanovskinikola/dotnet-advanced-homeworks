namespace TimeTrackingApp.Ui.Formatting;

public static class ConsoleStyler
{
    public static void WriteSuccess(string message) => WriteColored(message, ConsoleColor.Green);

    public static void WriteError(string message) => WriteColored(message, ConsoleColor.Red);

    public static void WriteInfo(string message) => Console.WriteLine(message);

    public static void WriteHeading(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Console.WriteLine(new string('-', message.Length));
    }

    public static void Pause(string message = "Press Enter to continue...")
    {
        Console.WriteLine();
        Console.Write(message);
        Console.ReadLine();
    }

    private static void WriteColored(string message, ConsoleColor color)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = previousColor;
    }
}
