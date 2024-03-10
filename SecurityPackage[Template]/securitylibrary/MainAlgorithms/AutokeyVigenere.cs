using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {

            plainText = plainText.ToUpper();
            cipherText = cipherText.ToUpper();
            Dictionary<int, char> numtoletter = new Dictionary<int, char>();

            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                numtoletter.Add(letter - 'A', letter);
            }
            int[] result_int = new int[cipherText.Length];
            for (int i = 0; i < cipherText.Length; i++)
            {
                result_int[i] = (cipherText[i] - 'A' - plainText[i] - 'A') % 26;
                if (result_int[i] < 0)
                    result_int[i] = 26 + result_int[i];
            }
            string result = "";

            for (int i = 0; i < result_int.Length; i++)
            {
                result += numtoletter[result_int[i]];
            }
            var le = plainText.Substring(0, 2);
            var res = result.Split(le.ToCharArray());
            return res[0];
        }

        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToUpper();
            key = key.ToUpper();
            Dictionary<int, char> numtoletter = new Dictionary<int, char>();

            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                numtoletter.Add(letter - 'A', letter);
            }

            int[] result_int = new int[cipherText.Length];
            int j = 0;
            for (int i = 0; i < cipherText.Length; i++)
            {
                if (i < key.Length)
                {
                    result_int[i] = (cipherText[i] - 'A' - key[i] - 'A') % 26;
                    if (result_int[i] < 0)
                        result_int[i] = 26 + result_int[i];
                }
                else
                {
                    result_int[i] = (cipherText[i] - 'A' - result_int[j]) % 26;
                    if (result_int[i] < 0)
                        result_int[i] = 26 + result_int[i];
                    j++;
                }
            }
            string result = "";
            for (int i = 0; i < result_int.Length; i++)
            {
                result += numtoletter[result_int[i]];
            }
            return result.ToLower();

        }

        public string Encrypt(string plainText, string key)
        {

            plainText = plainText.ToUpper();
            Dictionary<int, char> numtoletter = new Dictionary<int, char>();

            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                numtoletter.Add(letter - 'A', letter);
            }
            string key_stream = "";
            for (int i = 0; i < plainText.Length; i++)
            {
                if (i < key.Length)
                {
                    key_stream += key[i];
                }
                else
                {
                    key_stream += plainText[i - key.Length];
                }
            }
            key_stream = key_stream.ToUpper();
            int[] result_sum = new int[plainText.Length];
            for (int i = 0; i < key_stream.Length; i++)
            {
                result_sum[i] = ((plainText[i] - 'A') + (key_stream[i] - 'A')) % 26;
                Console.WriteLine(result_sum[i]);
            }
            string result = "";
            for (int i = 0; i < result_sum.Length; i++)
            {
                result += numtoletter[result_sum[i]];
            }
            return result;
        }
    }
}
