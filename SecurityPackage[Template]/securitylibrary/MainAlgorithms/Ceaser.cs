using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        char[] arr = {'a','b','c','d','e','f','g','h','i','j','k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z' };

        public int search(char x)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == x)
                {
                    return i;
                }

            }
            return -1;
        }

        public string Encrypt(string plainText, int key)
        {
            int x;
            int index;
            plainText = plainText.ToLower();
            char[] cipher = new char[plainText.Length];
            for (int i = 0; i < plainText.Length; i++)
            {
                x = search(plainText[i]);
                index = (x + key) % 26;
                cipher[i] = arr[index];

            }
            string result ="";
            for (int i = 0;i<plainText.Length;i++)
            {
                result += cipher[i];
            }
            return result;
        }

        public string Decrypt(string cipherText, int key)
        {
            int x;
            int index;
            cipherText = cipherText.ToLower();
            char[] plain = new char[cipherText.Length];
            for (int i = 0; i < cipherText.Length; i++)
            {
                x = search(cipherText[i]);
                index = (x - key) % 26;
                 if(index < 0)
                {

                    index = 26+index;
                    
                }
                
                plain[i] = arr[index];

            }
            string result = "";
            for (int i = 0; i < cipherText.Length; i++)
            {
                result += plain[i];
            }
            return result;

        }

        public int Analyse(string plainText, string cipherText)
        {
            int result;
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            int x1 = search(plainText[0]);
            int x2 = search(cipherText[0]);
            result = x1 - x2;
            if (x1 > x2)
            {
                result = 26 - result;
            }
           
            
            if (result >= 0)
            {
                return result;
            }
            else
            {
                return result * -1;
            }

        }
    }
}
