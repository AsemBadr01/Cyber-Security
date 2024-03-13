using System;
using System.Collections.Generic;
using System.Data.Common;

namespace SecurityLibrary
{

    public class HillCipher : ICryptographicTechnique<string, string>, ICryptographicTechnique<List<int>, List<int>>
    {
        public string Encrypt(string plainText, string key)
        {
            throw new NotImplementedException();
        }
        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }
        public string Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }
        public string Analyse3By3Key(string plain3, string cipher3)
        {
            throw new NotImplementedException();
        }



        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            int plaincount = 0, keycount = 0, sum = 0;
            int m = (int)square(key.Count);
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

        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            int column, count = 0, iterate, sum = 0;
            List<int> result = new List<int>();
            if (key.Count == 4)
            {
                for (int i = 0; i < key.Count; i++)
                {
                    if (key[i] < 0 || key[i] > 26)
                    {
                        throw new InvalidAnlysisException();
                    }
                }
                column = cipherText.Count / 2;
                int[,] twodarray = new int[2, column];
                List<double> inverse = Inverse2x2(key);
                int det = det2x2(key);
                if (det == 0)
                {
                    throw new InvalidAnlysisException();
                }
                GCD(det);
                check(det);
                for (int i = 0; i < column; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        twodarray[j, i] = cipherText[count];
                        count++;
                    }
                }

                for (int i = 0; i < column; i++)
                {
                    iterate = 0;
                    for (int j = 0; j < 2; j++)
                    {
                        sum += ((int)inverse[iterate] * twodarray[j, i]);
                        if (j == 1 && iterate == 3)
                        {
                            if (sum >= 0)
                            {
                                result.Add(sum % 26);
                            }
                            else
                            {
                                result.Add((sum % 26) + 26);
                            }
                            sum = 0;
                        }
                        else if (j == 1 && iterate != 3)
                        {
                            if (sum >= 0)
                            {
                                result.Add(sum % 26);
                            }
                            else
                            {
                                result.Add((sum % 26) + 26);
                            }
                            sum = 0;
                            j = -1;
                        }
                        iterate++;
                    }
                }
            }
            else
            {
                column = cipherText.Count / 3;
                int[,] twodarray = new int[3, column];
                List<int> keyinverse = Inverse3x3(key);
                for (int i = 0; i < column; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        twodarray[j, i] = cipherText[count];
                        count++;
                    }
                }

                for (int i = 0; i < column; i++)
                {
                    iterate = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        sum += ((int)keyinverse[iterate] * twodarray[j, i]);
                        if (j == 2 && iterate == 8)
                        {
                            if (sum >= 0)
                            {
                                result.Add(sum % 26);
                            }
                            else
                            {
                                result.Add((sum % 26) + 26);
                            }
                            sum = 0;
                        }
                        else if (j == 2 && iterate != 8)
                        {
                            if (sum >= 0)
                            {
                                result.Add(sum % 26);
                            }
                            else
                            {
                                result.Add((sum % 26) + 26);
                            }
                            sum = 0;
                            j = -1;
                        }
                        iterate++;
                    }
                }
            }
            return result;
        }

        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            List<int> inverse = new List<int>();
            List<int> fcp;
            List<int> scp;
            List<int> cipher = new List<int>();
            List<int> result = new List<int>();
            List<int> fp = new List<int>();

            int column = plainText.Count / 2, det, b = 0, sum = 0, findex = 0, sindex = 0, count = 0, iterate = 0;
            int[,] twodarray = new int[2, column];
            bool x = false;

            for (int index1 = 0; index1 < plainText.Count; index1 += 2)
            {
                for (int index2 = index1 + 2; index2 < plainText.Count; index2 += 2)
                {
                    fp = plainText.GetRange(index1, 2);
                    List<int> sp = plainText.GetRange(index2, 2);
                    for (int j = 0; j < sp.Count; j++)
                    {
                        fp.Add(sp[j]);
                    }
                    det = det2x2(fp);
                    if (GCDCheck(det))
                    {
                        for (int i = 0; i < 26; i++)
                        {
                            if ((i * det) % 26 == 1)
                            {
                                b = i;
                                x = true;
                                findex = index1;
                                sindex = index2;
                                break;
                            }
                        }
                    }
                    if (x)
                    {
                        break;
                    }
                }
                if (x)
                {
                    break;
                }
            }
            if (b == 0)
            {
                throw new InvalidAnlysisException();
            }
            else
            {
                inverse = Inv(b, fp);
                fcp = cipherText.GetRange(findex, 2);
                scp = cipherText.GetRange(sindex, 2);

                for (int i = 0; i < 4; i++)
                {
                    if (i < 2)
                    {
                        cipher.Add(fcp[i]);
                    }
                    else
                    {
                        cipher.Add(scp[i - 2]);
                    }
                }

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        twodarray[j, i] = cipher[count];
                        count++;
                    }
                }

                for (int i = 0; i < 2; i++)
                {
                    iterate = 0;
                    for (int j = 0; j < 2; j++)
                    {
                        sum += twodarray[i, j] * inverse[iterate];
                        if (j == 1 && iterate == 3)
                        {
                            if (sum >= 0)
                            {
                                result.Add(sum % 26);
                            }
                            else
                            {
                                result.Add((sum % 26) + 26);
                            }
                            sum = 0;
                        }
                        else if (j == 1 && iterate != 3)
                        {
                            if (sum >= 0)
                            {
                                result.Add(sum % 26);
                            }
                            else
                            {
                                result.Add((sum % 26) + 26);
                            }
                            sum = 0;
                            j = -1;
                        }
                        iterate++;
                    }
                }
            }

            return result;
        }


        public List<int> Analyse3By3Key(List<int> plain3, List<int> cipher3)
        {
            int column, count = 0, iterate, sum = 0;
            List<int> result = new List<int>();
            column = plain3.Count / 3;
            int[,] twodarray = new int[3, column];
            List<int> keyinverse = Inverse3x3(plain3);
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    twodarray[j, i] = cipher3[count];
                    count++;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                iterate = 0;
                for (int j = 0; j < column; j++)
                {
                    sum += ((int)keyinverse[iterate] * twodarray[i, j]);
                    if (j == 2 && iterate == 8)
                    {
                        if (sum >= 0)
                        {
                            result.Add(sum % 26);
                        }
                        else
                        {
                            result.Add((sum % 26) + 26);
                        }
                        sum = 0;
                    }
                    else if (j == 2 && iterate != 8)
                    {
                        if (sum >= 0)
                        {
                            result.Add(sum % 26);
                        }
                        else
                        {
                            result.Add((sum % 26) + 26);
                        }
                        sum = 0;
                        j = -1;
                    }
                    iterate++;
                }
            }
            return result;
        }


        public List<double> Inverse2x2(List<int> key)
        {
            double factor = (1.0 / ((key[0] * key[3]) - (key[1] * key[2])));
            List<double> result = new List<double> { key[3] * factor, key[1] * factor * -1, key[2] * factor * -1, key[0] * factor };
            return result;
        }
        public static List<int> Inv(int b, List<int> key)
        {
            List<int> result = new List<int> { key[3] * b, key[1] * b * -1, key[2] * b * -1, key[0] * b };
            return result;
        }
        public List<int> Inverse3x3(List<int> key)
        {
            for (int i = 0; i < key.Count; i++)
            {
                if (key[i] < 0 || key[i] > 26)
                {
                    throw new InvalidAnlysisException();
                }
            }
            int det, m, x, c, b = 0;
            m = (int)square(key.Count);
            int[,] twodkey = new int[m, m];
            List<int> result = new List<int>();

            det = det3x31row(key);
            GCD(det);
            if (det == 0)
            {
                throw new InvalidAnlysisException();
            }
            x = 26 - det;

            for (int i = 0; i < 26; i++)
            {
                if (((26 - i) * (26 - x)) % 26 == 1)
                {
                    c = i;
                    b = 26 - c;
                    if ((b * det) % 26 != 1 || b > 26 || b < 0)
                    {
                        throw new InvalidAnlysisException();
                    }
                    break;
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int y = (b * power(-1, i + j) * det3x3(key, i, j));
                    if (y >= 0)
                    {
                        twodkey[j, i] = (y % 26);
                    }
                    else
                    {
                        twodkey[j, i] = (y % 26) + 26;
                    }
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result.Add(twodkey[i, j]);
                }
            }
            return result;
        }

        public int det2x2(List<int> submatrix)
        {
            int result = ((submatrix[0] * submatrix[3]) - (submatrix[1] * submatrix[2]));
            return result;
        }
        public int det3x31row(List<int> key)
        {
            int m = (int)square(key.Count), count = 0, sum = 0, iterate = 0, summ;
            int[,] twodkey = new int[m, m];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    twodkey[i, j] = key[count];
                    count++;
                }
            }
            while (iterate < 3)
            {
                List<int> sub = new List<int>();
                int keyy = twodkey[0, iterate];
                for (int i = 0; i < m; i++)
                {
                    if (i != 0)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            if (j != iterate)
                            {
                                sub.Add(twodkey[i, j]);
                            }
                            else
                            {
                                continue;
                            }
                        }

                    }
                }
                if (iterate % 2 == 0)
                {
                    sum += keyy * det2x2(sub);
                }
                else
                {
                    sum += (keyy * -1) * det2x2(sub);
                }
                iterate++;
            }
            if (sum >= 0)
            {
                summ = sum % 26;
            }
            else
            {
                summ = ((sum) % 26) + 26;
            }
            return summ;
        }
        public int det3x3(List<int> key, int ii, int jj)
        {
            int m = (int)square(key.Count), sum = 0, count = 0;
            int[,] twodkey = new int[m, m];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    twodkey[i, j] = key[count];
                    count++;
                }
            }
            List<int> sub = new List<int>();
            for (int i = 0; i < m; i++)
            {
                if (i != ii)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (j != jj)
                        {
                            sub.Add(twodkey[i, j]);
                        }
                    }

                }
            }
            sum += det2x2(sub);
            return sum;
        }

        public double square(int number, double epsilon = 1e-10)
        {
            double guess = number / 2;
            if (guess > 0)
            {
                while (guess * guess - number > epsilon)
                {
                    guess = 0.5 * (guess + number / guess);
                }
            }
            else
            {
                while ((guess * guess - number) * -1 > epsilon)
                {
                    guess = 0.5 * (guess + number / guess);
                }
            }
            return guess;
        }
        public int power(int num, int pow)
        {
            int sum = 1;
            for (int i = 0; i < pow; i++)
            {
                sum *= num;
            }
            return sum;
        }
        public void check(int det)
        {
            bool x = false;
            for (int i = 0; i < 26; i++)
            {
                int k = (det * i) % 26;
                if (k >= 0)
                {
                    if (k == 1)
                    {
                        x = true;
                        break;
                    }
                }
                else
                {
                    if (k + 26 == 1)
                    {
                        x = true;
                        break;
                    }
                }
            }
            if (!x)
            {
                throw new InvalidAnlysisException();
            }
        }
        public void GCD(int det)
        {
            for (int i = 1; i < 26; i++)
            {
                if (det % i == 0 && 26 % i == 0 && i > 1)
                {
                    throw new InvalidAnlysisException();
                }
            }
        }
        public bool GCDCheck(int det)
        {
            bool x = true;
            for (int i = 1; i < 26; i++)
            {
                if (det % i == 0 && 26 % i == 0 && i > 1)
                {
                    x = false;
                    break;
                }
            }
            if (!x)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}