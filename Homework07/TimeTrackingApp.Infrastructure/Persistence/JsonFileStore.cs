using System.Text.Json;

namespace TimeTrackingApp.Infrastructure.Persistence;

public class JsonFileStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true
    };

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task<IReadOnlyCollection<T>> ReadCollectionAsync<T>(string filePath, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            EnsureFile(filePath, "[]");

            await using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var data = await JsonSerializer.DeserializeAsync<List<T>>(stream, SerializerOptions, cancellationToken);
            return data ?? new List<T>();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task WriteCollectionAsync<T>(string filePath, IEnumerable<T> items, CancellationToken cancellationToken = default)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            EnsureDirectory(filePath);

            await using var stream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            await JsonSerializer.SerializeAsync(stream, items, SerializerOptions, cancellationToken);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private static void EnsureFile(string filePath, string defaultContent)
    {
        EnsureDirectory(filePath);

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, defaultContent);
        }
    }

    private static void EnsureDirectory(string filePath)
    {
        var directoryPath = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
}
