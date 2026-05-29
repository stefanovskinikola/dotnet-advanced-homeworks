// This project name was suggested by AI :)

// Filter all cars that have origin from Europe and print them in console.
var europeanCars = CarsData.Cars
    .Where(car => car.Origin == "Europe")
    .ToList();

foreach (var car in europeanCars)
    Console.WriteLine(car.Model);

Console.WriteLine();

// Filter all cars that have more that 6 Cylinders not including 6 after that Filter all cars that have
// exactly 4 Cylinders and have HorsePower more then 110.0. Join them in one result and print them in console.

var moreThanSixCylinders = CarsData.Cars
    .Where(car => car.Cylinders > 6);

var fourCylinderHighHp = CarsData.Cars
    .Where(car => car.Cylinders == 4 && car.HorsePower > 110);

var combinedResult = moreThanSixCylinders
    .Union(fourCylinderHighHp)
    .ToList();

foreach (var car in combinedResult)
    Console.WriteLine($"{car.Model} — {car.Cylinders} cyl, {car.HorsePower} hp");

Console.WriteLine();

// Count all cars based on their Origin and print the result in console. Example outpur "US 10 models\n Eu 15 Models".
var countByOrigin = CarsData.Cars
    .GroupBy(car => car.Origin)
    .Select(group => new { Origin = group.Key, Count = group.Count() })
    .ToList();

foreach (var group in countByOrigin)
    Console.WriteLine($"{group.Origin} {group.Count} models");

Console.WriteLine();

// Filter all cars that have more then 200 HorsePower and Find out how much is the lowest, highes and average Miles per galon and print them in console.
var highHpCars = CarsData.Cars
    .Where(car => car.HorsePower > 200)
    .ToList();

if (highHpCars.Count > 0)
{
    double minMpg = highHpCars.Min(car => car.MilesPerGalon);
    double maxMpg = highHpCars.Max(car => car.MilesPerGalon);
    double avgMpg = highHpCars.Average(car => car.MilesPerGalon);

    Console.WriteLine($"Cars found: {highHpCars.Count}");
    Console.WriteLine($"Lowest MPG:  {minMpg:F1}");
    Console.WriteLine($"Highest MPG: {maxMpg:F1}");
    Console.WriteLine($"Average MPG: {avgMpg:F2}");
}
else
{
    Console.WriteLine("No cars found with HorsePower > 200.");
}