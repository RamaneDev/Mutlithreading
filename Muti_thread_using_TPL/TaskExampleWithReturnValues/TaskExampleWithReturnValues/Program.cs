using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskExampleWithReturnValues
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Create the 3 Tasks.
            Task<String> t1 = new Task<String>(() => WriteNumbers());
            Task<String> t2 = new Task<String>(() => WriteWords());
            Task<String> t3 = new Task<String>(() => WriteColors());
            //Run the 3 Tasks.
            t1.Start();
            t2.Start();
            t3.Start();
            Console.WriteLine(t1.Result);
            Console.WriteLine(t2.Result);
            Console.WriteLine(t3.Result);
            Console.ReadLine();
        }

        static String WriteNumbers()
        {
            //Set thread name.
            Thread.CurrentThread.Name = "Task 1";
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Thread name {0}, Number: {1}", Thread.CurrentThread.Name, i);
                Thread.Sleep(2000);
            }
            return String.Format("This Task has completed - {0}", Thread.CurrentThread.Name);
        }

        static String WriteWords()
        {
            //Set thread name.
            Thread.CurrentThread.Name = "Task 2";
            String localString = "This is an example for using tasks";
            String[] localWords = localString.Split(' ');
            foreach (String s in localWords)
            {
                Console.WriteLine("Thread name {0}, Word: {1}", Thread.CurrentThread.Name, s);
                Thread.Sleep(2000);
            }
            return String.Format("This Task has completed - {0}", Thread.CurrentThread.Name);
        }

        static String WriteColors()
        {
            //Set thread name.
            Thread.CurrentThread.Name = "Task 3";
            String[] localColors = { "red", "orange", "blue", "green", "yellow", "white", "black" };
            foreach (String s in localColors)
            {
                Console.WriteLine("Thread name {0}, Colors: {1}", Thread.CurrentThread.Name, s);
                Thread.Sleep(2000);
            }
            return String.Format("This Task has completed - {0}", Thread.CurrentThread.Name);
        }
    }



}

