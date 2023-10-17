internal class Program
{

    private static void Main(string[] args)
    {
        MyClass myClass = new MyClass();
        Console.WriteLine("Sum args: {0}", myClass.Sum(1, 2, 3, 4, 5));
        float avg; // does not need to be initialized, because out, also must assign value
        int sum = myClass.SumAvg(out avg, 1, 2, 3, 4, 5);
        Console.WriteLine("Avg args: {0} sum args: {1}", avg, sum);
        Console.WriteLine("Div: {0}", myClass.Div(10));
        Console.WriteLine("Div: {0}", myClass.Div(10, 3));
        Console.WriteLine("Div: {0}", myClass.Div(y: 3, x: 10));
        Console.WriteLine("Div: {0}", myClass.Div(x: 3, 10));
        //Console.WriteLine("Div: {0}", myClass.Div(y: 3, 10));// error
    }
}

public class MyClass
{
    public int Sum(params int[] arg)
    {
        int sum = 0;
        for (int i = 0; i < arg.Length; i++)
        {
            sum += arg[i];
        }

        return sum;
    }

    public int SumAvg(out float avg, params int[] arg)
    {
        int sum = arg.Sum();
        avg = sum / arg.Length;
        return sum;
    }

    public float Div(float x, float y = 5)
    {
        return x / y;
    }
}