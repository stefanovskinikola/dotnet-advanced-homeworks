public sealed class NameInputService
{
    public IReadOnlyList<string> ReadNamesFromConsole()
    {
        List<string> names = [];

        Console.WriteLine("Enter names to save in names.txt, one name per line.");
        Console.WriteLine("Press Enter on an empty line when you are done.");

        while (true)
        {
            Console.Write("Name: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                break;
            }

            string name = input.Trim();

            if (!string.IsNullOrWhiteSpace(name))
            {
                names.Add(name);
            }
        }

        return names;
    }
}