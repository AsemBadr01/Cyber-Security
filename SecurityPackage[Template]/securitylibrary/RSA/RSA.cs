using SecurityLibrary.AES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public int power(int f, int s, int sf)
        {
            int powerResult = 1;

            for (int i = 0; i < s; i++)
            {
                powerResult *= f;
                powerResult = powerResult % sf;
            }

            return powerResult;

        }
        ExtendedEuclid extendedEuclid = new ExtendedEuclid();
        public int Encrypt(int p, int q, int M, int e)
        {

             
            int c;

            int n = p * q;
            int phiN=(p-1)*(q-1);
            int d=extendedEuclid.GetMultiplicativeInverse(e, phiN);
            c = power(M, e, n);

            return c;
        }

        public int Decrypt(int p, int q, int C, int e)
        {
            int m;
            int n = p * q;
            int phiN = (p - 1) * (q - 1);
            int d = extendedEuclid.GetMultiplicativeInverse(e, phiN);
            m=power(C,d,n);

            return m;
        }
    }
}
