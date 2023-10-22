internal class Program
{
    private static void Main(string[] args)
    {
        DateTime start = DateTime.Now;
        int[] tab = new int[(int)1e6];
        for (int i = 0; i < tab.Length; i++)
        {
            tab[i] = i;
        }
        long sum = 0;
        for (int i = 0;i < tab.Length;i++)
        {
            sum += tab[i];
        }

        DateTime end = DateTime.Now;
        TimeSpan diff = end - start;
        Console.WriteLine(sum);
        Console.WriteLine(diff);

        start = DateTime.Now;
        object[] tab2 = new object[(int)1e6];
        for (int i = 0; i < tab2.Length; i++)
        {
            tab2[i] = i;
        }
        sum = 0;
        for (int i = 0;i < tab2.Length;i++)
        {
            sum += (int)tab2[i];
        }
        end = DateTime.Now;
        TimeSpan diff2 = end - start;

        Console.WriteLine(sum);
        Console.WriteLine(diff2);

        Console.WriteLine("diff: {0}", diff2 / diff);
    }
}