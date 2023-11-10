using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wykład4.DelegatEvent
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
            return $"[{Imie} {Nazwisko}:{Wiek}]";
        }
    }

    public delegate void BudgetAlertHandler(int missingAmount);

    public class Firma : IEnumerable
    {
        private Pracownik[] pracownicy;
        private int budzet;
        private int wydatki;

        public event BudgetAlertHandler BudgetExceeded;

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

        //nieistotne w kontekście delgatów
        public System.Collections.IEnumerator GetEnumerator()
        {
            foreach (Pracownik p in pracownicy)
            {
                yield return p;
            }
        }

        //nieistotne w kontekście delegatów
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
            if ((wydatki > budzet) && BudgetExceeded != null)
                BudgetExceeded((wydatki - budzet));

        }


    }

    public class DelegatCallback
    {

        public static void PrzekroczonoBudzet(int kwota)
        {
            Console.WriteLine("UWAGA: budżet przekroczono o: {0:C}", kwota);
        }

        public static void ZarejsetrujMetodyObslugi(int argument, Firma firma)
        {
            int zmiennaLokalna = 7;

            firma.BudgetExceeded += delegate (int a)
            {
                Console.WriteLine($"zmienna lokalna {zmiennaLokalna} {argument}");
                Console.WriteLine($"przekroczono o kwotę {a:C} ");
            };
        }


        public static void Main()
        {
            Firma firma = new Firma();

            //dodanie metody obsługi zdarzenia jako metody statycznej
            firma.BudgetExceeded += PrzekroczonoBudzet;

            //dodanie metody anonimowej
            firma.BudgetExceeded += delegate (int i)
            {
                Console.WriteLine("przekroczono kwote o: {0:C}", i);
            };

            //wywołanie podwyżki w celu przekroczenia budżetu
            firma.SalaryIncrease(0.2f);
        }
    }
}
