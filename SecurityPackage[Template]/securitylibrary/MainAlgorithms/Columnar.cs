using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        int Ceil(double number)
        {
            int integerPart = (int)number;
            if (number == integerPart)
                return integerPart;
            else
                return integerPart + 1;
        }



        static int search(char[] c, string str)
        {
            
            int key = 0;
            int L = c.Length;

            
                for (int i = 0; i < str.Length; i += L)
                {
                    if (key != 0 || str[i]=='x')
                    {
                        break;
                    }
                    for (int j = 0; j < L; j++)
                    {

                        if (c[j] == str[i + j])
                        {
                            if (j == L - 1)
                            {
                               
                                key = (i / L) + 1;
                            }
                        }

                        else
                        {
                            break;
                        }

                    }
                }
              
            return key;
        }

       

        public List<int> Analyse(string plainText, string cipherText)
        {
            List<int> key = new List<int>();
            int c = 0;   
            plainText=plainText.ToLower();
            cipherText=cipherText.ToLower();
            char c1 = cipherText[0];
            char c2 = cipherText[1];

            for (int i = 0; i < plainText.Length - 1; i++)
            {

                if (plainText[i] == plainText[i + 1])
                {

                    continue;
                }
                else
                {
                    if (c1 == plainText[i])
                    {

                        for (int j = i + 1; i < plainText.Length; j++)
                        {
                            c += 1;
                            if (c2 == plainText[j])
                            {

                                break;
                            }

                        }

                        break;
                    }

                }
            }

            double  x = (double)plainText.Length / c;             
            int rows = Ceil(x);
            char[,] matrix = new char[rows, c];

            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (k < plainText.Length)
                    {
                        matrix[i, j] = plainText[k];
                        k++;
                    }
                    else
                    {
                        matrix[i, j] = 'x';
                       // break;
                    }
                }
            }

            char[] ch = new char[rows];
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    ch[j] = matrix[j, i];
                }
                key.Add(search(ch, cipherText));
            }

            return key;
        }

        public string Decrypt(string cipherText, List<int> key)
        {
            string plainText = null;

            int columns = key.Count;
            double x = (double)cipherText.Length / columns;
            int rows = Ceil(x);
            char[,] matrix = new char[rows, columns];
            
            int k = 0;
            for (int j = 0; j < columns; j++)
            {
                int columnIndex = key.IndexOf(j + 1);
                for (int i = 0; i < rows; i++)
                {
                    if (k < cipherText.Length)
                    {
                        matrix[i, columnIndex] = cipherText[k];
                        k++;
                    }
                }
            }

            // Reconstruct the plaintext by reading the matrix row by row
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (matrix[i, j] != ' ') // Check for non-null character
                    {
                        plainText += matrix[i, j];
                    }
                }
            }
            return plainText;

        }

        public string Encrypt(string plainText, List<int> key)
        {
            string cipherText = null;

            int columns=key.Count;
            double x=(double)plainText.Length/columns;
            int rows=Ceil(x);
            char[,] matrix=new char[rows,columns];
            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int columnIndex = key[j] - 1;
                    if (k < plainText.Length)
                    {
                        matrix[i, columnIndex] = plainText[k];
                        k++;
                    }
                    else
                    {
                        matrix[i, columnIndex] = 'X'; // Placeholder for empty cells
                    }
                }
            }

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0;j < rows; j++)
                {
                    cipherText += matrix[j, i];
                }
            }


            return cipherText;
        }
    }
}
