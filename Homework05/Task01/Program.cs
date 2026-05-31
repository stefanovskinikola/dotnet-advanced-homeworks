Teacher professor = new("John", 45, "john@email.edu", "Math");

Student alice = new("Alice", 20, "alice@email.edu", "Math");
Student bob = new("Bob", 22, "bob@email.edu", "Math");
Student charlie = new("Charlie", 21, "charlie@email.edu", "Math");

professor.Subscribe(alice);
professor.Subscribe(bob);
professor.Subscribe(charlie);

Console.WriteLine();

professor.SendNotification();

Console.WriteLine();

professor.Unsubscribe(alice);
professor.Unsubscribe(charlie);

Console.WriteLine();

professor.SendNotification();