using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Wykład4.DelegatCallback
{
    public class DelegatCallback
    {
        public class Pracownik : IComparable
        {
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public int Wiek { get; set; }
            public int Wyplata { get; set; }

            public int CompareTo(object o)
            {
                Pracownik p = o as Pracownik;
                return this.Nazwisko.CompareTo(p.Nazwisko);
            }

            public override string ToString()
            {
                return $"[{Imie} {Nazwisko}:{Wiek} {Wyplata}]";
            }
        }

        public delegate void BudgetAlertHandler(int missingAmount);

        public class Firma : IEnumerable
        {
            private Pracownik[] pracownicy;
            private int budzet;
            private int wydatki;
            private BudgetAlertHandler budgetAlertHandlers;

           /* public void RegisterBudgetAlert( BudgetAlertHandler metoda)
            {
                 budgetAlertHandlers += metoda;
            }*/
            public event BudgetAlertHandler BudgetExceeded;

            public delegate Pracownik SelectTheBest(IEnumerable<Pracownik> p);

            //public SelectTheBest Selector = null;
            public Func<IEnumerable<Pracownik>, Pracownik> Selector = null;

            public Pracownik GetTheBest()
            {
                if (Selector != null)
                    return Selector(pracownicy);
                else
                    return null;
            }

            public static Pracownik GetSecond(IEnumerable<Pracownik> p)
            {
                return p.ElementAt(1);
            }

            public static Pracownik GetNajlepiejZarabiajacy(IEnumerable<Pracownik> p)
            {
                int maxWyplata = p.Max(x => x.Wyplata);
                return p.First(x => x.Wyplata == maxWyplata);
            }

            public static Pracownik GetNajmlodszy(IEnumerable<Pracownik> p)
            {
                int minWiek = p.Min(x => x.Wiek);
                return p.First(x => x.Wiek == minWiek);
            }
            public static Pracownik GetNajkrotszeImie(IEnumerable<Pracownik> p)
            {
                int minLenImie = p.Min(x => x.Imie.Length);
                return p.First(x => x.Imie.Length == minLenImie);
            }

            public Firma()
            {
                budzet = 20000;
                pracownicy = new Pracownik[]
                {
                    new Pracownik(){ Imie="Jan", Nazwisko="Kowalski", Wiek=43, Wyplata=4000},
                    new Pracownik(){ Imie="Marian", Nazwisko="Nowak", Wiek=32, Wyplata=5000},
                    new Pracownik(){ Imie="Alicja", Nazwisko="Kowalska", Wiek=27, Wyplata=4000},
                    new Pracownik(){ Imie="Grażyna", Nazwisko="Wiśniewska", Wiek=40, Wyplata=5500}
                };
                wydatki = 18500;
                
            }

            public System.Collections.IEnumerator GetEnumerator()
            {
                foreach (Pracownik p in pracownicy)
                {
                    yield return p;
                }
            }

            public IEnumerable GetByName()
            {
                yield return pracownicy[2];
                yield return pracownicy[0];
                yield return pracownicy[1];
                yield return pracownicy[3];
            }

            public void SalaryIncrease(float percent)
            {
                wydatki = 0;
                foreach (Pracownik p in pracownicy)
                {
                    p.Wyplata = (int)(p.Wyplata * (1.0f + percent));
                    wydatki += p.Wyplata;
                }
                if ((wydatki > budzet) && budgetAlertHandlers != null)
                    budgetAlertHandlers((wydatki - budzet)); 
                if ((wydatki > budzet) && BudgetExceeded != null)
                    BudgetExceeded((wydatki - budzet));

            }

            public void ZarejsetrujMetodyObslugi(int argument, Firma firma)
            {
                //int zmiennaLokalna = 7;
                
                firma.BudgetExceeded += delegate (int a)
                {
                    int zmiennaLokalna = 3;
                    Console.WriteLine($"zmienna lokalna {zmiennaLokalna} {argument}");
                    Console.WriteLine($"przekroczono o kwotę {a:C} z {budzet} ");
                };

            }

        }

        public static void PrzekroczonoBudzet(int kwota)
        {
            Console.WriteLine("UWAGA: budżet przekroczono o: {0:C}", kwota);
        }

        public static void ZarejsetrujMetodyObslugi( int argument, Firma firma )
        {
            int zmiennaLokalna = 7;

            firma.BudgetExceeded += delegate (int a)
            {
                Console.WriteLine($"zmienna lokalna {zmiennaLokalna} {argument}");
                Console.WriteLine( $"przekroczono o kwotę {a:C} ");
            };
        }

        public static void test()
        {
            Firma f = new Firma();
            f.Selector = delegate (IEnumerable<Pracownik> p)
            {
                return p.First();
            };
            Console.WriteLine(f.GetTheBest());
            f.Selector = Firma.GetSecond;
            Console.WriteLine(f.GetTheBest());
            f.Selector = (IEnumerable<Pracownik> p) => p.ElementAt(1);
            Console.WriteLine(f.GetTheBest());
        }

        public static void zad3()
        {
            Firma f = new Firma();
            f.Selector = Firma.GetNajlepiejZarabiajacy;
            Console.WriteLine(f.GetTheBest());
            f.Selector = Firma.GetNajmlodszy;
            Console.WriteLine(f.GetTheBest());
            f.Selector = Firma.GetNajkrotszeImie;
            Console.WriteLine(f.GetTheBest());
        }

        public static void Main()
        {
            //test();
            zad3();
            //Firma firma = new Firma();
            ////firma.RegisterBudgetAlert(PrzekroczonoBudzet);
            //firma.BudgetExceeded += PrzekroczonoBudzet;
            //firma.BudgetExceeded += new BudgetAlertHandler(PrzekroczonoBudzet);
            //firma.ZarejsetrujMetodyObslugi(10, firma);
            ////BudgetAlertHandler bdg = null;
            
            //firma.BudgetExceeded += delegate 
            //{
            //    Console.WriteLine("przekroczono kwote o: {0:C}");
            //};

            //firma.BudgetExceeded += (int s) => Console.WriteLine("aaaa");

            //firma.SalaryIncrease(0.2f);
        }
    }
}
