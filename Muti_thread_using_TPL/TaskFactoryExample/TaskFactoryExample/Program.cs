using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskFactoryExample
{
    internal class Program
    {
        static TaskFactory TF = new TaskFactory(TaskScheduler.Default);

        static void Main(string[] args)
        {
            List<Task> tasklist = new List<Task>();
            tasklist.Add(TF.StartNew(() => Worker("Task 1"), CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default));
            tasklist.Add(TF.StartNew(() => Worker("Task 2"), CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default));
            tasklist.Add(TF.StartNew(() => Worker("Task 3"), CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default));
            tasklist.Add(TF.StartNew(() => Worker("Task 4"), CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default));
            tasklist.Add(TF.StartNew(() => Worker("Task 5"), CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default));
            //wait for all tasks to complete.
            Task.WaitAll(tasklist.ToArray());
            //Wait for input before ending program.

            Console.ReadLine();
        }
        static void Worker(String taskName)
        {
            Console.WriteLine("This is Task - {0}", taskName);
        }
    }
    
}