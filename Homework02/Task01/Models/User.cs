public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    public User(int id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }

    public void PrintInfo() => Console.WriteLine($"#{Id} | Name: {Name} | Age: {Age}");
}