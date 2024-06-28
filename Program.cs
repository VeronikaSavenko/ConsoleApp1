using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int numberOfProcesses = 3;
            const int numberOfResources = 2;
            const int iterations = 3;

            ResourceManager resourceManager = new ResourceManager(numberOfResources);

            Process[] processes = new Process[numberOfProcesses];

            processes[0] = new Process(resourceManager, new int[] { 1, 2 }, iterations, "Process-1");
            processes[1] = new Process(resourceManager, new int[] { 1, 2 }, iterations, "Process-2");
            processes[2] = new Process(resourceManager, new int[] { 2 }, iterations, "Process-3");

            foreach (Process process in processes)
            {
                new Thread(new ThreadStart(process.Run)).Start();
            }

            //SharedBufferTest
            Buffer sharedLocation = new SynchronizedBuffer();

            // Create producers
            Producer[] producers = new Producer[3];
            for (int i = 0; i < producers.Length; i++)
            {
                producers[i] = new Producer(sharedLocation, i + 1);
                producers[i].Start();
            }

            // Create consumers
            Consumer[] consumers = new Consumer[3];
            for (int i = 0; i < consumers.Length; i++)
            {
                consumers[i] = new Consumer(sharedLocation, i + 1);
                consumers[i].Start();
            }
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
