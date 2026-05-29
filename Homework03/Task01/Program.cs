PrintInConsole printer = new PrintInConsole();

printer.Print(42);
printer.Print("Hello, World!");
printer.Print(3.14);

printer.PrintCollection(new List<int> { 1, 2, 3 });
printer.PrintCollection(new List<string> { "apple", "banana", "cherry" });