public class Car
{
    public string Model { get; set; }
    public double MilesPerGalon { get; set; }
    public int Cylinders { get; set; }
    public double HorsePower { get; set; }
    public double Weight { get; set; }

    /// <summary>
    /// Acceleration time to 100kmph
    /// </summary>
    public double AccelerationTime { get; set; }

    public string Origin { get; set; }
}