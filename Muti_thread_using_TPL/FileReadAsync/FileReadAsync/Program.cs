using System;
using System.IO;
using System.Text;
using System.Threading;

namespace FileReadAsync
{
    internal class Program
    {
        static byte[] buffer2;
        static void Main(string[] args)
        {
            
            buffer2 = new byte[5000];

            string path = "C:\\Users\\mesri\\OneDrive\\Documents\\Mutlithreading\\Muti_thread_using_TPL\\FileReadAsync\\FileReadAsync\\data\\text.txt";
            // Open a file for reading
            FileStream FS = new FileStream(path, FileMode.Open, FileAccess.Read);

            // Begin reading data asynchronously from the file
           // byte[] buffer = new byte[1024];
            Console.WriteLine("To start async read press return.");
            
            Console.ReadLine();
            IAsyncResult result = FS.BeginRead(buffer2, 0, buffer2.Length, AsyncCallbackMethod, FS);
            // Work being done while we wait on the async //read.
           
            Console.WriteLine("Doing Some other work here. \r\n");
       
            Console.ReadLine();
        }

        static void AsyncCallbackMethod(IAsyncResult ar)
        {
            byte[] buffer = new byte[5000];
            // Get the file stream object and end the asynchronous read operation
            FileStream FS = (FileStream)ar.AsyncState;
            int bytesRead = FS.EndRead(ar);

            // Make sure to close the FileStream.
            FS.Close();

            //Now, write out the results.
            Console.WriteLine("Read {0} bytes from the file. \r\n", bytesRead);
           
            Console.WriteLine("Is the async read completed - {0}. \r\n", ar.IsCompleted.ToString());
           
            Console.WriteLine("numbre of bytes read from stream : ", bytesRead);

            Console.WriteLine(BitConverter.ToString(buffer2));

            // Begin reading data asynchronously from the file again
            //buffer = new byte[1024];
            //fileStream.BeginRead(buffer, 0, buffer.Length, AsyncCallbackMethod, fileStream);   // we call recall if needed to read more bytes
        }
    }
}