using ContainersApp.Interfaces;

namespace ContainersApp
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
            foreach(IContainer c in blc.GetContainers())
            {
                Console.WriteLine($"{c.ID}: {c.Producer.Name} {c.Name} {c.ProductionYear} {c.Type}");
            }
        }
    }
}
