using System;
using System.Threading;
using System.Threading.Tasks;

public class Producer
{
    private readonly Buffer sharedLocation;
    private readonly int id;

    public Producer(Buffer shared, int id)
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
        for (int count = 1; count <= 3; count++)
        {
            ProduceNextValue(count);
        }
        Console.WriteLine($"producer {id} is finishing generation.\nFinishing thread producer {id}.");
    }

    private void ProduceNextValue(int value)
    {
        try
        {
            Thread.Sleep(new Random().Next(3001));
            sharedLocation.Set(value);
        }
        catch (ThreadInterruptedException ex)
        {
            Console.WriteLine(ex);
        }
    }
    //private readonly Buffer sharedLocation;

    //public Producer(Buffer shared, int id)
    //{
    //    Name = "producer " + id;
    //    this.sharedLocation = shared;
    //}

    //public override void Run()
    //{
    //    for (int count = 1; count <= 4; count++)
    //    {
    //        ProduceNextValue(count);
    //    }
    //    Console.WriteLine($"{Name} is finishing generation.\nFinishing thread {Name}.");
    //}

    //private void ProduceNextValue(int value)
    //{
    //    try
    //    {
    //        Thread.Sleep(new Random().Next(3001));
    //        sharedLocation.Set(value);
    //    }
    //    catch (ThreadInterruptedException ex)
    //    {
    //        Console.WriteLine(ex);
    //    }
    //}
}