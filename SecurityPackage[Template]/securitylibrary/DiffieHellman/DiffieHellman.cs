using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary.DiffieHellman
{


    public class DiffieHellman
    {

      public  int power(int f, int s, int sf)
        {
            int powerResult = 1;

            for(int i = 0; i < s; i++)
            {
                powerResult *= f;
                powerResult = powerResult % sf;
            }

            return powerResult;

        }

       public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {

            List<int> keys = new List<int>();

            int yA = power(alpha, xa, q);
            int yB=power(alpha, xb, q);
            int k1=power(yB,xa,q);
            int k2=power(yA,xb,q);

            keys.Add(k1);
            keys.Add(k2);

            return keys;
        }
    }
}