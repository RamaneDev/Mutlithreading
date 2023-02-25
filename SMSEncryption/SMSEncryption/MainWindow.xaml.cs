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

namespace SMSEncryption
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
    }
}
