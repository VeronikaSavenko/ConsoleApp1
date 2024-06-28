using System;
using System.Threading;
using System.Threading.Tasks;

//public class Consumer
//{
//    private readonly Buffer sharedLocation;

//    public Consumer(Buffer shared, int id)
//    {
//        Name = "consumer " + id;
//        this.sharedLocation = shared;
//    }

//    public override void Run()
//    {
//        int sum = 0;
//        for (int count = 1; count <= 4; ++count)
//        {
//            sum += ConsumeNextValue();
//        }

//        Console.WriteLine($"{Name} sum of read values: {sum}.\nFinishing thread {Name}.");
//    }

//    private int ConsumeNextValue()
//    {
//        int value = 0;
//        try
//        {
//            Thread.Sleep(new Random().Next(3001));
//            value = sharedLocation.Get();
//        }
//        catch (ThreadInterruptedException ex)
//        {
//            Console.WriteLine(ex);
//        }
//        return value;
//    }
//}

public class Consumer
{
    private readonly Buffer sharedLocation;
    private readonly int id;

    public Consumer(Buffer shared, int id)
    {
        this.sharedLocation = shared;
        this.id = id;
    }

    public void Start()
    {
        Thread thread = new Thread(new ThreadStart(Run));
        thread.Start();
    }

    private void Run()
    {
        int sum = 0;
        for (int count = 1; count <= 3; ++count)
        {
            sum += ConsumeNextValue();
        }

        Console.WriteLine($"{Thread.CurrentThread.Name} sum of read values: {sum}.\nFinishing thread {Thread.CurrentThread.Name}.");
    }

    private int ConsumeNextValue()
    {
        int value = 0;
        try
        {
            Thread.Sleep(new Random().Next(3001));
            value = sharedLocation.Get();
        }
        catch (ThreadInterruptedException ex)
        {
            Console.WriteLine(ex);
        }
        return value;
    }
}
