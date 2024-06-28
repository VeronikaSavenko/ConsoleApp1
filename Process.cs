using System;
using System.Threading;
using System.Threading.Tasks;

class Process
{
    private readonly ResourceManager resourceManager;
    private readonly int[] resourcesToUse;
    private readonly int iterations;
    private readonly string processName;

    public Process(ResourceManager resourceManager, int[] resourcesToUse, int iterations, string processName)
    {
        this.resourceManager = resourceManager;
        this.resourcesToUse = resourcesToUse;
        this.iterations = iterations;
        this.processName = processName;
    }

    public void Run()
    {
        try
        {
            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine($"{processName} is starting iteration {i + 1}");
                resourceManager.AcquireResources(resourcesToUse, processName);
                Console.WriteLine($"{processName} is working with resources {string.Join(", ", resourcesToUse)}");
                Thread.Sleep(new Random().Next(1000));  // Simulate doing work with resources
                resourceManager.ReleaseResources(resourcesToUse, processName);
                Console.WriteLine($"{processName} finished working with resources {string.Join(", ", resourcesToUse)}");
                Console.WriteLine($"{processName} is sleeping before next iteration");
                Thread.Sleep(new Random().Next(1000));  // Simulate time between resource usages

                // Release resources after each iteration
                resourceManager.ReleaseResources(resourcesToUse, processName);
            }
            Console.WriteLine($"{processName} has completed all iterations");
        }
        catch (ThreadInterruptedException ex)
        {
            Console.WriteLine(ex);
        }
    }
}