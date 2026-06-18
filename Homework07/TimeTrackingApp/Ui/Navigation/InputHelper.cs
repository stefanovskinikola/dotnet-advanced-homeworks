using TimeTrackingApp.Ui.Formatting;

namespace TimeTrackingApp.Ui.Navigation;

public static class InputHelper
{
    public static string ReadValidatedString(string prompt, Func<string, string> normalize, Func<string, string?> validate)
    {
        while (true)
        {
            Console.Write(prompt);
            var rawInput = Console.ReadLine() ?? string.Empty;
            var value = normalize(rawInput);
            var errorMessage = validate(value);

            if (errorMessage is null)
            {
                return value;
            }

            ConsoleStyler.WriteError(errorMessage);
        }
    }

    public static int ReadMenuChoice(string prompt, IReadOnlyDictionary<int, string> options)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine(prompt);

            foreach (var option in options.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{option.Key}. {option.Value}");
            }

            Console.Write("Select an option: ");
            var input = Console.ReadLine();

            if (!int.TryParse(input, out var choice))
            {
                ConsoleStyler.WriteError("Only numbers are allowed.");
                continue;
            }

            if (!options.ContainsKey(choice))
            {
                ConsoleStyler.WriteError("Please select one of the listed options.");
                continue;
            }

            return choice;
        }
    }

    public static string ReadRequiredString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            ConsoleStyler.WriteError("Value is required.");
        }
    }

    public static int ReadIntInRange(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (!int.TryParse(input, out var value))
            {
                ConsoleStyler.WriteError("Only numbers are allowed.");
                continue;
            }

            if (value < min || value > max)
            {
                ConsoleStyler.WriteError($"Please enter a number between {min} and {max}.");
                continue;
            }

            return value;
        }
    }

    public static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (y/n): ");
            var input = Console.ReadLine()?.Trim().ToLowerInvariant();

            if (input is "y" or "yes")
            {
                return true;
            }

            if (input is "n" or "no")
            {
                return false;
            }

            ConsoleStyler.WriteError("Please answer with y or n.");
        }
    }
}
