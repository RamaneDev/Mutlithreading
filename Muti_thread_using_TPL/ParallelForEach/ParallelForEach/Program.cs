using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelForEach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] localStrings = "I am doing a simple example of a Parallel foreach loop".Split(' ');
             Parallel.ForEach(localStrings, currentString =>
                                                          {
             Console.WriteLine("Current word is - {0}, and the current thread is - {1} ", currentString, Thread.CurrentThread.ManagedThreadId);
              }); 
             Console.ReadLine();
        }
    }
}