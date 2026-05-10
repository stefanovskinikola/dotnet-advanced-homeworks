namespace Task03_AbstractShape;

public class Circle : Shape
{
    private readonly double _radius;

    public Circle(double radius)
    {
        _radius = radius;
    }

    public override double CalculateArea() => Math.PI * _radius * _radius;

    public override double CalculatePerimeter() => 2 * Math.PI * _radius;
}