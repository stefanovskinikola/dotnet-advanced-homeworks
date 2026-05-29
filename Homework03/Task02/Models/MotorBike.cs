public class MotorBike : Vehicle
{
    public override void DisplayInfo()
        => Console.WriteLine("Im a motorbike and i drive on 2 wheels :)");

    public void Wheelie()
        => Console.WriteLine("Driving on one wheel");
}