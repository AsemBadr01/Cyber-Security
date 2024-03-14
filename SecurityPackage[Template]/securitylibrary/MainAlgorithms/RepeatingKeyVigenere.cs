using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
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

            int key_cutting_index = 0;
            int repeated_key = 0;
            bool sequence;
            for (int i = 1; i < result.Length; i++)
            {
                repeated_key = i;
                sequence = true;

                if (result[i] == result[0])
                {

                    for (int j = 0; j < i; j++, repeated_key++)
                    {
                        if (result[repeated_key] != result[j])
                        {
                            sequence = false;
                            break;
                        }
                    }
                    if (sequence)
                    {
                        key_cutting_index = i;
                        break;
                    }
                }

            }

            string res = result.Remove(key_cutting_index);
            res = res.ToLower();

            return res;
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
            int num = cipherText.Length / key.Length;
            for (int i = 0; i < cipherText.Length; i++)
            {
                result_int[i] = (cipherText[i] - 'A' - key[i % key.Length] - 'A') % 26;
                if (result_int[i] < 0)
                    result_int[i] = 26 + result_int[i];   
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
                
                key_stream += key[i % key.Length];
                
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

