using System;
using System.Collections.Generic;

namespace SecurityLibrary
{

    public class HillCipher : ICryptographicTechnique<string, string>, ICryptographicTechnique<List<int>, List<int>>
    {

        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {

            throw new NotImplementedException();
        }
        public string Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }

        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {

            throw new NotImplementedException();
        }
        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            int plaincount = 0, keycount = 0, sum = 0;
            int m = (int)Math.Sqrt(key.Count);
            List<int> result = new List<int>();


            while (plaincount < plainText.Count)
            {
                List<int> pt = plainText.GetRange(plaincount, m);
                while (keycount < key.Count)
                {
                    List<int> ky = key.GetRange(keycount, m);
                    for (int i = 0; i < m; i++)
                    {
                        sum += (pt[i] * ky[i]);
                    }
                    result.Add(sum % 26);
                    keycount += m;
                    sum = 0;
                }
                plaincount += m;
                keycount = 0;
            }

            return result;
        }
        public string Encrypt(string plainText, string key)
        {
            throw new NotImplementedException();
        }


        public List<int> Analyse3By3Key(List<int> plain3, List<int> cipher3)
        {

            throw new NotImplementedException();
        }

        public string Analyse3By3Key(string plain3, string cipher3)
        {
            throw new NotImplementedException();
        }



    }
}

