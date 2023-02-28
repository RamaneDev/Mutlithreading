using System.Threading.Tasks;

namespace TaskExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            Parallel.Invoke(
                new Action(WriteNumbers),
                new Action(WriteWords),
                new Action(WriteColors)
            );
            Console.ReadLine();
        }

        static void WriteNumbers()
        {
            //Set thread name.
            Thread.CurrentThread.Name = "Thread 1";
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Thread name {0}, Number: {1}", Thread.CurrentThread.Name, i);
                Thread.Sleep(2000);
            }

        }


        static void WriteWords()
        {
            //Set thread name.
            Thread.CurrentThread.Name = "Thread 2";
            String localString = "This is an example for using tasks";
            String[] localWords = localString.Split(' ');
            foreach (String s in localWords)
            {
                Console.WriteLine("Thread name {0}, Word: {1}", Thread.CurrentThread.Name, s);
                Thread.Sleep(2000);
            }
        }

        static void WriteColors()
        {
            //Set thread name.
            Thread.CurrentThread.Name = "Thread 3";
            String[] localColors = {"red", "orange", "blue", "green","yellow", "white", "black"};
            foreach (String s in localColors)
            {
                Console.WriteLine("Thread name {0}, Colors: {1}", Thread.CurrentThread.Name, s);
                Thread.Sleep(2000);
            }
        }

    }
}