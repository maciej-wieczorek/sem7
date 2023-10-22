internal class Program
{
    private static void Main(string[] args)
    {
        (var Imie, var Nazwisko, var wiek) = ("Jan", "Nowak", 33);
        var (Imie2, Nazwisko2, wiek2) = ("Jan", "Nowak", 33);
        var pracownik = ("Jan", "Nowak", 33);
        (string Imie, string Nazwisko, int Wiek) prac = ("Jan", "Nowak",33);
        Console.WriteLine("");
    }
}