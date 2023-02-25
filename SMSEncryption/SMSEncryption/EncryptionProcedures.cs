using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMSEncryption
{
    internal class EncryptionProcedures
    {
        public static string Encrypt(string psText)
        {
            string lsEncryptedText;
            string lsEncryptedTextWithFinalXOR;
            // A Random number generator

            Random loRandom = new Random();
            // The char position being encrypted
            int i;
            char loRandomChar;
            // Debug
            // Show the original text in the Immediate Window
            System.Diagnostics.Debug.Print("Original text:" + psText);
            lsEncryptedText = "";
            for (i = 0; i <= (psText.Length - 1); i++)
            {
                loRandomChar = (char)(loRandom.Next(65535));
                // Current char XOR random generated char
                // Debug
                // Show the random char code (in numbers) generated in the Immediate Window
                System.Diagnostics.Debug.Print("Random char generated: " + ((int)loRandomChar).ToString());
                lsEncryptedText += ((char)(psText[i] ^ loRandomChar)).ToString();
                // Random generated char XOR 65535 - i
                // It is saved because we need it later for the decryption process
                lsEncryptedText += ((char)(loRandomChar ^ (65535 - i))).ToString();
                // Another random generated char but just to add  garbage to confuse the hackers
                loRandomChar = (char)(loRandom.Next(65535));
                lsEncryptedText += loRandomChar.ToString();

                // Debug
                // Show how the encrypted text is being generated in the Immediate Window
                System.Diagnostics.Debug.Print("Partial encryption result char number: " + i.ToString() + ": " + lsEncryptedText);
            }
            lsEncryptedTextWithFinalXOR = "";
            // Now, every character XOR 125
            for (i = 0; i <= (lsEncryptedText.Length - 1); i++)
            {
                lsEncryptedTextWithFinalXOR += ((char)
               (lsEncryptedText[i] ^ 125)).ToString();
            }

            // Debug
            // Show how the encrypted text is being generated in the Immediate Window
            System.Diagnostics.Debug.Print("Final encryption result with XOR: " + lsEncryptedTextWithFinalXOR);
            return lsEncryptedTextWithFinalXOR;
        }

        public static string Decrypt(string psText)
        {
            // The decrypted text to return
            string lsDecryptedText;
            // The char position being decrypted
            int i;
            // The random char
            char loRandomChar;
            lsDecryptedText = "";
            for (i = 0; i <= (psText.Length - 1); i += 3)
            {
                // Retrieve the previously random generated char XOR 125 XOR 65535 - i(but previous i)

               loRandomChar = (char)(psText[i + 1] ^ 125 ^ (65535- (i / 3)));
                // Char XOR random generated char
               lsDecryptedText += ((char)(psText[i] ^ 125 ^ loRandomChar)).ToString();
            }
            return lsDecryptedText;
        }



    }
 }
