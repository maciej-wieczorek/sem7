internal class Program
{
    class MyClass
    {
        public int MyField;
    }
    struct Point
    {
        public int X;
        public int Y;
        public MyClass myClass;
        public void Print()
        {
            Console.WriteLine("X: {0}, Y: {1}, C: {2}", X, Y, myClass.MyField);
        }
    }
    private static void Main(string[] args)
    {
        string s = null;
        int[2] tab = new int[2];
        Point p1;
        //p1.X = 1;
        //p1.Y = 1;
        //p1.Print();

        Point p2;
        //p2.Print();

        Point p3 = new Point();
        p3.myClass = new MyClass();
        p3.Print();

        f();

        Point p4 = new Point();
        //Point p4;
        g(p4);

        p4.Print();
    }

    private static void f()
    {
        Console.WriteLine("f()");
        Point p1  = new Point();
        //p1.X = 1;
        //p1.Y = 1;

        Point p2;
        p1.myClass = new MyClass();
        p1.myClass.MyField = 5;
        p2 = p1;
        p1.myClass.MyField = 6;
        p2.Print();

        p1.X = 2;
        p1.Print();
        p2.Print();
    }

    private static void g(Point p)
    {
        Console.WriteLine("g()");
        p.X = 3;
        p.Y = 4;
    }
}