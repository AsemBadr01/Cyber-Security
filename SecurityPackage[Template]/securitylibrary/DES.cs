using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class DES : CryptographicTechnique
    {
        public override string Decrypt(string cipherText, string key)
        {
            List<int> binkey = hextobin(key);
            List<int> pc1key = PC1(binkey);
            List<List<int>> krounds = keyrounds(pc1key.GetRange(0, 28), pc1key.GetRange(28, 28));


            return " ";
        }

        public override string Encrypt(string plainText, string key)
        {
            throw new NotImplementedException();
        }

        List<int> hextobin(string key)
        {
            Dictionary<char, int> conv = new Dictionary<char, int>()
        {
            {'0',0 },
            {'1',1 },
            {'2',2 },
            {'3',3 },
            {'4',4 },
            {'5',5 },
            {'6',6 },
            {'7',7 },
            {'8',8 },
            {'9',9 },
            {'A',10 },
            {'B',11 },
            {'C',12 },
            {'D',13 },
            {'E',14 },
            {'F',15 },
        };
            Stack<int> stack = new Stack<int>();
            List<int> result = new List<int>();
            for (int i = 2; i < key.Length; i++)
            {
                char c = key[i];
                int val = conv[c];
                for (int j = 0; j < 4; j++)
                {
                    if (val % 2 == 0)
                    {
                        stack.Push(0);
                    }
                    else
                    {
                        stack.Push(1);
                    }
                    val = (val - (val % 2)) / 2;
                }
                for (int k = 0; k < 4; k++)
                {
                    result.Add(stack.Peek());
                    stack.Pop();
                }
                stack.Clear();

            }
            return result;
        }
        List<int> PC1(List<int> key)
        {
            List<int> pc1 = new List<int>{57,49,41,33,25,17,9,1,58,50,42,34,26,18,10,2,59,51,43,35,27,19,11,
            3,60,52,44,36,63,55,47,39,31,23,15,7,62,56,46,38,30,22,14,6,61,53,45,37,29,21,13,5,28,20,12,4 };
            List<int> result = new List<int>();

            for (int i = 0; i < pc1.Count; i++)
            {
                result.Add(key[pc1[i] - 1]);
            }
            return result;
        }
        List<int> PC2(List<int> key)
        {
            List<int> pc2 = new List<int>{14,17,11,24,1,5,3,28,15,6,21,10,23,19,12,4,26,8,16,7,27,20,13,
            2,41,52,31,37,47,55,30,40,51,45,33,48,44,49,39,56,34,53,46,42,50,36,29,32 };
            List<int> result = new List<int>();

            for (int i = 0; i < pc2.Count; i++)
            {
                result.Add(key[pc2[i] - 1]);
            }
            return result;
        }
        List<int> shift1(List<int> c)
        {
            List<int> result = c.GetRange(1, 27);
            result.Add(c[0]);
            return result;
        }
        List<int> shift2(List<int> c)
        {
            List<int> result = c.GetRange(2, 26);
            result.Add(c[0]);
            result.Add(c[1]);
            return result;
        }
        List<List<int>> keyrounds(List<int> c0, List<int> d0)
        {
            List<int> c = new List<int>();
            List<int> d = new List<int>();
            List<List<int>> result = new List<List<int>>();
            for (int i = 1; i < 17; i++)
            {
                if (i == 1)
                {
                    c = shift1(c0);
                    d = shift1(d0);

                }
                else
                {
                    if (i == 2 || i == 9 || i == 16)
                    {
                        c = shift1(c);
                        d = shift1(d);

                    }
                    else
                    {
                        c = shift2(c);
                        d = shift2(d);
                    }
                }

                List<int> key = c.Concat(d).ToList();

                key = PC2(key);
                result.Add(key);
            }
            return result;
        }
    }
}
