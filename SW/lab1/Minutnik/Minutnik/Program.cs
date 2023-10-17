internal class Program
{
    private static void Main(string[] args)
    {
        System.DateTime end = new DateTime(2023, 10, 31, 13, 15, 0);
        string prevDiffString = "";
        while (true)
        {
            TimeSpan diff = end - System.DateTime.Now;
            string diffString = diff.ToString(@"hh\:mm\:ss");
            if (diffString != prevDiffString)
            {
                Console.Clear();
                Console.SetCursorPosition(Console.WindowWidth - diffString.Length, 0);
                Console.WriteLine(diffString);
                prevDiffString = diffString;
            }
        }
    }
}