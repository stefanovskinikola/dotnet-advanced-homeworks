using System.Text;
using Task04_Employee;

Console.OutputEncoding = Encoding.UTF8;

Employee manager = new Manager("John", 150000, 30000);
Employee programmer = new Programmer("Jane", 100000, 20);

manager.DisplayInfo();
programmer.DisplayInfo();