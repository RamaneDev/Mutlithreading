using System;
using System.IO;

namespace FileReadAsync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] FileData = new byte[1000];
            string path = "C:\\Users\\mesri\\OneDrive\\Documents\\Mutlithreading\\Muti_thread_using_TPL\\FileReadAsync\\FileReadAsync\\data\\text.txt";
            FileStream FS = new FileStream(path, FileMode.Open, 
                                                 FileAccess.Read, 
                                                 FileShare.Read,
                                                 1024,
                                                 FileOptions.Asynchronous);
            
            Console.WriteLine("To start async read press return.");
            Console.ReadLine();
            
            IAsyncResult result = FS.BeginRead(FileData, 0, FileData.Length, null, null);
            
            // Work being done while we wait on the async //read.
            Console.WriteLine("\r\n");
            Console.WriteLine("Doing Some other work here. \r\n");
            Console.WriteLine("\r\n");
            
            //Calling EndRead will block the main thread //until the async work has finished.
            int num = FS.EndRead(result);
            FS.Close();

            Console.WriteLine("Read {0} bytes from the file. \r\n", num);
            Console.WriteLine("Is the async read completed - {0}. \r\n", result.IsCompleted.ToString());
            Console.WriteLine(BitConverter.ToString(FileData));
            Console.ReadLine();

        }
    }
}