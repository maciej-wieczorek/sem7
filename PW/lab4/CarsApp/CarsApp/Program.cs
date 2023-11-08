using CarsApp.Interfaces;

namespace CarsApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string libraryName = System.Configuration.ConfigurationManager.AppSettings["DBLibraryName"];
            BLC.BLC blc = new BLC.BLC(libraryName);

            foreach(IProducer p in blc.GetProducers())
            {
                Console.WriteLine($"{p.ID}: {p.Name}");
            }
            Console.WriteLine("--------------------");
            foreach(ICar c in blc.GetCars())
            {
                Console.WriteLine($"{c.ID}: {c.Producer.Name} {c.Name} {c.ProductionYear} {c.Transmission}");
            }
        }
    }
}