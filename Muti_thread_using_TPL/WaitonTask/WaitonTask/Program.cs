using System;
using System.Threading;
using System.Threading.Tasks;

namespace WaitonTask
{
    internal class Program    {

        static Random ran = new Random();
        static void Main(string[] args)
        {
            // Wait on a single task with no timeout.
            Task taskA = Task.Factory.StartNew(() => Worker(10000));
            taskA.Wait(); // will wait until the end of execution of taskA.
            Console.WriteLine("Task A Finished.");
            
            // Wait on a single task with a timeout.
            Task taskB = Task.Factory.StartNew(() => Worker(2000000));

            Task<double> taskC = Task<double>.Factory.StartNew(() => Worker1());
            Console.WriteLine("TaskC finished = result is {0}.", taskC.Result); // here the task return a value, so we do not need to wait()
                                                                                // for this task the .Net will wait automatically by calling task.Result
                                                                                // but for the tasks that does not return value, wa must wait for them explicitly

            taskB.Wait(2000); //Wait for 2 seconds and Go on -> taskB will always be running
            if (taskB.IsCompleted)
                Console.WriteLine("Task B Finished.");
            else
                Console.WriteLine("Timed out without Task B finishing.");   // <--- this line will be reached in case of waiting 2 seconds in line 20 
            Console.ReadLine();
        }

        static void Worker(int waitTime)
        {
            Thread.Sleep(waitTime);
        }

        static double Worker1()
        {
            int i = ran.Next(1000000);
            Thread.SpinWait(i);
            return i;
        }

    }
}