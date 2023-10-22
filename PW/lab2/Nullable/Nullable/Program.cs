internal class Program
{
    public class Boo
    {
        public void ShowMessage() => Console.WriteLine("Hello!");
    }
    private static void Main(string[] args)
    {
        Nullable<bool> myBool = null;
        if (myBool.GetValueOrDefault())
        {
            Console.WriteLine("bool");
        }

        Boo boo = null;
        //boo.ShowMessage();
        //boo!.ShowMessage();
        boo?.ShowMessage();
        (boo ?? new Boo()).ShowMessage();
    }
}