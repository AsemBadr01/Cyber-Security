using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            Dictionary<char, char> map = new Dictionary<char, char>();
            Dictionary<char, char> mapped = new Dictionary<char, char>();
            LinkedList<char> list = new LinkedList<char>(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' });
            StringBuilder x = new StringBuilder();
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            string y;
            for (int i = 0; i < plainText.Length; i++)
            {
                if (map.ContainsKey(plainText[i]))
                {
                    continue;
                }
                else
                {
                    map.Add(plainText[i], cipherText[i]);

                }
            }
            for (char i = 'a'; i <= 'z'; i++)
            {
                if (!map.ContainsKey(i))
                {
                    continue;
                }
                else
                {
                    mapped.Add(i, map[i]);
                    if (list.Find(map[i]) != null)
                    {
                        LinkedListNode<char> remove = list.Find(map[i]);
                        list.Remove(remove);
                    }
                }
            }

            for (char i = 'a'; i <= 'z'; i++)
            {
                if (!mapped.ContainsKey(i))
                {
                    x.Append(list.First.Value);
                    list.RemoveFirst();
                }
                else
                {
                    x.Append(mapped[i]);

                }
            }
            y = x.ToString();
            return y;
        }

        public string Decrypt(string cipherText, string key)
        {
            Dictionary<char, char> unmap = new Dictionary<char, char>();
            StringBuilder x = new StringBuilder();
            string y;
            int c = 0;
            cipherText = cipherText.ToLower();
            key = key.ToLower();
            for (char i = 'a'; i <= 'z'; i++)
            {
                unmap.Add(key[c], i);
                c++;
            }

            for (int i = 0; i < cipherText.Length; i++)
            {
                x.Append(unmap[cipherText[i]]);
            }
            y = x.ToString();
            return y;
        }

        public string Encrypt(string plainText, string key)
        {
            Dictionary<char, char> map = new Dictionary<char, char>();
            StringBuilder x = new StringBuilder();
            plainText = plainText.ToLower();
            key = key.ToLower();
            string y;
            int c = 0;
            for (char i = 'a'; i <= 'z'; i++)
            {
                map.Add(i, key[c]);
                c++;
            }

            for (int i = 0; i < plainText.Length; i++)
            {
                x.Append(map[plainText[i]]);
            }
            y = x.ToString();
            return y;
        }







        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	=
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        /// 

        public string AnalyseUsingCharFrequency(string cipher)
        {
            Queue<char> list = new Queue<char>(new[] { 'e', 't', 'a', 'o', 'i', 'n', 's', 'r', 'h', 'l', 'd', 'c', 'u', 'm', 'f', 'p', 'g', 'w', 'y', 'b', 'v', 'k', 'x', 'j', 'q', 'z' });
            Dictionary<char, int> map = new Dictionary<char, int>();
            Dictionary<char, char> mapped = new Dictionary<char, char>();
            StringBuilder z = new StringBuilder();
            cipher=cipher.ToLower();
            string y;
            for (int i = 0; i < cipher.Length; i++)
            {
                if (map.ContainsKey(cipher[i]))
                {
                    map[cipher[i]]++;
                }
                else
                {
                    map.Add(cipher[i], 1);
                }
            }
            var sortmap = map.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            foreach (var pair in sortmap)
            {
                mapped.Add(pair.Key, list.Dequeue());
            }

            for (int i = 0; i < cipher.Length; i++)
            {
                z.Append(mapped[cipher[i]]);
            }
            y = z.ToString();
            return y;
        }
    }
}