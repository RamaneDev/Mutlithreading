using System;
using System.Threading;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = 0; // Results output
            Cell cell = new Cell();
            Producer producer = new Producer(cell, 5);
            Consumer consumer = new Consumer(cell, 5);
            Thread producerThread = new Thread(new ThreadStart(producer.ThreadRun));
            Thread consumerThread = new Thread(new ThreadStart(consumer.ThreadRun));
            try
            {
                producerThread.Start();
                consumerThread.Start();
                // Join both threads.
                producerThread.Join();
                consumerThread.Join();
            }
            catch (ThreadStateException e)
            {
                System.Diagnostics.Debug.WriteLine(e); // Output text of exception.
                result = 1; // Set result to indicate an error.
            }
            catch (ThreadInterruptedException e)
            {
                System.Diagnostics.Debug.WriteLine(e); // Output text noting an interruption.
                result = 1; // Set result to indicate an error.
            }
            Environment.ExitCode = result;
        }
    }
}

