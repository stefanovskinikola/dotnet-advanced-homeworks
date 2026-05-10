namespace Task04_Employee;

public class Programmer : Employee
{
    private readonly int _overtimeHours;
    private const double OvertimeRate = 1000.0;

    public Programmer(string name, double baseSalary, int overtimeHours)
        : base(name, baseSalary)
    {
        _overtimeHours = overtimeHours;
    }

    public override double CalculateSalary() => BaseSalary + (_overtimeHours * OvertimeRate);

    public override void DisplayInfo()
    {
        Console.WriteLine($"[Programmer] Name: {Name} | Base Salary: {BaseSalary:C} | Overtime Hours: {_overtimeHours} | Total Salary: {CalculateSalary():C}");
    }
}