using System;
using System.Threading;
using System.Threading.Tasks;

//public class SharedBufferTest
//{
//    public static void Main(string[] args)
//    {
//        Buffer sharedLocation = new SynchronizedBuffer();

//        // Create producers
//        Producer[] producers = new Producer[4];
//        for (int i = 0; i < producers.Length; i++)
//        {
//            producers[i] = new Producer(sharedLocation, i + 1);
//            producers[i].Start();
//        }

//        // Create consumers
//        Consumer[] consumers = new Consumer[4];
//        for (int i = 0; i < consumers.Length; i++)
//        {
//            consumers[i] = new Consumer(sharedLocation, i + 1);
//            consumers[i].Start();
//        }
//    }
//}