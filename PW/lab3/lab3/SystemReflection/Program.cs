using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        Assembly a = Assembly.UnsafeLoadFrom(@"C:\Users\macie\Desktop\studia\przedmioty\PW\lab3\HiddenLibrary.dll");
        foreach (var t in a.GetTypes())
        {
            Console.WriteLine($"{t.Namespace}.{t.Name}");
        }

        Type techUniversity = a.GetType("HiddenLibrary.TechnicalUniveristy");
        Type[] ti = techUniversity.GetInterfaces();

        var o = Activator.CreateInstance(techUniversity, new object[]{""});
    }
}