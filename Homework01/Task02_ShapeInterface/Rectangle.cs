namespace Task02_ShapeInterface;

public class Rectangle : IShape
{
    private readonly double _width;
    private readonly double _height;

    public Rectangle(double width, double height)
    {
        _width = width;
        _height = height;
    }

    public double GetArea() => _width * _height;
}
