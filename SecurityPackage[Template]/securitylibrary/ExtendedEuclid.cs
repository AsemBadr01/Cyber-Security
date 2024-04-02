using System.Collections.Generic;
using System.Linq;

namespace SecurityLibrary.AES
{
    public class ExtendedEuclid
    {

        public int GetMultiplicativeInverse(int number, int baseN)
        {
            List<int> Q = new List<int>();
            List<int> A1 = new List<int>();
            List<int> A2 = new List<int>();
            List<int> A3 = new List<int>();
            List<int> B1 = new List<int>();
            List<int> B2 = new List<int>();
            List<int> B3 = new List<int>();
            A1.Add(1);
            A2.Add(0);
            A3.Add(baseN);
            B1.Add(0);
            B2.Add(1);
            B3.Add(number);
            int i = 0;
            while (B3.Last() != 1 && B3.Last() != 0)
            {
                Q.Add(((A3[i]) / (B3[i])));
                A1.Add(B1[i]);
                A2.Add(B2[i]);
                A3.Add(B3[i]);
                B1.Add(A1[i] - ((Q[i]) * B1[i]));
                B2.Add(A2[i] - ((Q[i]) * B2[i]));
                B3.Add(A3[i] - ((Q[i]) * B3[i]));
                i++;
            }
            if (B3.Last() == 0)
            {
                return -1;
            }
            else if (B3.Last() == 1)
            {
                if (B2.Last() >= 0)
                {
                    return B2.Last();
                }
                else
                {
                    int last = B2.Last();
                    while (last < 0)
                    {
                        last += baseN;
                    }
                    return last;
                }
            }
            return 0;
        }
    }
}
