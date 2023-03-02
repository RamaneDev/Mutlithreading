using System;
using System.Collections.Concurrent;
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

namespace ParallelMath1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] numbers;

        ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();

        long total = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateNumbers3(int i, ParallelLoopState  pls)
        {
            int j = numbers[i];
            try
            {
                for (int k = 1; k <= 10; k++)
                {
                    j *= k;
                    if (j > 5000000) throw new ArgumentException(String.Format("The value of text box {0} is {1}.", i + 1, j));
                }
            }
            catch (Exception e)
            {
                exceptions.Enqueue(e);
            }
            numbers[i] = j;
        }

        private void CalculateNumbers(int i)
        {
            int j = numbers[i];

            for (int k = 1; k <= 10; k++)
            {
                j *= k;
            }
            numbers[i] = j;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            numbers =new int[10];
            numbers[0] = Convert.ToInt32(tb1.Text);
            numbers[1] = Convert.ToInt32(tb2.Text);
            numbers[2] = Convert.ToInt32(tb3.Text);
            numbers[3] = Convert.ToInt32(tb4.Text);
            numbers[4] = Convert.ToInt32(tb5.Text);
            numbers[5] = Convert.ToInt32(tb6.Text);
            numbers[6] = Convert.ToInt32(tb7.Text);
            numbers[7] = Convert.ToInt32(tb8.Text);
            numbers[8] = Convert.ToInt32(tb9.Text);
            numbers[9] = Convert.ToInt32(tb10.Text);

            Func<int, ParallelLoopState, long, long> body = (i, loop, subtotal) =>
                                                                                    {
                                                                                        int j = numbers[i];
                                                                                        for (int k = 1; k <= 10; k++)
                                                                                        {
                                                                                            j *= k;
                                                                                        }
                                                                                        numbers[i] = j;
                                                                                        subtotal += j;
                                                                                        return subtotal;
                                                                                    };

            Action<long> localFinally = (finalResult) => Interlocked.Add(ref total, finalResult);

            try
            {
                //Parallel.For(0, 10, CalculateNumbers3);

                Parallel.For<long>(0, 10, () => 0, body, localFinally);

                tbSum.Text = total.ToString();

                if (exceptions.Count > 0) throw new AggregateException(exceptions);
            }
            catch (AggregateException ae)
            {
                // This is where you can choose which exceptions to handle. 
                foreach (var ex in ae.InnerExceptions)
                {
                    if (ex is ArgumentException)
                    {
                        tbMessages.Text += ex.Message;
                        tbMessages.Text += "\r\n";
                    }
                    else
                        throw ex;
                }
            }

            tb1.Text = numbers[0].ToString();
            tb2.Text = numbers[1].ToString();
            tb3.Text = numbers[2].ToString();
            tb4.Text = numbers[3].ToString();
            tb5.Text = numbers[4].ToString();
            tb6.Text = numbers[5].ToString();
            tb7.Text = numbers[6].ToString();
            tb8.Text = numbers[7].ToString();
            tb9.Text = numbers[8].ToString();
            tb10.Text = numbers[9].ToString();
        }

        private void CalculateNumbers2(int i, ParallelLoopState pls)
        {
            int j = numbers[i];
            if (i < 7)
            {
                for (int k = 1; k <= 10; k++)
                {
                    j *= k;
                }
                numbers[i] = j;
            }
            else
            {
                pls.Stop();
                return;
            }
        }
    }
}
