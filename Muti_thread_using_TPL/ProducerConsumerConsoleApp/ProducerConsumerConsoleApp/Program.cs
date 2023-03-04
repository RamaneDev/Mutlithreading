using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks.Dataflow;
using System;

namespace ProducerConsumerConsoleApp
{
    internal class Program
    {

        private static String PipelineEncryptFile = @"C:\Users\mesri\OneDrive\Documents\Mutlithreading\Muti_thread_using_TPL\ProducerConsumerConsoleApp\ProducerConsumerConsoleApp\data\EncryptText.txt";
        private static String PipelineInputFile = @"C:\Users\mesri\OneDrive\Documents\Mutlithreading\Muti_thread_using_TPL\ProducerConsumerConsoleApp\ProducerConsumerConsoleApp\data\text.txt";
        static void Main(string[] args)
        {
            // Create the buffer block object to use between the producer and consumer.
            BufferBlock<char> buffer = new BufferBlock<char>();
            
            // The consumer method runs asynchronously. Start it now.
            Task<int> consumer = Consumer(buffer);
            
            // Post source data to the dataflow block.
            Producer(buffer);

            // Wait for the consumer to process all data.
            consumer.Wait();
            
            // Print the count of characters from the input file.
            Console.WriteLine("Processed {0} bytes from input file.", consumer.Result);
            
            //Print out the input file to the console.
            Console.WriteLine("\r\n\r\n");
            Console.WriteLine("This is the input data file. \r\n");
            using (StreamReader inputfile = new StreamReader(PipelineInputFile))
            {
                while (inputfile.Peek() >= 0)
                {
                    Console.Write((char)inputfile.Read());
                }

            }

            //Print out the encrypted file to the console.
            Console.WriteLine("\r\n\r\n");
            Console.WriteLine("This is the encrypted data file. \r\n");
            using (StreamReader encryptfile = new StreamReader(PipelineEncryptFile))
            {
                while (encryptfile.Peek() >= 0)
                {
                    Console.Write((char)encryptfile.Read());
                }
            }

            //Wait before closing the application so we can see the results.
            Console.ReadLine();

        }


            // Our Producer method.
        static void Producer(ITargetBlock<char> Target)
        {
            String DisplayData = "";
            try
            {
                foreach (char C in GetData(PipelineInputFile))
                {
                    //Displayed characters read in from the file.
                    DisplayData = DisplayData + C.ToString();
                    // Add each character to the buffer for the next stage.
                    Target.Post(C);
                }
            }
            finally
            {
                Target.Complete();
            }
        }


        // This is our consumer method. IT runs asynchronously.
        static async Task<int> Consumer(ISourceBlock<char> Source)
        {
            String DisplayData = "";
            // Read from the source buffer until the source buffer has no
            // available output data. 
            while (await Source.OutputAvailableAsync())
            {
                char C = Source.Receive();
                //Encrypt each character.

                char encrypted = Encrypt(C);
                DisplayData = DisplayData + encrypted.ToString();
            }            
            
            //write the decrypted string to the output file.
            using (StreamWriter outfile = new StreamWriter(PipelineEncryptFile))
            {
                outfile.Write(DisplayData);
            }
            return DisplayData.Length;
        }


        public static List<char> GetData(String PipelineInputFile)
        {
            List<char> Data = new List<char>();
            //Get the Source data.
            using (StreamReader inputfile = new StreamReader(PipelineInputFile))
            {
                while (inputfile.Peek() >= 0)
                {
                    Data.Add((char)inputfile.Read());
                }
            }
            return Data;
        }


        public static char Encrypt(char C)
        {
            //Take the character, convert to an int, add 1, then convert back to a character.
            int i = (int)C;
            i = i + 1;
            C = Convert.ToChar(i);
            return C;
        }



    }

}