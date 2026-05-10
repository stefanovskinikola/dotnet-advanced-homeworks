namespace Task02_ShapeInterface;

public class Triangle : IShape
{
    private readonly double _base;
    private readonly double _height;

    public Triangle(double @base, double height)
    {
        _base = @base;
        _height = height;
    }

    public double GetArea() => 0.5 * _base * _height;
}