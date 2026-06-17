public sealed class NameGroupingService
{
    public IReadOnlyDictionary<char, List<string>> GroupByStartingLetter(IEnumerable<string> names)
    {
        Dictionary<char, List<string>> groupedNames = new();

        foreach (char letter in Enumerable.Range('A', 26).Select(value => (char)value))
        {
            List<string> matchingNames = names
                .Where(name => StartsWithLetter(name, letter))
                .ToList();

            if (matchingNames.Count > 0)
            {
                groupedNames[letter] = matchingNames;
            }
        }

        return groupedNames;
    }

    private static bool StartsWithLetter(string name, char letter)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        return char.ToUpperInvariant(name.Trim()[0]) == char.ToUpperInvariant(letter);
    }
}