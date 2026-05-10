using Task03_AbstractShape;

Shape circle = new Circle(7);
Shape triangle = new Triangle(3, 4, 5);

Console.WriteLine($"Circle - Area: {circle.CalculateArea():F2} | Perimeter: {circle.CalculatePerimeter():F2}");
Console.WriteLine($"Triangle - Area: {triangle.CalculateArea():F2} | Perimeter: {triangle.CalculatePerimeter():F2}");