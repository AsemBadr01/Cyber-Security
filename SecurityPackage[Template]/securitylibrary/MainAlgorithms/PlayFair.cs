using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            string alpha = "abcdefghiklmnopqrstuvwxyz";
            string unique = string.Join("", key.Distinct());


            char[,] array = new char[5, 5];
            int x = 0, y = 0;
            for (int i = 0; i < unique.Length; i++)
            {
                if (x == 5)
                {
                    x = 0;
                    y++;
                }
                array[y, x] = char.ToUpper(unique[i]);
                x++;
            }

            
            if(unique.Length < 24) {
                foreach (char c in unique)
                {
                    alpha = alpha.Replace(c.ToString(), "");
                }
                for (int i = 0; i < alpha.Length; i++)
                {
                    if (x == 5)
                    {
                        x = 0;
                        y++;
                    }
                    array[y, x] = char.ToUpper(alpha[i]);
                    x++;
                }
            }


            cipherText = cipherText.Replace(" ", "");

            string str = "";
            for (int i = 0; i <= cipherText.Length - 1; i++)
            {
                if (i < cipherText.Length - 2 && cipherText[i] == cipherText[i + 1])
                {
                    str += cipherText[i] + "x";
                }
                else
                {
                    str += cipherText[i];
                }
            }
            string Resultant = "";
            if (str.Length % 2 != 0) str += "x";
            for (int i = 0; i <= str.Length - 1; i += 2)
            {
                string twoCharacters = str.Substring(i, 2);
                int x1 = 0; int y1 = 0;
                int x2 = 0; int y2 = 0;
                string res = "";
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if (array[j, k] == char.ToUpper(twoCharacters[0]))
                        {
                            x1 = j; y1 = k;
                        }
                        if (array[j, k] == char.ToUpper(twoCharacters[1]))
                        {
                            x2 = j; y2 = k;
                        }
                    }
                }
                if (x1 == x2 && y1 != 0 && y2 != 0)
                {
                    res += array[x1, y1 - 1];
                    res += array[x2, y2 - 1];
                }
                else if (y1 == y2 && x1 != 0 && x2 != 0)
                {
                    res += array[x1 - 1, y1];
                    res += array[x2 - 1, y2];
                }
                else if (y1 != y2 && x1 != x2)
                {
                    res += array[x1, y2];
                    res += array[x2, y1];
                }
                else if (y1 == y2 && x1 == 0 && x2 != 0)
                {

                    res += array[4, y1];
                    res += array[x2 - 1, y2];
                }
                else if (y1 == y2 && x1 != 0 && x2 == 0)
                {

                    res += array[x1 - 1, y1];
                    res += array[4, y2];
                }
                else if (x1 == x2 && y1 == 0 && y2 != 0)
                {
                    res += array[x1, 4];
                    res += array[x2, y2 - 1];
                }
                else if (x1 == x2 && y1 != 0 && y2 == 0)
                {
                    res += array[x1, y1 - 1];
                    res += array[x2, 4];
                }


                Resultant += res;
            }
            if (Resultant[Resultant.Length - 1] == 'X')
            {
                Resultant = Resultant.Remove(Resultant.Length - 1);
            }
            return Resultant;
        }

        public string Encrypt(string plainText, string key)
        {
            string alpha = "abcdefghiklmnopqrstuvwxyz";

            string unique = string.Join("", key.Distinct());


            char[,] array = new char[5, 5];
            int x = 0, y = 0;
            for (int i = 0; i < unique.Length; i++)
            {
                if (x == 5)
                {
                    x = 0;
                    y++;
                }
                array[y, x] = char.ToUpper(unique[i]);
                x++;
            }

            foreach (char c in unique)
            {
                alpha = alpha.Replace(c.ToString(), "");
            }
            for (int i = 0; i < alpha.Length; i++)
            {
                if (x == 5)
                {
                    x = 0;
                    y++;
                }
                array[y, x] = char.ToUpper(alpha[i]);
                x++;
            }


            plainText = plainText.Replace(" ", "");

            string str = "";
            for (int i = 0; i <= plainText.Length - 1; i++)
            {
                if (i < plainText.Length - 2 && plainText[i] == plainText[i + 1])
                {
                    str += plainText[i] + "x";
                }
                else
                {
                    str += plainText[i];
                }
            }
            string Resultant = "";
            if (str.Length % 2 != 0) str += "x";
            for (int i = 0; i <= str.Length - 1; i += 2)
            {
                string twoCharacters = str.Substring(i, 2);
                int x1 = 0; int y1 = 0;
                int x2 = 0; int y2 = 0;
                string res = "";
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if (array[j, k] == char.ToUpper(twoCharacters[0]))
                        {
                            x1 = j; y1 = k;
                        }
                        if (array[j, k] == char.ToUpper(twoCharacters[1]))
                        {
                            x2 = j; y2 = k;
                        }
                    }
                }
                if (x1 == x2 && y1 != 4 && y2 != 4)
                {
                    res += array[x1, y1 + 1];
                    res += array[x2, y2 + 1];
                }
                else if (y1 == y2 && x1 != 4 && x2 != 4)
                {
                    res += array[x1 + 1, y1];
                    res += array[x2 + 1, y2];
                }
                else if (y1 != y2 && x1 != x2)
                {
                    res += array[x1, y2];
                    res += array[x2, y1];
                }
                else if (y1 == y2 && x1 == 4 && x2 != 4)
                {

                    res += array[0, y1];
                    res += array[x2 + 1, y2];
                }
                else if (y1 == y2 && x1 != 4 && x2 == 4)
                {

                    res += array[x1 + 1, y1];
                    res += array[0, y2];
                }
                else if (x1 == x2 && y1 == 4 && y2 != 4)
                {
                    res += array[x1, 0];
                    res += array[x2, y2 + 1];
                }
                else if (x1 == x2 && y1 != 4 && y2 == 4)
                {
                    res += array[x1, y1 + 1];
                    res += array[x2, 0];
                }


                Resultant += res;
            }

            return Resultant;
        }
    }
}