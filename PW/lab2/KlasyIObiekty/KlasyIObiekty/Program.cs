internal class Program
{
    public class Worker
    {
        public Worker(string imie_, string nazwisko_, uint rokUrodzenia_)
        {
            Imie = imie_;
            Nazwisko = nazwisko_;
            RokUrodzenia = rokUrodzenia_;
            GenerateNewID();
        }
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} {RokUrodzenia} {ID}";
        }
        public void GenerateNewID()
        {
            ID = Guid.NewGuid();
        }
        public string Imie { get; set;}
        public string Nazwisko { get; set;}
        public Guid ID { get; set;}
        public uint RokUrodzenia { get; set; }
    }

    public sealed class OfficeWorker : Worker
    {
        public OfficeWorker(string imie_, string nazwisko_, uint rokUrodzenia_)
            : base(imie_, nazwisko_, rokUrodzenia_)
        {

        }
    }

    public class Manager : Worker
    {
        public Manager(string imie_, string nazwisko_, uint rokUrodzenia_)
            : base(imie_, nazwisko_, rokUrodzenia_)
        {

        }

        public override sealed string ToString()
        {
            return $"Manager: {base.ToString()}";
        }
    }
    public class Supervisor : Manager
    {
        public Supervisor(string imie_, string nazwisko_, uint rokUrodzenia_)
            : base(imie_, nazwisko_, rokUrodzenia_)
        {

        }

        public new string ToString()
        {
            return $"Supervisor: {base.ToString()}";
        }
    }

    public class MyClass
    {
        static MyClass()
        {
            Console.WriteLine("MyClass static constructor");
            staticMamber = 1;
            MyClass myClass = new MyClass();
        }

        public MyClass()
        {
            Console.WriteLine("MyClass constructor");
        }

        static int staticMamber;
        int member;
    }
    private static void Main(string[] args)
    {
        Worker worker = new Worker("Maciej", "Wieczorek", 2000);
        OfficeWorker officeWorker = new OfficeWorker("Maciej1", "Wieczorek1", 2000) {Imie = "Stefan"};
        Console.WriteLine(worker.ToString());
        Console.WriteLine(officeWorker.ToString());
        Manager manager = new Manager("Manager_imie", "Manager_nazwisko", 1980);
        Console.WriteLine(manager.ToString());
        Supervisor supervisor = new Supervisor("Supervisor_imie", "Supervisor_nazwisko", 1975);
        Console.WriteLine(supervisor.ToString());

        MyClass myClass1 = new MyClass();
        MyClass myClass2 = new MyClass();
    }
}