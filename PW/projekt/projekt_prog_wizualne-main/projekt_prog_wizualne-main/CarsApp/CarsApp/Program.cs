using CarsApp.BLC;
using CarsApp.Interfaces;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        string? libraryName = System.Configuration.ConfigurationManager.AppSettings["DBLibraryName"];

        BLC blc = new BLC(libraryName);

        foreach (IProducer p in blc.GetProducers())
        {
            Console.WriteLine($"{p.ID}: {p.Name}");
        }
        Console.WriteLine("-----------------------------------");

        foreach (IMedicine c in blc.GetMedicines())
        {
            Console.WriteLine($"{c.ID}: {c.Producer.Name} {c.Name} {c.Price} {c.MedType}");
        }
    }
}
