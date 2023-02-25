using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;
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
using static System.Net.Mime.MediaTypeNames;

namespace BreakingCodeWithSingleMainThread
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly BackgroundWorker bakCodebreaker = new BackgroundWorker();

        private readonly BackgroundWorker bakCodebreaker2 = new BackgroundWorker();

        private readonly BackgroundWorker bakCodebreaker3 = new BackgroundWorker();

        private readonly BackgroundWorker bakCodebreaker4 = new BackgroundWorker();
        // The simulated code to be broken
        private string Code;

        // The list of Labels of the characters to be broken.
        private List<TextBlock> OutputCharLabels;

        // The list of ProgressBar controls that show the 
        // progress of the character being decoded
        private List<ProgressBar> prloProgressChar;

        public MainWindow()
        {
            bakCodebreaker.DoWork += DoWorkProcedure;
            bakCodebreaker.ProgressChanged += ProgressChangedProcedure;
            bakCodebreaker.RunWorkerCompleted += RunWorkerCompletedProcedure;

            bakCodebreaker2.DoWork += DoWorkProcedure;
            bakCodebreaker2.ProgressChanged += ProgressChangedProcedure;
            bakCodebreaker2.RunWorkerCompleted += RunWorkerCompletedProcedure;

            bakCodebreaker3.DoWork += DoWorkProcedure;
            bakCodebreaker3.ProgressChanged += ProgressChangedProcedure;
            bakCodebreaker3.RunWorkerCompleted += RunWorkerCompletedProcedure;

            bakCodebreaker4.DoWork += DoWorkProcedure;
            bakCodebreaker4.ProgressChanged += ProgressChangedProcedure;          
            bakCodebreaker4.RunWorkerCompleted += RunWorkerCompletedProcedure;







            bakCodebreaker.WorkerReportsProgress = true;
            bakCodebreaker.WorkerSupportsCancellation = true;

            bakCodebreaker2.WorkerReportsProgress = true;
            bakCodebreaker2.WorkerSupportsCancellation = true;

            bakCodebreaker3.WorkerReportsProgress = true;
            bakCodebreaker3.WorkerSupportsCancellation = true;

            bakCodebreaker4.WorkerReportsProgress = true;
            bakCodebreaker4.WorkerSupportsCancellation = true;


            InitializeComponent();

            // Create a new list of ProgressBar controls that show 
            // the progress of each character of the code being 
            // broken
            prloProgressChar = new List<ProgressBar>(4);
            // Add the ProgressBar controls to the list
            prloProgressChar.Add(pgbProgressChar1);
            prloProgressChar.Add(pgbProgressChar2);
            prloProgressChar.Add(pgbProgressChar3);
            prloProgressChar.Add(pgbProgressChar4);

            // Generate a random code to be broken
            SimulateCodeGeneration();

            // Create a new list of Label controls that show the characters of the code being broken.
            OutputCharLabels = new List<TextBlock>(4);

            // Add the Label controls to the List
            OutputCharLabels.Add(txtOutput1);
            OutputCharLabels.Add(txtOutput2);
            OutputCharLabels.Add(txtOutput3);
            OutputCharLabels.Add(txtOutput4);

            // desable stop button
            btnStop.IsEnabled = false;

            // Hide the fishes game and show the CodeBreaker
            showCodeBreaker();

        }


        private void ProgressChangedProcedure(object sender, ProgressChangedEventArgs e)
        {
            // This variable will hold a CodeBreakerProgress 
            // instance
            CodeBreakerProgress loCodeBreakerProgress = (CodeBreakerProgress)e.UserState;
            // Update the corresponding ProgressBar with the percentage received as a parameter
            prloProgressChar[loCodeBreakerProgress.CharNumber].Value = loCodeBreakerProgress.PercentageCompleted;
            // Update the corresponding Label with the character being  processed
            OutputCharLabels[loCodeBreakerProgress.CharNumber].Text = ((char)loCodeBreakerProgress.CharCode).ToString();
        }

        private void SimulateCodeGeneration()
        {
            // A Random number generator.
            Random loRandom = new Random();
            // The char position being generated
            int i;
            Code = "";
            for (i = 0; i <= 4; i++)
            {
                // Generate a Random Unicode char for each of 
                //the 4 positions
                Code += (char)(loRandom.Next(65535));
            }
        }

        

        private void setFishesVisibility(Visibility pbValue)
        {
            // Change the visibility of the controls 
            //related to the fishes game.
            imgFish1.Visibility = pbValue;
            imgFish2.Visibility = pbValue;
            imgFish3.Visibility = pbValue;
            txtFishGame.Visibility = pbValue;
            btnGameOver.Visibility = pbValue;
        }


        private void setCodeBreakerVisibility(Visibility pbValue)
        {
            // Change the visibility of the controls related to the CodeBreaking procedure.
            imgSkull.Visibility = pbValue;
            imgAgent.Visibility = pbValue;
            txtCodeBreaker.Visibility = pbValue;
            txtNumber1.Visibility = pbValue;
            txtNumber2.Visibility = pbValue;
            txtNumber3.Visibility = pbValue;
            txtNumber4.Visibility = pbValue;
            txtOutput1.Visibility = pbValue;
            txtOutput2.Visibility = pbValue;
            txtOutput3.Visibility = pbValue;
            txtOutput4.Visibility = pbValue;
            btnStart.Visibility = pbValue;
            btnHide.Visibility = pbValue;
            pgbProgressChar1.Visibility = pbValue;
            pgbProgressChar2.Visibility = pbValue;
            pgbProgressChar3.Visibility = pbValue;
            pgbProgressChar4.Visibility = pbValue;
            btnStop.Visibility = pbValue;
        }

        private void showFishes()
        {
            // Hide all the controls related to the code 
            // breaking procedure.
            setCodeBreakerVisibility(Visibility.Hidden);
            // Change the window title
            this.Title = "Fishing game for Windows 1.0";
            // Make the fishes visible
            setFishesVisibility(Visibility.Visible);
        }

        private void showCodeBreaker()
        {
            // Hide all the controls related to the fishes 
            // game
            setFishesVisibility(System.Windows.Visibility.Hidden);
            // Change the window title
            this.Title = "CodeBreaker Application";
            // Make the code breaker controls visible
            setCodeBreakerVisibility(System.Windows.Visibility.
           Visible);
        }

        private bool checkCodeChar(char pcChar, int piCharNumber)
        {
            // Returns a bool value indicating whether the piCharNumber position of the code is the pcChar received.
            return (Code[piCharNumber] == pcChar);
        }

        private void btnGameOver_Click(object sender, RoutedEventArgs e)
        {
            showCodeBreaker();
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            showFishes();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

            // Start running the code programmed in each 
            // BackgroundWorker DoWork event handler in a new 
            // independent thread and return control to the 
            // application's main thread
            // First, create the CodeBreakerParameters for each 
            // BackgroundWorker and set its parameters
            CodeBreakerParameters loParameters1 = new CodeBreakerParameters();
            CodeBreakerParameters loParameters2 = new CodeBreakerParameters();
            CodeBreakerParameters loParameters3 = new CodeBreakerParameters();
            CodeBreakerParameters loParameters4 = new CodeBreakerParameters();

            loParameters1.MaxUnicodeCharCode = 32000;
            loParameters1.FirstCharNumber = 0;
            loParameters1.LastCharNumber = 0;

            loParameters2.MaxUnicodeCharCode = 32000;
            loParameters2.FirstCharNumber = 1;
            loParameters2.LastCharNumber = 1;

            loParameters3.MaxUnicodeCharCode = 32000;
            loParameters3.FirstCharNumber = 2;
            loParameters3.LastCharNumber = 2;

            loParameters4.MaxUnicodeCharCode = 32000;
            loParameters4.FirstCharNumber = 3;
            loParameters4.LastCharNumber = 3;

            bakCodebreaker.RunWorkerAsync(loParameters1);
            bakCodebreaker2.RunWorkerAsync(loParameters2);
            bakCodebreaker3.RunWorkerAsync(loParameters3);
            bakCodebreaker4.RunWorkerAsync(loParameters4);

            // Start running the code programmed in 
            // BackgroundWorker DoWork event handler
            // in a new independent thread and return control to 
            // the application's main thread

            // Disable the Start button
            btnStart.IsEnabled = false;
            // Enable the Stop button
            btnStop.IsEnabled = true;

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            // Disable the Stop button
            btnStop.IsEnabled = false;
            // Enable the Start button
            btnStart.IsEnabled = true;

            //Call the CancelAsync method to cancel the 
            // process.
            bakCodebreaker.CancelAsync();
            bakCodebreaker2.CancelAsync();
            bakCodebreaker3.CancelAsync();
            bakCodebreaker4.CancelAsync();

        }


        private void DoWorkProcedure(object sender, DoWorkEventArgs e)
        {
            // This variable will hold the broken code
            string lsBrokenCode = "";
            CodeBreakerParameters loCodeBreakerParameters = (CodeBreakerParameters)e.Argument;

            // This code will break the simulated code.
            // This variable will hold a number to iterate from 1 to 65,535 - Unicode character set.
            int i;
            // This variable will hold a number to iterate from 0 to 3(the characters positions in the code to be broken).
            int liCharNumber;
            // This variable will hold a char generated from the number in i
            char lcChar;
            // This variable will hold the current Label control that shows the char position being decoded.

            //TextBlock loOutputCharCurrentLabel;           


            for (liCharNumber = loCodeBreakerParameters.FirstCharNumber; liCharNumber <= loCodeBreakerParameters.LastCharNumber; liCharNumber++)
            {
                // This variable will hold the last percentage of the iteration completed
                int liOldPercentageCompleted = 0;

                CodeBreakerProgress loCodeBreakerProgress = new CodeBreakerProgress();

                //loOutputCharCurrentLabel = OutputCharLabels[liCharNumber];
                // This loop will run 65,536 times
                for (i = 0; i <= 65535; i++)
                {
                    // to cancel breaker Thread if CancellationPending = true

                    if (((BackgroundWorker)sender).CancellationPending)
                    {
                        // The user requested to cancel the process
                        e.Cancel = true;
                        return;
                    }
                    // myChar holds a Unicode char
                    lcChar = (char)(i);
                    // loOutputCharCurrentLabel.Text = lcChar.ToString();
                    // This variable will hold a CodeBreakerProgress 
                    // instance


                    // The percentage completed is calculated and stored in 
                    // the PercentageCompleted property
                    loCodeBreakerProgress.PercentageCompleted = (int)((i * 100) / 65535);
                    loCodeBreakerProgress.CharNumber = liCharNumber;
                    loCodeBreakerProgress.CharCode = i;

                    if (loCodeBreakerProgress.PercentageCompleted > liOldPercentageCompleted)
                    {

                        // The progress is reported only when it changes with regard to the last one(liOldPercentageCompleted)
                        ((BackgroundWorker)sender).ReportProgress(loCodeBreakerProgress.PercentageCompleted, loCodeBreakerProgress);
                        // The old percentage completed is now the 
                        // percentage reported
                        liOldPercentageCompleted = loCodeBreakerProgress.PercentageCompleted;

                    }
                    System.Threading.Thread.Sleep(1);
                    if (checkCodeChar(lcChar, liCharNumber))
                    {
                        lsBrokenCode = lcChar.ToString();
                       // loCodeBreakerProgress.PercentageCompleted = 100;
                        // The progress is reported only when it changes with regard to the last one(liOldPercentageCompleted)
                        //((BackgroundWorker)sender).ReportProgress(loCodeBreakerProgress.PercentageCompleted, loCodeBreakerProgress);
                        break;
                    }
                }
            }

            // Create a new instance of the CodeBreakerResult class 
            // and set its properties' values
            CodeBreakerResult loResult = new CodeBreakerResult();
            loResult.FirstCharNumber = loCodeBreakerParameters.FirstCharNumber;
            loResult.LastCharNumber = loCodeBreakerParameters.LastCharNumber;
            loResult.BrokenCode = lsBrokenCode;
            // Return a CodeBreakerResult instance in the Result 
            // property
            e.Result = loResult;

            //MessageBox.Show("The code has been decoded successfully.", this.Title);
        }

        private void RunWorkerCompletedProcedure(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                // Obtain the CodeBreakerResult instance 
                // contained in the Result property of e 
                // parameter
                CodeBreakerResult loResult = (CodeBreakerResult)e.Result;
                int i;
                // Iterate through the parts of the result 
                // resolved by this BackgroundWorker

                for (i = loResult.FirstCharNumber; i <= loResult.LastCharNumber; i++)
                {
                    // The process has finishes, therefore the 
                    // ProgressBar control must show a 100%
                    prloProgressChar[i].Value = 100;
                    // Show the part of the broken code in the 
                    // label
                    OutputCharLabels[i].Text = loResult.BrokenCode[i - loResult.FirstCharNumber].ToString();
                }
            }
        }

      
    }
}