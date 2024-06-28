using System;
using System.Threading;
using System.Threading.Tasks;

public class ResourceManager
{
    private readonly int numberOfResources;
    private readonly bool[] resourceState;
    private readonly object lockObject;
    private readonly ConditionVariable[] resourceAvailable;

    public ResourceManager(int numberOfResources)
    {
        this.numberOfResources = numberOfResources;
        this.resourceState = new bool[numberOfResources];
        for (int i = 0; i < numberOfResources; i++)
        {
            resourceState[i] = true; // All resources are free at the beginning
        }
        this.lockObject = new object();
        this.resourceAvailable = new ConditionVariable[numberOfResources];
        for (int i = 0; i < numberOfResources; i++)
        {
            resourceAvailable[i] = new ConditionVariable(lockObject);
        }
    }

    //public bool AcquireResources(int[] resourcesToAcquire, string processName)
    //{
    //    lock (lockObject)
    //    {
    //        try
    //        {
    //            foreach (var resource in resourcesToAcquire)
    //            {
    //                while (!resourceState[resource - 1])
    //                {
    //                    Console.WriteLine($"{processName} is waiting for resource {resource}");
    //                    resourceAvailable[resource - 1].Wait(); // Wait for the resource to be released
    //                }
    //            }
    //            // If all resources are available, acquire them
    //            foreach (var resource in resourcesToAcquire)
    //            {
    //                resourceState[resource - 1] = false;
    //                Console.WriteLine($"{processName} acquired resource {resource}");
    //            }
    //            return true;
    //        }
    //        finally
    //        {
    //            Monitor.Exit(lockObject);
    //        }
    //    }
    //}
    public bool AcquireResources(int[] resourcesToAcquire, string processName)
    {
        lock (lockObject)
        {
            try
            {
                bool allAcquired = true;

                foreach (var resource in resourcesToAcquire)
                {
                    while (!resourceState[resource - 1])
                    {
                        Console.WriteLine($"{processName} is waiting for resource {resource}");
                        Monitor.Wait(lockObject); // Wait for the resource to be released
                    }

                    // Acquire the resource
                    resourceState[resource - 1] = false;
                    Console.WriteLine($"{processName} acquired resource {resource}");
                }

                return true;
            }
            finally
            {
                Monitor.PulseAll(lockObject); // Signal waiting threads
            }
        }
    }


    //public void ReleaseResources(int[] resourcesToRelease, string processName)
    //{
    //    lock (lockObject)
    //    {
    //        try
    //        {
    //            // Release the acquired resources
    //            foreach (var resource in resourcesToRelease)
    //            {
    //                resourceState[resource - 1] = true;
    //                Console.WriteLine($"{processName} released resource {resource}");
    //                // Notify waiting threads about the resource release
    //                resourceAvailable[resource - 1].Signal();
    //            }
    //        }
    //        finally
    //        {
    //            Monitor.Exit(lockObject);
    //        }
    //    }
    //}
    public void ReleaseResources(int[] resourcesToRelease, string processName)
    {
        lock (lockObject)
        {
            try
            {
                foreach (var resource in resourcesToRelease)
                {
                    resourceState[resource - 1] = true;
                    Console.WriteLine($"{processName} released resource {resource}");
                }

                Monitor.PulseAll(lockObject); // Signal waiting threads
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in ReleaseResources: {ex.Message}");
            }
        }
    }
}

//public class ConditionVariable
//{
//    private readonly object lockObject;
//    private readonly SemaphoreSlim semaphore = new SemaphoreSlim(0, 1);

//    public ConditionVariable(object lockObject)
//    {
//        this.lockObject = lockObject;
//    }

//    public void Wait()
//    {
//        Monitor.Exit(lockObject);
//        semaphore.Wait();
//        Monitor.Enter(lockObject);
//    }

//    public void Signal()
//    {
//        semaphore.Release();
//    }
//}
public class ConditionVariable
{
    private readonly object lockObject;
    private readonly SemaphoreSlim semaphore = new SemaphoreSlim(0, 1);

    public ConditionVariable(object lockObject)
    {
        this.lockObject = lockObject;
    }

    public void Wait()
    {
        // Вихід з блокування потоку перед очікуванням
        Monitor.Exit(lockObject);

        try
        {
            semaphore.Wait(); // Очікування сигналу
        }
        finally
        {
            // Вхід до блокування після отримання сигналу
            Monitor.Enter(lockObject);
        }
    }

    public void Signal()
    {
        semaphore.Release(); // Відправлення сигналу
    }
}
