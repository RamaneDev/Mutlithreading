using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SMSEncryption
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // The thread
        private Thread proThreadEncryption;
        
        // The string list with SMS messages to encrypt (input)
        private List<string> prlsSMSToEncrypt;
        
        // The string list with SMS messages encrypted (output)
        private List<string> prlsEncryptedSMS;

        private BackgroundWorker bakShowEncryptedStrings = new BackgroundWorker();

        // The number of the last encrypted string
        private int priLastEncryptedString;
        
        // The number of the last encrypted string shown in the UI
        private int priLastEncryptedStringShown;
        
        // The number of the previous last encrypted string shown in the UI
        private int priOldLastEncryptedStringShown;

        public MainWindow()
        {
            bakShowEncryptedStrings.DoWork += bakShowEncryptedStrings_DoWork;
            bakShowEncryptedStrings.ProgressChanged += bakShowEncryptedStrings_ProgressChanged;
            bakShowEncryptedStrings.WorkerReportsProgress = true;
            InitializeComponent();
        }

        private void bakShowEncryptedStrings_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            // The iteration
            int i;
            // Show the number of SMS messages encrypted by the concurrent proThreadEncryption thread.
            lblNumberOfSMSEncrypted.Text = priLastEncryptedString.ToString();
            // Append each new string, from priOldLastEncryptedStringShown to the received parameter in e.ProgressPercentage - 1

            for (i = priOldLastEncryptedStringShown; i < (int)e.ProgressPercentage; i++)
            {
                // Append the string to the txtEncryptedSMS TextBox
                txtEncryptedSMS.AppendText(prlsEncryptedSMS[i] + Environment.NewLine);
            }
            // Update the old last encrypted string shown
            priOldLastEncryptedStringShown = priLastEncryptedStringShown;
        }

        private void bakShowEncryptedStrings_DoWork(object? sender, DoWorkEventArgs e)
        {
            // Initialize the last encrypted string shown
            priLastEncryptedStringShown = 0;
            // Initialize the last encrypted string shown before
            priOldLastEncryptedStringShown = 0;
            // The iteration
            int i;
            // The last encrypted string (saved locally to avoid changes in the middle of the iteration)
            int liLast;

            // Wait until proThreadEncryption begins
            while ((priLastEncryptedString < 1))
            {
                // Sleep the thread for 10 milliseconds)
                Thread.Sleep(10);
            }
            while (proThreadEncryption.IsAlive || (priLastEncryptedString > priLastEncryptedStringShown))
            {
                liLast = priLastEncryptedString;
                if (liLast != priLastEncryptedStringShown)
                {
                    ((BackgroundWorker)sender).ReportProgress(liLast);
                    priLastEncryptedStringShown = liLast;
                }
                // Sleep the thread for 1 second (1000 milliseconds)
                Thread.Sleep(1000);
            }
        }

        private void butTest_Click(object sender, RoutedEventArgs e)
        {
            // The encrypted text
            string lsEncryptedText;
            // For each line in txtOriginalSMS TextBox
            int lineCount = txtOriginalSMS.LineCount;
            for (int line = 0; line < lineCount; line++)
            {
                lsEncryptedText = EncryptionProcedures.Encrypt(txtOriginalSMS.GetLineText(line));
                // Append a line with the Encrypted text
                txtEncryptedSMS.AppendText(lsEncryptedText + Environment.NewLine);
                // Append a line with the Encrypted text decrypted to test everything is as expected
                txtEncryptedSMS.AppendText(EncryptionProcedures.Decrypt(lsEncryptedText) + Environment.NewLine);
            }
        }

        private void butRunInThread_Click(object sender, RoutedEventArgs e)
        {
            // Prepare everything the thread needs from the UI
            // For each line in txtOriginalSMS TextBox
            prlsSMSToEncrypt = new List<string>(txtOriginalSMS.LineCount);
            


            // Add the lines in txtOriginalSMS TextBox
            int lineCount = txtOriginalSMS.LineCount;
            for (int line = 0; line < lineCount; line++)
            {
                prlsSMSToEncrypt.Add(txtOriginalSMS.GetLineText(line));
            }
            
            // Create the new Thread and use the ThreadEncryptProcedure method            
            proThreadEncryption = new Thread(new ThreadStart(ThreadEncryptProcedure));

            // Start the BackgroundWorker with an asynchronous execution
            bakShowEncryptedStrings.RunWorkerAsync();

            // Start running the thread
            proThreadEncryption.Start();

            // Join the independent thread to this thread to wait until ThreadProc ends
            // proThreadEncryption.Join();  we will not wait for thread to get encrypted messages this work will be done by
            // the bakShowEncryptedStrings

            // When the thread finishes running this is the next line that is going to be executed
            // Copy the string List generated by the thread

            //foreach (string lsEncryptedText in prlsEncryptedSMS)   also the update of UI will be done by bakShowEncryptedStrings
            // through bakShowEncryptedStrings_ProgressChanged method
            //{
            //    // Append a line with the Encrypted text
            //    txtEncryptedSMS.AppendText(lsEncryptedText + Environment.NewLine);
            //}
        }


        private void ThreadEncryptProcedure()
        {

            priLastEncryptedString = 0;

            string lsEncryptedText;
            //Initialize the encrypted array to the size of the array to encrypt.
            
            prlsEncryptedSMS = new List<string>(prlsSMSToEncrypt.Count);
            // Line of text message to encrypt
            string lsText;
            // Iterate through each string in the prlsSMSToEncrypt  string
            
            for (int i = 0; i < prlsSMSToEncrypt.Count; i++)
            {
                lsText = prlsSMSToEncrypt[i];
                lsEncryptedText = EncryptionProcedures.Encrypt(lsText);

                // Add the encrypted string to the List of encrypted strings
                prlsEncryptedSMS.Add(lsEncryptedText);
                priLastEncryptedString++;
            }

            // Wait for 1 minute
            // Thread.Sleep(20000); in this period the main thread is blocked to join() thread running ThreadEncryptProcedure
            // so the UI will be not ready to use 
        }
    }
}
