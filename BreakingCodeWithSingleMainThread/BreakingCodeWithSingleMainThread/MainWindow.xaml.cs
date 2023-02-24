using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        // The simulated code to be broken
        private string Code;

        // The list of Labels of the characters to be broken.
        private List<TextBlock> OutputCharLabels;

        // The list of ProgressBar controls that show the 
        // progress of the character being decoded
        private List<ProgressBar> prloProgressChar;

        public MainWindow()
        {
            bakCodebreaker.DoWork += bakCodebreaker_DoWork;
            bakCodebreaker.ProgressChanged += bakCodebreaker_ProgressChanged;
            bakCodebreaker.WorkerReportsProgress = true;

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
            
            // Hide the fishes game and show the CodeBreaker
            showCodeBreaker();

        }

        private void bakCodebreaker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {

            // This variable will hold a CodeBreakerProgress instance
            CodeBreakerProgress loCodeBreakerProgress = (CodeBreakerProgress)e.UserState;
            // Update the corresponding ProgressBar with the percentage received in the as a parameter

             prloProgressChar[loCodeBreakerProgress.CharNumber].Value = loCodeBreakerProgress.PercentageCompleted;
            // Update the corresponding Label with the character being processed
             
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

        private void bakCodebreaker_DoWork(object sender, DoWorkEventArgs e)
        {

            // This code will break the simulated code.
            // This variable will hold a number to iterate from 1 to 65,535 - Unicode character set.
            int i;
            // This variable will hold a number to iterate from 0 to 3(the characters positions in the code to be broken).
            int liCharNumber;
            // This variable will hold a char generated from the number in i
            char lcChar;
            // This variable will hold the current Label control that shows the char position being decoded.

            //TextBlock loOutputCharCurrentLabel;           
           

            for (liCharNumber = 0; liCharNumber < 4; liCharNumber++)
            {
                // This variable will hold the last percentage of the iteration completed
                int liOldPercentageCompleted = 0;

                CodeBreakerProgress loCodeBreakerProgress = new CodeBreakerProgress();

                //loOutputCharCurrentLabel = OutputCharLabels[liCharNumber];
                // This loop will run 65,536 times
                for (i = 0; i <= 65535; i++)
                {
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
                            bakCodebreaker.ReportProgress(loCodeBreakerProgress.PercentageCompleted, loCodeBreakerProgress);
                            // The old percentage completed is now the 
                            // percentage reported
                            liOldPercentageCompleted = loCodeBreakerProgress.PercentageCompleted;                  
                           
                    }
                    System.Threading.Thread.Sleep(1);
                    if (checkCodeChar(lcChar, liCharNumber))
                    {
                        loCodeBreakerProgress.PercentageCompleted = 100;
                        // The progress is reported only when it changes with regard to the last one(liOldPercentageCompleted)
                        bakCodebreaker.ReportProgress(loCodeBreakerProgress.PercentageCompleted, loCodeBreakerProgress);
                        break;
                    }
                }
            }
            
           //MessageBox.Show("The code has been decoded successfully.", this.Title);

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
            // Start running the code programmed in 
            // BackgroundWorker DoWork event handler
            // in a new independent thread and return control to 
            // the application's main thread
            bakCodebreaker.RunWorkerAsync();
        }
    }
}
