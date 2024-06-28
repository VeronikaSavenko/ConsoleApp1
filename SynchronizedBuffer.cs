using System;
using System.Threading;
using System.Threading.Tasks;

public class SynchronizedBuffer : Buffer
{
    private int buf = -1;
    private bool isFull = false;
    private readonly object lockObject = new object();
    private readonly AutoResetEvent canWrite = new AutoResetEvent(true);
    private readonly AutoResetEvent canRead = new AutoResetEvent(false);

    public void Set(int value)
    {
        canWrite.WaitOne();
        lock (lockObject)
        {
            buf = value;
            isFull = true;
            Console.WriteLine(Thread.CurrentThread.Name + " is writing " + value);
            canRead.Set();
        }
    }

    public int Get()
    {
        int readVal = 0;
        canRead.WaitOne();
        lock (lockObject)
        {
            readVal = buf;
            isFull = false;
            Console.WriteLine(Thread.CurrentThread.Name + " is reading " + readVal);
            canWrite.Set();
        }
        return readVal;
    }
}
