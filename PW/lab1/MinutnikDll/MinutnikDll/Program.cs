using Minutnik;
internal class Program
{
    private static void Main(string[] args)
    {
        System.DateTime end = new DateTime(2023, 10, 31, 13, 15, 0);
        Minutnik.Minutnik minutnik = new Minutnik.Minutnik(end);
        Console.WriteLine(minutnik.GetDiffFormated());
    }
}