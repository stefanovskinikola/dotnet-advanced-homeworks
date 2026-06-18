using TimeTrackingApp.Application.Abstractions.Persistence;
using TimeTrackingApp.Domain.Entities;

namespace TimeTrackingApp.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly JsonFileStore _fileStore;
    private readonly AppDataPaths _appDataPaths;

    public UserRepository(JsonFileStore fileStore, AppDataPaths appDataPaths)
    {
        _fileStore = fileStore;
        _appDataPaths = appDataPaths;
    }

    public async Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _fileStore.ReadCollectionAsync<User>(_appDataPaths.UsersFilePath, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var users = await GetAllAsync(cancellationToken);
        return users.FirstOrDefault(x => x.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var normalizedUsername = username.Trim();
        var users = await GetAllAsync(cancellationToken);
        return users.FirstOrDefault(x => string.Equals(x.Username, normalizedUsername, StringComparison.OrdinalIgnoreCase));
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        var users = (await GetAllAsync(cancellationToken)).ToList();
        users.Add(user);
        await _fileStore.WriteCollectionAsync(_appDataPaths.UsersFilePath, users, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        var users = (await GetAllAsync(cancellationToken)).ToList();
        var index = users.FindIndex(x => x.Id == user.Id);
        if (index >= 0)
        {
            users[index] = user;
            await _fileStore.WriteCollectionAsync(_appDataPaths.UsersFilePath, users, cancellationToken);
        }
    }
}
