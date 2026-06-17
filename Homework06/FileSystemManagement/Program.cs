FileStorageService fileStorageService = new();
NameInputService nameInputService = new();
NameGroupingService nameGroupingService = new();

fileStorageService.EnsureStorageExists();

IReadOnlyList<string> existingNames = fileStorageService.ReadAllNames();
PrintNames("Current content of names.txt", existingNames);

IReadOnlyList<string> newNames = nameInputService.ReadNamesFromConsole();

if (newNames.Count > 0)
{
    fileStorageService.AppendNames(newNames);
    Console.WriteLine();
    Console.WriteLine($"Saved {newNames.Count} name(s) to names.txt.");
}
else
{
    Console.WriteLine();
    Console.WriteLine("No new names were entered.");
}

Console.WriteLine();

IReadOnlyList<string> allNames = fileStorageService.ReadAllNames();
PrintNames("Updated content of names.txt", allNames);

IReadOnlyDictionary<char, List<string>> groupedNames = nameGroupingService.GroupByStartingLetter(allNames);

if (groupedNames.Count == 0)
{
    Console.WriteLine("No names starting with letters A-Z were found.");
    return;
}

Console.WriteLine("Generated letter files:");

foreach (KeyValuePair<char, List<string>> group in groupedNames.OrderBy(group => group.Key))
{
    int addedNamesCount = fileStorageService.MergeNamesForLetter(group.Key, group.Value);
    string letterFilePath = fileStorageService.GetLetterFilePath(group.Key);

    Console.WriteLine($"- {Path.GetFileName(letterFilePath)}: {group.Value.Count} matching name(s), {addedNamesCount} newly added.");
}

static void PrintNames(string title, IReadOnlyList<string> names)
{
    Console.WriteLine(title);

    if (names.Count == 0)
    {
        Console.WriteLine("- File is currently empty.");
        Console.WriteLine();
        return;
    }

    foreach (string name in names)
    {
        Console.WriteLine($"- {name}");
    }

    Console.WriteLine();
}