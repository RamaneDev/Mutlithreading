using System;
using System.Collections.Generic;
using System.Linq;
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

namespace RandomCircles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static Random ran = new Random(); 

        public MainWindow()
        {
            InitializeComponent();
        }


        private String Worker1()
        {           
            int result = ran.Next(10000000);
            Thread.SpinWait(result);
            return String.Format("Random Number is {0} and Time is {1}.",result, DateTime.Now.Millisecond);
 }

        private void btnRandomAll_Click(object sender, RoutedEventArgs e)
        {
            // Wait for all tasks to finish.
            Task<String>[] tasks = new Task<String>[3];
            for (int i = 0; i < 3; i++)
            {
                tasks[i] = Task<String>.Factory.StartNew(() => Worker1());
            }
            Task.WaitAll(tasks);
            text1.Text = tasks[0].Result.ToString();
            text2.Text = tasks[1].Result.ToString();
            text3.Text = tasks[2].Result.ToString();
        }
    }
}
