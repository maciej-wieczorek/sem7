namespace Lambda
{
    internal class Program
    {
        public static bool IsOdd(int i)
        {
            return (i % 2 == 1);
        }

        public delegate void ParityChecker();

        public void zad13()
        {
            ParityChecker parityChecker = null;
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int i in arr.Where(parityChecker))
                Console.WriteLine(i);
        }
        static void Main(string[] args)
        {
            zad13();
            //int[] t = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            //foreach (int i in t.Where(x => x % 2 != 0)) { Console.WriteLine(i); }
        }
    }
}