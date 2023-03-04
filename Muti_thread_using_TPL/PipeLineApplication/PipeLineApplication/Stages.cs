using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace PipeLineApplication
{
    internal class Stages
    {
        public void FirstStage(BlockingCollection<char> output, String PipelineInputFile)
        {
            String DisplayData = "";
            try
            {
                foreach (char C in GetData(PipelineInputFile))
                {
                        //Displayed characters read in from the file.              
                    DisplayData = DisplayData + C.ToString();
                    // Add each character to the buffer for the next stage.
                    output.Add(C);
                }

            }
            finally
            {
                output.CompleteAdding();
                Debug.Print(string.Format("FirstStage at : {0}", DateTime.Now.ToString()));
            }
        }


        public void StageWorker(BlockingCollection<char> input, BlockingCollection<char> output, String PipelineEncryptFile)
        {
            String DisplayData = "";
            try
            {
                foreach (char C in input.GetConsumingEnumerable())
                {
                    //Encrypt each character.
                    char encrypted = Encrypt(C);
                    DisplayData = DisplayData + encrypted.ToString();
                    //Add characters to the buffer for the next stage.
                    output.Add(encrypted);
                }
                //write the encrypted string to the output file.
                using (StreamWriter outfile = new StreamWriter(PipelineEncryptFile))
                {
                    outfile.Write(DisplayData);
                }
            }
            finally
            {
                output.CompleteAdding();
                Debug.Print(string.Format("StageWorker at : {0}", DateTime.Now.ToString()));
            }
        }


        public void FinalStage(BlockingCollection<char> input, String PipelineResultsFile)
        {
            String OutputString = "";
            String DisplayData = "";
            //Read the encrypted characters from the buffer, decrypt them, and display them.
            foreach (char C in input.GetConsumingEnumerable())
            {
                //Decrypt the data.
                char decrypted = Decrypt(C);
                //Display the decrypted data.
                DisplayData = DisplayData + decrypted.ToString();
                //Add to the output string.
                OutputString += decrypted.ToString();
            }
            //write the decrypted string to the output file.
            using (StreamWriter outfile = new StreamWriter(PipelineResultsFile))
            {
                outfile.Write(OutputString);
            }

            Debug.Print(string.Format("FinalStage at : {0}", DateTime.Now.ToString()));
        }

        public List<char> GetData(String PipelineInputFile)
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

        public char Encrypt(char C)
        {
            //Take the character, convert to an int, add 1, then convert back to a character.
            int i = (int)C;
            i = i + 1;
            C = Convert.ToChar(i);
            return C;
        }

        public char Decrypt(char C)
        {
            int i = (int)C;
            i = i - 1;
            C = Convert.ToChar(i);
            return C;
        }







    }

}
