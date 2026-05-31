public class AccessControlService
{
    private static readonly HashSet<string> _bannedUsers = new(StringComparer.OrdinalIgnoreCase)
    {
        "Bob", "Jill", "Alice"
    };

    public event Action<User>? UnauthorizedAccessDetected;

    public void Login(User user)
    {
        if (_bannedUsers.Contains(user.Name))
        {
            UnauthorizedAccessDetected?.Invoke(user);
        }
        else
        {
            Console.WriteLine($"Welcome {user.Name}.");
        }
    }
}