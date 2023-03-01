using System;
using System.Collections.Generic;
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

namespace ParallelMath1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] numbers;

        public MainWindow()
        {
            InitializeComponent();
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
            
            Parallel.For(0, 10, CalculateNumbers);
            
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

       
    }
}
