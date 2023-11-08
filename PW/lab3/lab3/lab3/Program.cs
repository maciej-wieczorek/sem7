using System.Collections;
using System.Runtime.InteropServices;

internal class Program
{

    public interface IPrintable
    {
        int ID { get; set; }
        void Print();
    }
    public interface IPublicPrintable
    {
        void Print();
    }
    public class Worker : IPrintable, IPublicPrintable, IComparable, IComparer
    {
        public string Name { get; set; }

        public int Wiek { get; set; }
        public int ID { get; set; }

        public override string ToString()
        {
            return $"{Name} # {Wiek}";
        }

        public virtual void Print()
        {
            Console.WriteLine($"Worker {ToString()} ID: {ID}");
        }
        void IPublicPrintable.Print()
        {
            Console.WriteLine($"Worker {ToString()}");
        }

        public virtual int CompareTo(object w2)
        {
            return 0;
        }
        public virtual int Compare(object w1, object w2)
        {
            return 0;
        }
    }

    public class Manager : Worker
    {
        public override void Print()
        {
            Console.WriteLine($"Manager {ToString()} ID: {ID}");
        }
    }

    public class Supervisor : Manager
    {
        public override void Print()
        {
            Console.WriteLine($"Supervisor {ToString()} ID: {ID}");
        }

    }

    public interface I1
    {
        void T();
    }

    public class C1 : I1 
    {
        public virtual void T() { Console.WriteLine("C1"); }
    }
    public class C2 : C1, I1
    {
        public override void T() { Console.WriteLine("C2"); }
    }
    public class C3 : C2, I1
    {
        public new void T() { Console.WriteLine("C3"); }
    }

    /*
      new Worker { ID=1, Name="Jan Kowalski", Wiek=50}, 
      new Supervisor { ID = 2, Name="Marian Nowak", Wiek = 33}, 
      new Supervisor { ID = 3, Name= "Alicja Kowalska", Wiek=48 }, 
      new Manager { ID = 4, Name="Grażyna Wiśniewska", Wiek=50 }, 
      new Manager { ID = 5, Name= "Janusz Wójcik", Wiek=37 }, 
      new Worker { ID = 6, Name="Mariusz Mariusz", Wiek=22 }
    */

    public static void zad3()
    {
        Worker[] workers = {
          new Worker { ID=1, Name="Jan Kowalski", Wiek=50},
          new Supervisor { ID = 2, Name="Marian Nowak", Wiek = 33},
          new Supervisor { ID = 3, Name= "Alicja Kowalska", Wiek=48 },
          new Manager { ID = 4, Name="Grażyna Wiśniewska", Wiek=50 },
          new Manager { ID = 5, Name= "Janusz Wójcik", Wiek=37 },
          new Worker { ID = 6, Name="Mariusz Mariusz", Wiek=22 }};

        foreach (Worker worker in workers)
        {
            ((IPrintable)worker).Print();
        }
    }

    public static void zad4()
    {
        Supervisor m = new Supervisor() { ID = 11 };
        m.Print();
        ((IPrintable)m).Print();
        ((IPublicPrintable)m).Print();
    }

    public class Office : IEnumerable
    {
        Worker[] workers;
        public Office()
        {
            workers = new Worker[] {
              new Worker { ID=1, Name="Jan Kowalski", Wiek=50},
              new Supervisor { ID = 2, Name="Marian Nowak", Wiek = 33},
              new Supervisor { ID = 3, Name= "Alicja Kowalska", Wiek=48 },
              new Manager { ID = 4, Name="Grażyna Wiśniewska", Wiek=50 },
              new Manager { ID = 5, Name= "Janusz Wójcik", Wiek=37 },
              new Worker { ID = 6, Name="Mariusz Mariusz", Wiek=22 }};
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return workers.GetEnumerator();
        }

        public IEnumerable GetWorkersByName(bool ascending)
        {
            if (ascending)
            {
                yield return workers[0];
            }
            else
            {
                yield return workers[5];
            }
        }
        public IEnumerable GetWorkersByPosition(bool ascending)
        {
            if (ascending)
            {
                yield return workers[0];
            }
            else
            {
                yield return workers[5];
            }
        }
    }

    public static void test()
    {
        C1 c3 = new C3();
        //c3.T();
        C1 c1_3 = (C1)c3;
        c1_3.T();
        ((I1)c1_3).T();
    }

    public static void testEnumerable()
    {
        Office office = new Office();
        foreach (Worker w in office)
        {
            w.Print();
        }

        Console.WriteLine("GetWorkersByName");
        foreach (Worker w in office.GetWorkersByName(true))
        {
            w.Print();
        }

        Console.WriteLine("GetWorkersByPosition");
        foreach (Worker w in office.GetWorkersByPosition(true))
        {
            w.Print();
        }
    }

    public static void testCompare()
    {

    }
    private static void Main(string[] args)
    {
        //zad4();
        //test();
        testEnumerable();
    }
}