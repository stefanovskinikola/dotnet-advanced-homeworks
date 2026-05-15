public static class UserDatabase
{
    public static List<User> Users = new()
    {
        new User(1, "Alice",   28),
        new User(2, "Bob",     34),
        new User(3, "Charlie", 22),
        new User(4, "Diana",   28),
        new User(5, "Eve",     45),
    };

    public static User? SearchById(int id)
        => Users.FirstOrDefault(user => user.Id == id);

    public static IEnumerable<User> SearchByName(string name)
        => Users.Where(user => user.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

    public static IEnumerable<User> SearchByAge(int age)
        => Users.Where(user => user.Age == age);
}