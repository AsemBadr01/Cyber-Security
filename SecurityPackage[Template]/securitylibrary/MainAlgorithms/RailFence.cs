using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            int key = 0;
            plainText = plainText.Replace(" ", "");
            string result = "";
            for (int x = 2; x < cipherText.Length; x++)
            {
                int y;
                if (cipherText.Length % x == 0)
                    y = cipherText.Length / x;
                else
                    y = cipherText.Length / x + 1;
                char[,] array = new char[x, y];
                int j = 0;
                int k = 0;
                for (int i = 0; i < plainText.Length; i++)
                {

                    array[j, k] = plainText[i];
                    j++;
                    if (j == x)
                    {
                        j = 0;
                        k++;
                    }

                }
                int c = 0;
                for (int i = 0; i < x; i++)
                {
                    for (int h = 0; h < y; h++)
                    {
                        if (array[i, h] != '\0')
                        {
                            result += array[i, h];
                        }

                    }
                }
                if (cipherText.ToLower() == result.ToLower())
                {
                    key = x;
                    break;
                }
                result = "";
            }
            return key;

        }
        public string Decrypt(string cipherText, int key)
        {
            
            Console.WriteLine(cipherText);
            string res = "";
            int step;
            if (cipherText.Length % key == 0)
                step = cipherText.Length / key;
            else
                step = cipherText.Length / key + 1;

            Console.WriteLine(step);
            for (int i = 0; i < step; i++)
            {   
                for (int j = i; j < cipherText.Length; j += step)
                {
                    res += cipherText[j];
                }
            }
            res = res.ToLower();
            return res;
        }

        public string Encrypt(string plainText, int key)
        {
            plainText = plainText.Replace(" ", "");
            int y;
            if (plainText.Length % key == 0)
                y = plainText.Length / key;
            else
                y = plainText.Length / key + 1;

            char[,] array = new char[key, y];
            int j = 0;
            int k = 0;
            for (int i = 0; i < plainText.Length; i++)
            {
                Console.Write(j);
                Console.WriteLine(k);
                array[j, k] = plainText[i];
                j++;
                if (j == key)
                {
                    j = 0;
                    k++;
                }
            }
            string result = "";
            for (int i = 0; i < key; i++)
            {
                for (int h = 0; h < y; h++)
                {
                    result += array[i, h];
                }
            }
            return result;
        }
    }
}
