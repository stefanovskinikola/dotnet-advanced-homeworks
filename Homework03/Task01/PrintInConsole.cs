public class PrintInConsole
{
    public void Print<T>(T value)
        => Console.WriteLine(value);

    public void PrintCollection<T>(IEnumerable<T> collection)
    {
        foreach (var item in collection)
            Console.WriteLine(item);
    }
}
