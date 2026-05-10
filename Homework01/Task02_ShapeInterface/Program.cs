using Task02_ShapeInterface;

IShape rectangle = new Rectangle(5, 8);
IShape circle = new Circle(4);
IShape triangle = new Triangle(6, 9);

Console.WriteLine($"Rectangle area: {rectangle.GetArea():F2}");
Console.WriteLine($"Circle    area: {circle.GetArea():F2}");
Console.WriteLine($"Triangle  area: {triangle.GetArea():F2}");