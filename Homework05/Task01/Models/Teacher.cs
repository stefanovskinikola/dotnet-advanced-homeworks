public class Teacher
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }

    private event Action<string>? _notificationEvent;

    public Teacher(string name, int age, string email, string subject)
    {
        Name = name;
        Age = age;
        Email = email;
        Subject = subject;
    }

    public void Subscribe(Student student)
    {
        _notificationEvent += student.GetNotification;
        Console.WriteLine($"{student.Name} subscribed to Professor {Name}'s notifications.");
    }

    public void Unsubscribe(Student student)
    {
        _notificationEvent -= student.GetNotification;
        Console.WriteLine($"{student.Name} unsubscribed from Professor {Name}'s notifications.");
    }

    public void SendNotification()
    {
        Console.WriteLine("Sending notifications....");
        _notificationEvent?.Invoke($"Notification from Professor {Name}: Class for {Subject} will start at 10 AM.");
    }
}