using System;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileReadAsync
{
    internal class Program
    {
        static byte[] buffer2;
        static string path = "C:\\Users\\mesri\\OneDrive\\Documents\\Mutlithreading\\Muti_thread_using_TPL\\FileReadAsync\\FileReadAsync\\data\\text.txt";
        static void Main(string[] args)
        {

            Console.WriteLine("The ID of the Main method: {0}. \r\n", Thread.CurrentThread.ManagedThreadId);

            //Wait on the user to begin the reading of the //file.
            Console.ReadLine();

            // Create task, start it, and wait for it to //finish.
            Task task = new Task(ProcessFileAsync);
            task.Start();
            task.Wait();

            //Wait for a return before exiting.
            Console.ReadLine();
        }

        static async void ProcessFileAsync()
        {
            // Write out the id of the thread of the task //that will call the async method to read the file.
            Console.WriteLine("The thread id of the ProcessFileAsync method: {0}. \r\n", Thread.CurrentThread.ManagedThreadId);

            // Start the HandleFile method.
            Task<String> task = ReadFileAsync(path);


            // Perform some other work.
            Console.WriteLine("Do some other work. \r\n");
            Console.WriteLine("Proceed with waiting on the read to complete. \r\n");


            Console.ReadLine();

            // Wait for the task to finish reading the //file.
            String results = await task;
            Console.WriteLine("Number of characters read are: {0}. \r\n", results.Length);


            Console.WriteLine("The file contents are: {0}. \r\n", results);
        }



        static async Task<String> ReadFileAsync(string file)
        {
            // Write out the id of the thread that is //performing the read.
            Console.WriteLine("The thread id of the ReadFileAsync method: {0}. \r\n", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Begin Reading file asynchronously. \r\n");

            // Read the specified file.
            String DataRead = "";
            using (StreamReader reader = new StreamReader(file))
            {
                string character = await reader.ReadToEndAsync();
                //Build string of data read.
                DataRead = DataRead + character;
                //Slow down the process.
                Thread.Sleep(10000);

            }

            Console.WriteLine("Done Reading File asynchronously. \r\n");
            return DataRead;
        }
    }




       









    

}