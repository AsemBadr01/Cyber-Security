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
            cipherText = cipherText.ToLower();
            key = key.ToLower();
            Dictionary<string, Tuple<int, int>> keytable = new Dictionary<string, Tuple<int, int>>();
            Dictionary<Tuple<int, int>, string> sub = new Dictionary<Tuple<int, int>, string>();

            HashSet<string> keysplit = new HashSet<string>();
            HashSet<string> keys = new HashSet<string> { "a", "b", "c", "d", "e", "f", "g", "h",
        "i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
            StringBuilder result = new StringBuilder();
            StringBuilder resultt = new StringBuilder();

            for (int i = 0; i < key.Length; i++)
            {
                if (!keysplit.Contains(key[i].ToString()))
                {
                    keysplit.Add(key[i].ToString());
                    keys.Remove(key[i].ToString());
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (keysplit.Count > 0)
                    {
                        if (keysplit.First() == "i")
                        {
                            keytable.Add("i", Tuple.Create(i, j));
                            keytable.Add("j", Tuple.Create(i, j));
                            sub.Add(Tuple.Create(i, j), "i");
                            keysplit.Remove("i");
                            keysplit.Remove("j");
                            keys.Remove("i");
                            keys.Remove("j");
                        }
                        else if (keysplit.First() == "j")
                        {
                            keytable.Add("i", Tuple.Create(i, j));
                            keytable.Add("j", Tuple.Create(i, j));
                            sub.Add(Tuple.Create(i, j), "i");
                            keysplit.Remove("i");
                            keysplit.Remove("j");
                            keys.Remove("i");
                            keys.Remove("j");
                        }
                        else
                        {
                            keytable.Add(keysplit.First(), Tuple.Create(i, j));
                            sub.Add(Tuple.Create(i, j), keysplit.First());
                            keysplit.Remove(keysplit.First());
                        }
                    }
                    else
                    {
                        if (keys.First() == "i")
                        {
                            keytable.Add("i", Tuple.Create(i, j));
                            keytable.Add("j", Tuple.Create(i, j));
                            sub.Add(Tuple.Create(i, j), "i");
                            keys.Remove("i");
                            keys.Remove("j");
                        }
                        else if (keys.First() == "j")
                        {
                            keytable.Add("i", Tuple.Create(i, j));
                            keytable.Add("j", Tuple.Create(i, j));
                            sub.Add(Tuple.Create(i, j), "i");
                            keys.Remove("i");
                            keys.Remove("j");
                        }
                        else
                        {
                            keytable.Add(keys.First(), Tuple.Create(i, j));
                            sub.Add(Tuple.Create(i, j), keys.First());
                            keys.Remove(keys.First());
                        }
                    }
                }
            }

            char f, s;
            for (int i = 0; i < cipherText.Length; i += 2)
            {
                f = cipherText[i];
                s = cipherText[i + 1];
                result.Append(toplain(f, s, keytable, sub));
            }

            for (int i = 0; i < result.Length; i++)
            {
                if (i == result.Length - 1)
                {
                    if (result[i] != 'x')
                    {
                        resultt.Append(result[i]);
                    }
                    else
                    {
                        if (result.Length % 2 != 0)
                        {
                            resultt.Append(result[i]);
                        }
                    }
                }
                else
                {
                    if (result[i] == 'x' && result[i - 1] == result[i + 1])
                    {
                        continue;
                    }
                    else
                    {
                        resultt.Append(result[i]);
                    }
                }
            }
            string b = resultt.ToString();
            return b;
        }

        public string Encrypt(string plainText, string key)
        {
            Dictionary<string, Tuple<int, int>> keytable = new Dictionary<string, Tuple<int, int>>();
            Dictionary<Tuple<int, int>, string> sub = new Dictionary<Tuple<int, int>, string>();

            HashSet<string> keysplit = new HashSet<string>();
            HashSet<string> keys = new HashSet<string> { "a", "b", "c", "d", "e", "f", "g", "h",
        "i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
            HashSet<int> indices = new HashSet<int>();
            HashSet<int> statindices = new HashSet<int>();
            StringBuilder sb = new StringBuilder();
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < key.Length; i++)
            {
                if (!keysplit.Contains(key[i].ToString()))
                {
                    keysplit.Add(key[i].ToString());
                    keys.Remove(key[i].ToString());
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (keysplit.Count > 0)
                    {
                        if (keysplit.First() == "i")
                        {
                            keytable.Add("i", Tuple.Create(i, j));
                            keytable.Add("j", Tuple.Create(i, j));
                            sub.Add(Tuple.Create(i, j), "i");
                            keysplit.Remove("i");
                            keysplit.Remove("j");
                            keys.Remove("i");
                            keys.Remove("j");
                        }
                        else if (keysplit.First() == "j")
                        {
                            keytable.Add("i", Tuple.Create(i, j));
                            keytable.Add("j", Tuple.Create(i, j));
                            sub.Add(Tuple.Create(i, j), "i");
                            keysplit.Remove("i");
                            keysplit.Remove("j");
                            keys.Remove("i");
                            keys.Remove("j");
                        }
                        else
                        {
                            keytable.Add(keysplit.First(), Tuple.Create(i, j));
                            sub.Add(Tuple.Create(i, j), keysplit.First());
                            keysplit.Remove(keysplit.First());
                        }
                    }
                    else
                    {
                        if (keys.First() == "i")
                        {
                            keytable.Add("i", Tuple.Create(i, j));
                            keytable.Add("j", Tuple.Create(i, j));
                            sub.Add(Tuple.Create(i, j), "i");
                            keys.Remove("i");
                            keys.Remove("j");
                        }
                        else if (keys.First() == "j")
                        {
                            keytable.Add("i", Tuple.Create(i, j));
                            keytable.Add("j", Tuple.Create(i, j));
                            sub.Add(Tuple.Create(i, j), "i");
                            keys.Remove("i");
                            keys.Remove("j");
                        }
                        else
                        {
                            keytable.Add(keys.First(), Tuple.Create(i, j));
                            sub.Add(Tuple.Create(i, j), keys.First());
                            keys.Remove(keys.First());
                        }
                    }
                }
            }

            char f, s;
            for (int i = 0; i < plainText.Length; i += 2)
            {
                if (i + 1 == plainText.Length)
                {
                    f = plainText[i];
                    result.Append(tocipher(f, 'x', keytable, sub));
                }
                else
                {
                    f = plainText[i];
                    s = plainText[i + 1];
                    if (f != s)
                    {
                        result.Append(tocipher(f, s, keytable, sub));
                    }
                    else if (f == s)
                    {
                        result.Append(tocipher(f, 'x', keytable, sub));
                        i -= 1;
                    }
                }

            }
            string b = result.ToString();
            return b;
        }

        public string tocipher(char first, char second, Dictionary<string, Tuple<int, int>> keytable, Dictionary<Tuple<int, int>, string> sub)
        {
            StringBuilder result = new StringBuilder();
            int r1, r2, c1, c2;
            if (keytable[first.ToString()].Item1 == keytable[second.ToString()].Item1)
            {
                r1 = keytable[first.ToString()].Item1;
                r2 = keytable[second.ToString()].Item1;
                c1 = keytable[first.ToString()].Item2;
                c2 = keytable[second.ToString()].Item2;
                if ((c1 + 1) > 4 && (c2 + 1) > 4)
                {
                    Tuple<int, int> nextval1 = Tuple.Create(r1, 0);
                    Tuple<int, int> nextval2 = Tuple.Create(r2, 0);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
                else if ((c1 + 1) > 4)
                {
                    //Console.WriteLine(c1 + " " + c2);
                    Tuple<int, int> nextval1 = Tuple.Create(r1, 0);
                    Tuple<int, int> nextval2 = Tuple.Create(r2, c2 + 1);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
                else if ((c2 + 1) > 4)
                {
                    Tuple<int, int> nextval1 = Tuple.Create(r1, c1 + 1);
                    Tuple<int, int> nextval2 = Tuple.Create(r2, 0);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
                else
                {
                    Tuple<int, int> nextval1 = Tuple.Create(r1, c1 + 1);
                    Tuple<int, int> nextval2 = Tuple.Create(r2, c2 + 1);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
            }
            else if (keytable[first.ToString()].Item2 == keytable[second.ToString()].Item2)
            {
                r1 = keytable[first.ToString()].Item1;
                r2 = keytable[second.ToString()].Item1;
                c1 = keytable[first.ToString()].Item2;
                c2 = keytable[second.ToString()].Item2;
                if (r1 + 1 > 4 && r2 + 1 > 4)
                {
                    Tuple<int, int> nextval1 = Tuple.Create(0, c1);
                    Tuple<int, int> nextval2 = Tuple.Create(0, c2);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
                else if (r1 + 1 > 4)
                {
                    Tuple<int, int> nextval1 = Tuple.Create(0, c1);
                    Tuple<int, int> nextval2 = Tuple.Create(r2 + 1, c2);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
                else if (r2 + 1 > 4)
                {
                    Tuple<int, int> nextval1 = Tuple.Create(r1 + 1, c1);
                    Tuple<int, int> nextval2 = Tuple.Create(0, c2);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
                else
                {
                    Tuple<int, int> nextval1 = Tuple.Create(r1 + 1, c1);
                    Tuple<int, int> nextval2 = Tuple.Create(r2 + 1, c2);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
            }
            else
            {
                r1 = keytable[first.ToString()].Item1;
                r2 = keytable[second.ToString()].Item1;
                c1 = keytable[first.ToString()].Item2;
                c2 = keytable[second.ToString()].Item2;
                Tuple<int, int> nextval1 = Tuple.Create(r1, c2);
                Tuple<int, int> nextval2 = Tuple.Create(r2, c1);
                result.Append(sub[nextval1]);
                result.Append(sub[nextval2]);
            }
            string resultt = result.ToString();
            return resultt;
        }
        public string toplain(char first, char second, Dictionary<string, Tuple<int, int>> keytable, Dictionary<Tuple<int, int>, string> sub)
        {
            StringBuilder result = new StringBuilder();
            int r1, r2, c1, c2;
            if (keytable[first.ToString()].Item1 == keytable[second.ToString()].Item1)
            {
                r1 = keytable[first.ToString()].Item1;
                r2 = keytable[second.ToString()].Item1;
                c1 = keytable[first.ToString()].Item2;
                c2 = keytable[second.ToString()].Item2;
                if ((c1 - 1) < 0 && (c2 - 1) < 0)
                {
                    Tuple<int, int> nextval1 = Tuple.Create(r1, 4);
                    Tuple<int, int> nextval2 = Tuple.Create(r2, 4);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
                else if ((c1 - 1) < 0)
                {
                    Tuple<int, int> nextval1 = Tuple.Create(r1, 4);
                    Tuple<int, int> nextval2 = Tuple.Create(r2, c2 - 1);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
                else if ((c2 - 1) < 0)
                {
                    Tuple<int, int> nextval1 = Tuple.Create(r1, c1 - 1);
                    Tuple<int, int> nextval2 = Tuple.Create(r2, 4);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
                else
                {
                    Tuple<int, int> nextval1 = Tuple.Create(r1, c1 - 1);
                    Tuple<int, int> nextval2 = Tuple.Create(r2, c2 - 1);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
            }
            else if (keytable[first.ToString()].Item2 == keytable[second.ToString()].Item2)
            {
                r1 = keytable[first.ToString()].Item1;
                r2 = keytable[second.ToString()].Item1;
                c1 = keytable[first.ToString()].Item2;
                c2 = keytable[second.ToString()].Item2;
                if (r1 - 1 < 0 && r2 - 1 < 0)
                {
                    Tuple<int, int> nextval1 = Tuple.Create(4, c1);
                    Tuple<int, int> nextval2 = Tuple.Create(4, c2);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
                else if (r1 - 1 < 0)
                {
                    Tuple<int, int> nextval1 = Tuple.Create(4, c1);
                    Tuple<int, int> nextval2 = Tuple.Create(r2 - 1, c2);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
                else if (r2 - 1 < 0)
                {
                    Tuple<int, int> nextval1 = Tuple.Create(r1 - 1, c1);
                    Tuple<int, int> nextval2 = Tuple.Create(4, c2);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
                else
                {
                    Tuple<int, int> nextval1 = Tuple.Create(r1 - 1, c1);
                    Tuple<int, int> nextval2 = Tuple.Create(r2 - 1, c2);
                    result.Append(sub[nextval1]);
                    result.Append(sub[nextval2]);
                }
            }
            else
            {
                r1 = keytable[first.ToString()].Item1;
                r2 = keytable[second.ToString()].Item1;
                c1 = keytable[first.ToString()].Item2;
                c2 = keytable[second.ToString()].Item2;
                Tuple<int, int> nextval1 = Tuple.Create(r1, c2);
                Tuple<int, int> nextval2 = Tuple.Create(r2, c1);
                result.Append(sub[nextval1]);
                result.Append(sub[nextval2]);
            }
            string resultt = result.ToString();
            return resultt;
        }
    }
}