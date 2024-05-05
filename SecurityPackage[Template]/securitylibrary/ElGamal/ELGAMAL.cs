using SecurityLibrary.AES;
using System.Collections.Generic;


namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>
        /// 

        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
            List<long> cipher = new List<long>();
            long c1 = power(alpha, k, q);
            cipher.Add(c1);
            long K = power(y, k, q);
            long c2 = (K * m) % q;
            cipher.Add(c2);
            return cipher;
        }

        public int Decrypt(int c1, int c2, int x, int q)
        {
            ExtendedEuclid algorithm = new ExtendedEuclid();

            long K = power(c1, x, q);
            int res = algorithm.GetMultiplicativeInverse((int)K, q);

            int M = (c2 * res) % q;

            return M;

        }
        public long power(int num, int p, int mod)
        {
            long total = 1;
            for (int i = 0; i < p; i++)
            {
                total *= num;
                if (total > mod)
                {
                    total %= mod;
                }
            }
            return total;
        }
        /*public long powerhelp(int num, int powerr, int mod)
        {
            long s = 0, m, total = 1;
            while (powerr > 5)
            {
                powerr = powerr - 5;
                s = power(num, 5);
                m = s % mod;
                total *= m;
                if (total > mod)
                {
                    total %= mod;
                }
            }
            s = power(num, powerr);
            m = s % mod;
            total *= m;
            total = total % mod;
            return total;
        }*/
    }
}
