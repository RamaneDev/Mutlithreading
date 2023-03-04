using Microsoft.VisualBasic;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace PipeLineApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>    /// 

    public partial class MainWindow : Window
    {

        private static String PipelineResultsFile = @"C:\Users\mesri\OneDrive\Documents\Mutlithreading\Muti_thread_using_TPL\PipeLineApplication\PipeLineApplication\data\Resulttext.txt";
        private static String PipelineEncryptFile = @"C:\Users\mesri\OneDrive\Documents\Mutlithreading\Muti_thread_using_TPL\PipeLineApplication\PipeLineApplication\data\EncryptText.txt";
        private static String PipelineInputFile = @"C:\Users\mesri\OneDrive\Documents\Mutlithreading\Muti_thread_using_TPL\PipeLineApplication\PipeLineApplication\data\text.txt";
        private Stages Stage;

        public MainWindow()
        {
            InitializeComponent();

            //Create the Stage object and register the event listeners to update the UI as the stages work.
            Stage = new Stages();
        }

        private void butEncrypt_Click(object sender, RoutedEventArgs e)
        {
            //PipeLine Design Pattern
            //Create queues for input and output to stages.
            int size = 20;
            
            BlockingCollection<char> Buffer1 = new BlockingCollection<char>(size);
            BlockingCollection<char> Buffer2 = new BlockingCollection<char>(size);
            
            TaskFactory tasks = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);


            Task Stage1 = tasks.StartNew(() => Stage.FirstStage(Buffer1, PipelineInputFile));
            Task Stage2 = tasks.StartNew(() => Stage.StageWorker(Buffer1, Buffer2, PipelineEncryptFile));
            Task Stage3 = tasks.StartNew(() => Stage.FinalStage(Buffer2, PipelineResultsFile));


           Task.WaitAll(Stage2, Stage3);
            
            //Display the 3 files.
            using (StreamReader inputfile = new StreamReader(PipelineInputFile))
            {
                while (inputfile.Peek() >= 0)
                {
                    tbStage1.Text = tbStage1.Text + (char)inputfile.Read();
                }
            }
            
            using (StreamReader inputfile = new StreamReader(PipelineEncryptFile))
            {
                while (inputfile.Peek() >= 0)
                {
                    tbStage2.Text = tbStage2.Text + (char)inputfile.Read();
                }
            }
            
            using (StreamReader inputfile = new StreamReader(PipelineResultsFile))
            {
                while (inputfile.Peek() >= 0)
                {
                    tbStage3.Text = tbStage3.Text + (char)inputfile.Read();
                }
            }
        }
    }
}
