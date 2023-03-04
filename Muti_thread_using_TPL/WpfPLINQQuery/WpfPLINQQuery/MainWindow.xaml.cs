using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfPLINQQuery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnMethod1_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<int> collection1 = Enumerable.Range(10, 10000);
            
            //Start the timer.
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
           
            //Method 1 - This uses a ForAll method and an empty 
            //delegate method.
            ParallelQuery<int> PQ1 = from num in collection1.AsParallel()
                                     where num % 5 == 0
                                     select num;
            
            PQ1.ForAll((i) => DoWork(i));   
            // Use a standard foreach loop and merge the results.
            foreach (int i in PQ1)
            {
                lb1.Items.Add(i);
            }

            //Stop the timer.
            sw1.Stop();
            tbTime1.Text = sw1.ElapsedMilliseconds.ToString();
        }

        private void btnMethod2_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<int> collection2 = Enumerable.Range(10, 10000);
            
            //Start the timer.
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            
            // Method 2 - Use a standard ToArray method to return 
            //the results.
            int[] PQ2 = (from num in collection2.AsParallel()
                         where num % 10 == 0
                         select num).ToArray();
            // Use a standard foreach loop and merge the results.
            foreach (int i in PQ2)
            {
                lb2.Items.Add(i);
            }
            //Stop the timer.
            sw2.Stop();
            tbTime2.Text = sw2.ElapsedMilliseconds.ToString();
        }

        private void btnMethod3_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<int> collection3 = Enumerable.Range(10, 10000);
            //Start the timer.
            Stopwatch sw3 = new Stopwatch();
            sw3.Start();

            // Method 3 - Use the LINQ standard method format.
            ParallelQuery<int> PQ3 = collection3.AsParallel().Where(n => n % 10 == 0).Select(n => n);
            // Use a standard foreach loop and merge the results.
            foreach (int i in PQ3)
            {
                lb3.Items.Add(i);
            }
            //Stop the timer.
            sw3.Stop();
            tbTime3.Text = sw3.ElapsedMilliseconds.ToString();
        }

        static void DoWork(int i)
        {
        }
    }
}
