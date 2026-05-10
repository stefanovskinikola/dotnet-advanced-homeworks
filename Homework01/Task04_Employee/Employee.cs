namespace Task04_Employee;

public abstract class Employee
{
    public string Name { get; }
    public double BaseSalary { get; }

    protected Employee(string name, double baseSalary)
    {
        Name = name;
        BaseSalary = baseSalary;
    }

    public abstract double CalculateSalary();

    public abstract void DisplayInfo();
}