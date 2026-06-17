public sealed class FileStorageService
{
    private const string FilesDirectoryName = "Files";
    private const string NamesFileName = "names.txt";

    private readonly string _filesDirectoryPath;
    private readonly string _namesFilePath;

    public FileStorageService()
    {
        string projectDirectoryPath = ResolveProjectDirectory();
        _filesDirectoryPath = Path.Combine(projectDirectoryPath, FilesDirectoryName);
        _namesFilePath = Path.Combine(_filesDirectoryPath, NamesFileName);
    }

    public string FilesDirectoryPath => _filesDirectoryPath;

    public string NamesFilePath => _namesFilePath;

    public void EnsureStorageExists()
    {
        Directory.CreateDirectory(_filesDirectoryPath);

        if (!File.Exists(_namesFilePath))
        {
            using FileStream fileStream = File.Create(_namesFilePath);
        }
    }

    public IReadOnlyList<string> ReadAllNames()
    {
        EnsureStorageExists();

        return File.ReadAllLines(_namesFilePath)
            .Select(name => name.Trim())
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .ToList();
    }

    public void AppendNames(IEnumerable<string> names)
    {
        EnsureStorageExists();

        List<string> validNames = names
            .Select(name => name.Trim())
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .ToList();

        if (validNames.Count == 0)
        {
            return;
        }

        File.AppendAllLines(_namesFilePath, validNames);
    }

    public IReadOnlyList<string> ReadNamesForLetter(char letter)
    {
        string filePath = GetLetterFilePath(letter);

        if (!File.Exists(filePath))
        {
            return [];
        }

        return File.ReadAllLines(filePath)
            .Select(name => name.Trim())
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .ToList();
    }

    public int MergeNamesForLetter(char letter, IEnumerable<string> names)
    {
        EnsureStorageExists();

        List<string> validNames = names
            .Select(name => name.Trim())
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .ToList();

        if (validNames.Count == 0)
        {
            return 0;
        }

        string filePath = GetLetterFilePath(letter);
        HashSet<string> existingNames = new(ReadNamesForLetter(letter), StringComparer.OrdinalIgnoreCase);

        List<string> namesToAppend = validNames
            .Where(existingNames.Add)
            .ToList();

        if (namesToAppend.Count == 0)
        {
            return 0;
        }

        File.AppendAllLines(filePath, namesToAppend);
        return namesToAppend.Count;
    }

    public string GetLetterFilePath(char letter)
    {
        char normalizedLetter = char.ToUpperInvariant(letter);
        return Path.Combine(_filesDirectoryPath, $"namesStartingWith_{normalizedLetter}.txt");
    }

    private static string ResolveProjectDirectory()
    {
        DirectoryInfo? currentDirectory = new(AppContext.BaseDirectory);

        while (currentDirectory is not null)
        {
            if (currentDirectory.GetFiles("*.csproj").Length > 0)
            {
                return currentDirectory.FullName;
            }

            currentDirectory = currentDirectory.Parent;
        }

        return Directory.GetCurrentDirectory();
    }
}