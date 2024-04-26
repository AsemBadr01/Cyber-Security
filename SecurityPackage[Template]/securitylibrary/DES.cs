using System;

namespace SecurityLibrary.DES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class DES : CryptographicTechnique
    {
        public override string Decrypt(string cipherText, string key)
        {
            string plainText = "";

            cipherText = cipherText.Substring(2);
            key = key.Substring(2);

            long decimalPlainNumber = Convert.ToInt64(cipherText, 16);
            long decimalKeyNumber = Convert.ToInt64(key, 16);

            // Convert long to binary
            string binaryCypherText = Convert.ToString(decimalPlainNumber, 2).PadLeft(64, '0');
            string binaryKeyText = Convert.ToString(decimalKeyNumber, 2).PadLeft(64, '0');

            string keyPermuted = permutaionChoice(binaryKeyText, pc_1, 8, 7);

            string c = keyPermuted.Substring(0, 28);
            string d = keyPermuted.Substring(28);

          string  permutedPlain = permutaionChoice(binaryCypherText, ipMatrix, 8, 8);
           string l0 = permutedPlain.Substring(0, 32);
          string  r0 = permutedPlain.Substring(32);
            int counter = 0;
            string f;
            string l1;
            int k = 15;
            string[] keys = new string[16];
            string keyNum;
            for (int i=0;i<16;i++)
            {
                counter += shift[i];
                keys[i] = shiftKey(c, d, counter);
            }
            for (int i = 0; i < 16; i++)
            {
                keyNum = keys[k];
                k--;
                string resultPC2 = permutaionChoice(keyNum, pc_2, 8, 6);
                f = F(r0, resultPC2);
                l1 = r0;
                r0 = xOr(l0, f);
                l0 = l1;
            }


            string rees = r0 + l0;
            string binaryPlain = permutaionChoice(rees, IP_1_Matrix, 8, 8);

            for (int i = 0; i < binaryPlain.Length; i += 4)
            {
                string x = binaryPlain.Substring(i, 4);
                int index = Array.IndexOf(hexColumn, x);
                if (index == 10)
                {
                    plainText += "A";
                }
                else if (index == 11)
                {
                    plainText += "B";
                }
                else if (index == 12)
                {
                    plainText += "C";
                }
                else if (index == 13)
                {
                    plainText += "D";
                }
                else if (index == 14)
                {
                    plainText += "E";
                }
                else if (index == 15)
                {
                    plainText += "F";
                }
                else
                {
                    plainText += index;
                }

            }
            return "0x"+plainText;
        }

        int[,] pc_1 = new int[,]
        { {57,49,41,33,25,17,9 },
          {1,58,50,42,34,26,18 },
          {10,2,59,51,43,35,27 },
          {19,11,3,60,52,44,36 },
          {63,55,47,39,31,23,15 },
          {7,62,56,46,38,30,22 },
          {14,6,61,53,45,37,29 },
          {21,13,5,28,20,12,4 }
        };
        int[] shift = new int[] { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
        int[,] pc_2 = new int[,]
        {
           { 14, 17, 11, 24,  1,  5 },
           {  3, 28, 15,  6, 21, 10 },
           { 23, 19, 12,  4, 26,  8 },
           { 16,  7, 27, 20, 13,  2 },
           { 41, 52, 31, 37, 47, 55 },
           { 30, 40, 51, 45, 33, 48 },
           { 44, 49, 39, 56, 34, 53 },
           { 46, 42, 50, 36, 29, 32 }
        };
        int[,] ipMatrix = new int[8, 8]
        {
          { 58, 50, 42, 34, 26, 18, 10,  2 },
          { 60, 52, 44, 36, 28, 20, 12,  4 },
          { 62, 54, 46, 38, 30, 22, 14,  6 },
          { 64, 56, 48, 40, 32, 24, 16,  8 },
          { 57, 49, 41, 33, 25, 17,  9,  1 },
          { 59, 51, 43, 35, 27, 19, 11,  3 },
          { 61, 53, 45, 37, 29, 21, 13,  5 },
          { 63, 55, 47, 39, 31, 23, 15,  7 }
        };
        int[,] e_bit = new int[,]
        {
               { 32,  1,  2,  3,  4,  5 },
    {  4,  5,  6,  7,  8,  9 },
    {  8,  9, 10, 11, 12, 13 },
    { 12, 13, 14, 15, 16, 17 },
    { 16, 17, 18, 19, 20, 21 },
    { 20, 21, 22, 23, 24, 25 },
    { 24, 25, 26, 27, 28, 29 },
    { 28, 29, 30, 31, 32,  1 }
        };
        int[,] s1 = new int[,]
        {
            { 14,  4, 13,  1,  2, 15, 11,  8,  3, 10,  6, 12,  5,  9,  0,  7 },
            {  0, 15,  7,  4, 14,  2, 13,  1, 10,  6, 12, 11,  9,  5,  3,  8 },
            {  4,  1, 14,  8, 13,  6,  2, 11, 15, 12,  9,  7,  3, 10,  5,  0 },
            { 15, 12,  8,  2,  4,  9,  1,  7,  5, 11,  3, 14, 10,  0,  6, 13 }
        };
        int[,] s2 = new int[4, 16]
{
    { 15,  1,  8, 14,  6, 11,  3,  4,  9,  7,  2, 13, 12,  0,  5, 10 },
    {  3, 13,  4,  7, 15,  2,  8, 14, 12,  0,  1, 10,  6,  9, 11,  5 },
    {  0, 14,  7, 11, 10,  4, 13,  1,  5,  8, 12,  6,  9,  3,  2, 15 },
    { 13,  8, 10,  1,  3, 15,  4,  2, 11,  6,  7, 12,  0,  5, 14,  9 }
};
        int[,] s3 = new int[4, 16]
        {
    { 10,  0,  9, 14,  6,  3, 15,  5,  1, 13, 12,  7, 11,  4,  2,  8 },
    { 13,  7,  0,  9,  3,  4,  6, 10,  2,  8,  5, 14, 12, 11, 15,  1 },
    { 13,  6,  4,  9,  8, 15,  3,  0, 11,  1,  2, 12,  5, 10, 14,  7 },
    {  1, 10, 13,  0, 6,  9,  8,  7, 4,  15,  14, 3,  11, 5,  2,  12 }
        };
        int[,] s4 = new int[4, 16]
        {
    {  7, 13, 14,  3,  0,  6,  9, 10,  1,  2,  8,  5, 11, 12,  4, 15 },
    { 13,  8, 11,  5,  6, 15,  0,  3,  4,  7,  2, 12,  1, 10, 14,  9 },
    { 10,  6,  9,  0, 12, 11,  7, 13, 15,  1,  3, 14,  5,  2,  8,  4 },
    {  3, 15,  0,  6, 10,  1, 13,  8,  9,  4,  5, 11, 12,  7,  2, 14 }
        };
        int[,] s5 = new int[4, 16]
{
    {  2, 12,  4,  1,  7, 10, 11,  6,  8,  5,  3, 15, 13,  0, 14,  9 },
    { 14, 11,  2, 12,  4,  7, 13,  1,  5,  0, 15, 10,  3,  9,  8,  6 },
    {  4,  2,  1, 11, 10, 13,  7,  8, 15,  9, 12,  5,  6,  3,  0, 14 },
    { 11,  8, 12,  7,  1, 14,  2, 13,  6, 15,  0,  9, 10,  4,  5,  3 }
};
        int[,] s6 = new int[4, 16]
        {
    { 12,  1, 10, 15,  9,  2,  6,  8,  0, 13,  3,  4, 14,  7,  5, 11 },
    { 10, 15,  4,  2,  7, 12,  9,  5,  6,  1, 13, 14,  0, 11,  3,  8 },
    {  9, 14, 15,  5,  2,  8, 12,  3,  7,  0,  4, 10,  1, 13, 11,  6 },
    {  4,  3,  2, 12,  9,  5, 15, 10, 11, 14,  1,  7,  6,  0,  8, 13 }
        };
        int[,] s7 = new int[4, 16]
{
    {  4, 11,  2, 14, 15,  0,  8, 13,  3, 12,  9,  7,  5, 10,  6,  1 },
    { 13,  0, 11,  7,  4,  9,  1, 10, 14,  3,  5, 12,  2, 15,  8,  6 },
    {  1,  4, 11, 13, 12,  3,  7, 14, 10, 15,  6,  8,  0,  5,  9,  2 },
    {  6, 11, 13,  8,  1,  4, 10,  7,  9,  5,  0, 15, 14,  2,  3, 12 }
};
        int[,] s8 = new int[4, 16]
{
    { 13,  2,  8,  4,  6, 15, 11,  1, 10,  9,  3, 14,  5,  0, 12,  7 },
    {  1, 15, 13,  8, 10,  3,  7,  4, 12,  5,  6, 11,  0, 14,  9,  2 },
    {  7, 11,  4,  1,  9, 12, 14,  2,  0,  6, 10, 13, 15,  3,  5,  8 },
    {  2,  1, 14,  7,  4, 10,  8, 13, 15, 12,  9,  0,  3,  5,  6, 11 }
};

        int[,] pMatrix = new int[8, 4]
{
    {16,  7, 20, 21},
    {29, 12, 28, 17},
    { 1, 15, 23, 26},
    { 5, 18, 31, 10},
    { 2,  8, 24, 14},
    {32, 27,  3,  9},
    {19, 13, 30,  6},
    {22, 11,  4, 25}
};
        int[,] IP_1_Matrix = new int[8, 8]
{
    {40,  8, 48, 16, 56, 24, 64, 32},
    {39,  7, 47, 15, 55, 23, 63, 31},
    {38,  6, 46, 14, 54, 22, 62, 30},
    {37,  5, 45, 13, 53, 21, 61, 29},
    {36,  4, 44, 12, 52, 20, 60, 28},
    {35,  3, 43, 11, 51, 19, 59, 27},
    {34,  2, 42, 10, 50, 18, 58, 26},
    {33,  1, 41,  9, 49, 17, 57, 25}
};


        string[] hexColumn = new string[] { "0000","0001","0010","0011","0100","0101","0110",
            "0111","1000","1001","1010","1011","1100","1101","1110","1111" };
        string[] hexRow = new string[] { "00", "01", "10", "11" };

        string permutaionChoice(string s, int[,] pc, int r, int c)
        {
            string result = "";
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    result += s[pc[i, j] - 1];
                }
            }

            return result;
        }

        string shiftKey(string c, string d, int index)
        {
            string result = "";
            string c1 = "";
            string d1 = "";

            c1 = c.Substring(index) + c.Substring(0, index);
            d1 = d.Substring(index) + d.Substring(0, index);
            result = c1 + d1;

            return result;
        }

        string xOr(string k, string e)
        {
            string result = "";
            for (int i = 0; i < k.Length; i++)
            {
                if (k[i] == e[i])
                {
                    result += '0';
                }
                else
                {
                    result += '1';
                }
            }
            return result;
        }

        int getResSub(int r, int c, int counter)
        {
            int res;

            if (counter == 0)
            {
                res = s1[r, c];
            }
            else if (counter == 1)
            {
                res = s2[r, c];
            }
            else if (counter == 2)
            {
                res = s3[r, c];
            }
            else if (counter == 3)
            {
                res = s4[r, c];
            }
            else if (counter == 4)
            {
                res = s5[r, c];
            }
            else if (counter == 5)
            {
                res = s6[r, c];
            }
            else if (counter == 6)
            {
                res = s7[r, c];
            }
            else
            {
                res = s8[r, c];
            }
            return res;
        }
        string subBytes(string s)
        {
            string result = "";
            int j = 0;
            for (int i = 0; i < s.Length; i += 6)
            {
                string sub = s.Substring(i, 6);
                string r = sub[0] + "" + sub[5];
                string c = sub.Substring(1, 4);
                int columnNum = Array.IndexOf(hexColumn, c);
                int rowNum = Array.IndexOf(hexRow, r);

                int resSub = getResSub(rowNum, columnNum, j);
                j++;
                result += hexColumn[resSub];
            }

            return result;
        }
        string F(string R0, string k)
        {
            string e = permutaionChoice(R0, e_bit, 8, 6);
            string xorResult = xOr(k, e);
            string s = subBytes(xorResult);
            string f = permutaionChoice(s, pMatrix, 8, 4);
            return f;
        }

        public override string Encrypt(string plainText, string key)
        {
            string cipherText = "";

            plainText = plainText.Substring(2);
            key = key.Substring(2);

            long decimalPlainNumber = Convert.ToInt64(plainText, 16);
            long decimalKeyNumber = Convert.ToInt64(key, 16);

            // Convert long to binary
            string binaryPlainText = Convert.ToString(decimalPlainNumber, 2).PadLeft(64, '0');
            string binaryKeyText = Convert.ToString(decimalKeyNumber, 2).PadLeft(64, '0');

            string keyPermuted = permutaionChoice(binaryKeyText, pc_1, 8, 7);

            string c = keyPermuted.Substring(0, 28);
            string d = keyPermuted.Substring(28);
            string permutedPlain = "";
            string l0 = "";
            string r0 = "";
            string l1 = "";
            string f;
            int counter = 0;

            permutedPlain = permutaionChoice(binaryPlainText, ipMatrix, 8, 8);
            l0 = permutedPlain.Substring(0, 32);
            r0 = permutedPlain.Substring(32);

            for (int i = 0; i < 16; i++)
            {
                counter += shift[i];
                string shiftedKey = shiftKey(c, d, counter);
                string resultPC2 = permutaionChoice(shiftedKey, pc_2, 8, 6);
                f = F(r0, resultPC2);
                l1 = r0;
                r0 = xOr(l0, f);
                l0 = l1;
            }

            string rees = r0 + l0;
            string binaryCipher = permutaionChoice(rees, IP_1_Matrix, 8, 8);

            for (int i = 0; i < binaryCipher.Length; i += 4)
            {
                string x = binaryCipher.Substring(i, 4);
                int index = Array.IndexOf(hexColumn, x);
                if (index == 10)
                {
                    cipherText += "A";
                }
                else if (index == 11)
                {
                    cipherText += "B";
                }
                else if (index == 12)
                {
                    cipherText += "C";
                }
                else if (index == 13)
                {
                    cipherText += "D";
                }
                else if (index == 14)
                {
                    cipherText += "E";
                }
                else if (index == 15)
                {
                    cipherText += "F";
                }
                else
                {
                    cipherText += index;
                }

            }

            return "0x" + cipherText;
        }
    }
}
