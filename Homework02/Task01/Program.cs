Console.WriteLine("Search by ID (ID = 3)");
var byId = UserDatabase.SearchById(3);
if (byId is not null) byId.PrintInfo();
else Console.WriteLine("No user found.");

Console.WriteLine("\nSearch by Name (\"ali\")");
var byName = UserDatabase.SearchByName("ali").ToList();
if (byName.Count > 0) byName.ForEach(user => user.PrintInfo());
else Console.WriteLine("No user found.");

Console.WriteLine("\nSearch by Age (age = 28)");
var byAge = UserDatabase.SearchByAge(28).ToList();
if (byAge.Count > 0) byAge.ForEach(user => user.PrintInfo());
else Console.WriteLine("No user found.");