namespace Task04_Employee;

public class Manager : Employee
{
    private readonly double _bonus;

    public Manager(string name, double baseSalary, double bonus)
        : base(name, baseSalary)
    {
        _bonus = bonus;
    }

    public override double CalculateSalary() => BaseSalary + _bonus;

    public override void DisplayInfo()
    {
        Console.WriteLine($"[Manager] Name: {Name} | Base Salary: {BaseSalary:C} | Bonus: {_bonus:C} | Total Salary: {CalculateSalary():C}");
    }
}