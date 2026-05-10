namespace Task02_ShapeInterface;

public class Circle : IShape
{
    private readonly double _radius;

    public Circle(double radius)
    {
        _radius = radius;
    }

    public double GetArea() => Math.PI * _radius * _radius;
}
