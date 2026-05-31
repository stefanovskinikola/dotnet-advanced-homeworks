AccessControlService accessControl = new();

accessControl.UnauthorizedAccessDetected += OnUnauthorizedAccess;

Console.Write("Enter your name: ");
string name = Console.ReadLine() ?? string.Empty;

User user = new(name);
accessControl.Login(user);

static void OnUnauthorizedAccess(User user)
{
    Console.WriteLine($"{user.Name} Users Found. Sending Email to Administration.");
    SendEmail();
    StartAlarm();
    Console.WriteLine("Logging out.");
}

static void SendEmail()
{
    Console.WriteLine("Email Sent.");
}

static void StartAlarm()
{
    Console.WriteLine("Warning Alarm Started.");
}