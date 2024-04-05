using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary.AES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class AES : CryptographicTechnique
    {
        public override string Decrypt(string cipherText, string key)
        {
            string[,] cipherTextMatrix = new string[4, 4];
            string[,] keyMatrix = new string[4, 4];
            string[,] plainTextMatrix = new string[4, 4];
            string plainText = "";
            int x = 2;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cipherTextMatrix[j, i] = cipherText[x] + "" + cipherText[x + 1];
                    keyMatrix[j, i] = key[x] + "" + key[x + 1];
                    x += 2;
                }
            }

            List<string[,]> keyList = new List<string[,]>();
            keyList.Add(keyMatrix);

            for (int i = 0; i < 10; i++)
            {
                keyMatrix = GeneratKey(keyMatrix, i);
                keyList.Add(keyMatrix);
            }

            cipherTextMatrix = Addroundkey(cipherTextMatrix, keyList[10]);
            cipherTextMatrix = invShiftRowsDecrypt(cipherTextMatrix);
            cipherTextMatrix = invSubByte(cipherTextMatrix, 4, 4);

            for (int i = 9; i >= 1; i--)
            {
                cipherTextMatrix = Addroundkey(cipherTextMatrix, keyList[i]);
                cipherTextMatrix = InverseMixColumns(cipherTextMatrix);
                cipherTextMatrix = invShiftRowsDecrypt(cipherTextMatrix);
                cipherTextMatrix = invSubByte(cipherTextMatrix, 4, 4);
            }

            plainTextMatrix = Addroundkey(cipherTextMatrix, keyList[0]);


            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    plainText += plainTextMatrix[j, i];
                }
            }

            plainText = "0x" + plainText;

            return plainText;
        }


        string[,] S_boxEncryption = new string[,]
        {
        {"63", "7C", "77", "7B", "F2", "6B", "6F", "C5", "30", "01", "67", "2B", "FE", "D7", "AB", "76"},
        {"CA", "82", "C9", "7D", "FA", "59", "47", "F0", "AD", "D4", "A2", "AF", "9C", "A4", "72", "C0"},
        {"B7", "FD", "93", "26", "36", "3F", "F7", "CC", "34", "A5", "E5", "F1", "71", "D8", "31", "15"},
        {"04", "C7", "23", "C3", "18", "96", "05", "9A", "07", "12", "80", "E2", "EB", "27", "B2", "75"},
        {"09", "83", "2C", "1A", "1B", "6E", "5A", "A0", "52", "3B", "D6", "B3", "29", "E3", "2F", "84"},
        {"53", "D1", "00", "ED", "20", "FC", "B1", "5B", "6A", "CB", "BE", "39", "4A", "4C", "58", "CF"},
        {"D0", "EF", "AA", "FB", "43", "4D", "33", "85", "45", "F9", "02", "7F", "50", "3C", "9F", "A8"},
        {"51", "A3", "40", "8F", "92", "9D", "38", "F5", "BC", "B6", "DA", "21", "10", "FF", "F3", "D2"},
        {"CD", "0C", "13", "EC", "5F", "97", "44", "17", "C4", "A7", "7E", "3D", "64", "5D", "19", "73"},
        {"60", "81", "4F", "DC", "22", "2A", "90", "88", "46", "EE", "B8", "14", "DE", "5E", "0B", "DB"},
        {"E0", "32", "3A", "0A", "49", "06", "24", "5C", "C2", "D3", "AC", "62", "91", "95", "E4", "79"},
        {"E7", "C8", "37", "6D", "8D", "D5", "4E", "A9", "6C", "56", "F4", "EA", "65", "7A", "AE", "08"},
        {"BA", "78", "25", "2E", "1C", "A6", "B4", "C6", "E8", "DD", "74", "1F", "4B", "BD", "8B", "8A"},
        {"70", "3E", "B5", "66", "48", "03", "F6", "0E", "61", "35", "57", "B9", "86", "C1", "1D", "9E"},
        {"E1", "F8", "98", "11", "69", "D9", "8E", "94", "9B", "1E", "87", "E9", "CE", "55", "28", "DF"},
        {"8C", "A1", "89", "0D", "BF", "E6", "42", "68", "41", "99", "2D", "0F", "B0", "54", "BB", "16"}
        };
        string[,] S_boxDecryption = new string[,]
        {
        {"52", "09", "6A", "D5", "30", "36", "A5", "38", "BF", "40", "A3", "9E", "81", "F3", "D7", "FB"},
        {"7C", "E3", "39", "82", "9B", "2F", "FF", "87", "34", "8E", "43", "44", "C4", "DE", "E9", "CB"},
        {"54", "7B", "94", "32", "A6", "C2", "23", "3D", "EE", "4C", "95", "0B", "42", "FA", "C3", "4E"},
        {"08", "2E", "A1", "66", "28", "D9", "24", "B2", "76", "5B", "A2", "49", "6D", "8B", "D1", "25"},
        {"72", "F8", "F6", "64", "86", "68", "98", "16", "D4", "A4", "5C", "CC", "5D", "65", "B6", "92"},
        {"6C", "70", "48", "50", "FD", "ED", "B9", "DA", "5E", "15", "46", "57", "A7", "8D", "9D", "84"},
        {"90", "D8", "AB", "00", "8C", "BC", "D3", "0A", "F7", "E4", "58", "05", "B8", "B3", "45", "06"},
        {"D0", "2C", "1E", "8F", "CA", "3F", "0F", "02", "C1", "AF", "BD", "03", "01", "13", "8A", "6B"},
        {"3A", "91", "11", "41", "4F", "67", "DC", "EA", "97", "F2", "CF", "CE", "F0", "B4", "E6", "73"},
        {"96", "AC", "74", "22", "E7", "AD", "35", "85", "E2", "F9", "37", "E8", "1C", "75", "DF", "6E"},
        {"47", "F1", "1A", "71", "1D", "29", "C5", "89", "6F", "B7", "62", "0E", "AA", "18", "BE", "1B"},
        {"FC", "56", "3E", "4B", "C6", "D2", "79", "20", "9A", "DB", "C0", "FE", "78", "CD", "5A", "F4"},
        {"1F", "DD", "A8", "33", "88", "07", "C7", "31", "B1", "12", "10", "59", "27", "80", "EC", "5F"},
        {"60", "51", "7F", "A9", "19", "B5", "4A", "0D", "2D", "E5", "7A", "9F", "93", "C9", "9C", "EF"},
        {"A0", "E0", "3B", "4D", "AE", "2A", "F5", "B0", "C8", "EB", "BB", "3C", "83", "53", "99", "61"},
        {"17", "2B", "04", "7E", "BA", "77", "D6", "26", "E1", "69", "14", "63", "55", "21", "0C", "7D"}
        };

        string[,] MixColumnsEncryptionMatrix = {{"02","03","01","01"},
                                                {"01","02","03","01"},
                                                {"01","01","02","03"},
                                                {"03","01","01","02"}};

        string[,] MixColumnsDecryptionMatrix = {{"0E","0B","0D","09"},
                                                {"09","0E","0B","0D"},
                                                {"0D","09","0E","0B"},
                                                {"0B","0D","09","0E"}};

        string[,] Rcon = new string[4, 10]
            {
            {"01", "02", "04", "08", "10","20", "40", "80", "1B", "36"},
            {"00", "00", "00", "00", "00","00","00", "00", "00","00"},
            {"00", "00", "00", "00", "00","00","00", "00", "00","00"},
            {"00", "00", "00", "00", "00","00","00", "00", "00","00"}
            };
        string[,] shiftRowsEncrypt(string[,] matrix)
        {
            string[,] res = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                int c = i;
                for (int j = 0; j < 4; j++)
                {
                    if (c == 4)
                    {
                        c = 0;
                    }
                    res[i, j] = matrix[i, c];
                    c++;
                }
            }
            return res;
        }
        string[,] invShiftRowsDecrypt(string[,] matrix)
        {
            string[,] res = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int newColIndex = (j + i) % 4;
                    res[i, newColIndex] = matrix[i, j];
                }
            }
            return res;
        }

        string[,] subByte(string[,] matrix, int r, int c)
        {
            string[,] res = new string[r, c];
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    string s = matrix[i, j];
                    res[i, j] = search(s, S_boxEncryption);
                }
            }
            return res;
        }
        string[,] invSubByte(string[,] matrix, int r, int c)
        {
            string[,] res = new string[r, c];
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    string s = matrix[i, j];
                    res[i, j] = search(s, S_boxDecryption);
                }
            }
            return res;
        }
        string search(string s, string[,] s_box)
        {
            s = s.ToLower();
            char s1 = s[0];
            char s2 = s[1];
            int row;
            int col;

            if (s1 == 'a' || s1 == 'b' || s1 == 'c' || s1 == 'd' || s1 == 'e' || s1 == 'f')
            {
                row = s1 - 87;
            }
            else
            {
                row = int.Parse(s1.ToString());
            }
            if (s2 == 'a' || s2 == 'b' || s2 == 'c' || s2 == 'd' || s2 == 'e' || s2 == 'f')
            {
                col = s2 - 87;
            }
            else
            {
                col = int.Parse(s2.ToString());
            }
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (row == i && col == j)
                    {
                        return s_box[i, j];
                    }

                }
            }

            return "";
        }
        string[,] ShiftColumnUp(string[,] matrix)
        {
            string[,] Shift_coloumn = new string[4, 1];

            string temp = matrix[0, 3];
            for (int i = 0; i < 3; i++)
            {
                Shift_coloumn[i, 0] = matrix[i + 1, 3];
            }
            Shift_coloumn[3, 0] = temp;
            return Shift_coloumn;
        }
        string[,] Xor(string[,] old_key, string[,] new_coloumn, int numkey)
        {
            string[,] xorResult = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                int value1 = int.Parse(old_key[i, 0], NumberStyles.HexNumber);
                int value2 = int.Parse(new_coloumn[i, 0], NumberStyles.HexNumber);
                int value3 = int.Parse(Rcon[i, numkey], NumberStyles.HexNumber);

                xorResult[i, 0] = (value1 ^ value2 ^ value3).ToString("X2");
            }
            for (int j = 1; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    int value1 = int.Parse(old_key[i, j], NumberStyles.HexNumber);
                    int value2 = int.Parse(xorResult[i, j - 1], NumberStyles.HexNumber);

                    xorResult[i, j] = (value1 ^ value2).ToString("X2");
                }
            }
            return xorResult;
        }
        string[,] GeneratKey(string[,] old_key, int keynum)
        {
            string[,] first_coloumn = new string[4, 1];
            string[,] SBox_coloumn = new string[4, 1];
            string[,] key = new string[4, 4];
            first_coloumn = ShiftColumnUp(old_key);
            SBox_coloumn = subByte(first_coloumn, 4, 1);
            key = Xor(old_key, SBox_coloumn, keynum);

            return key;
        }
        byte[,] ConvertToByteArray(string[,] hexArray)
        {
            byte[,] byteArray = new byte[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    byteArray[i, j] = byte.Parse(hexArray[i, j], NumberStyles.HexNumber);
                }
            }
            return byteArray;
        }
        byte GaloisMult(byte a, byte b)
        {
            byte result = 0;
            for (int i = 0; i < 8; i++)
            {
                if ((b & 1) != 0)
                {
                    result ^= a;
                }

                bool hi_bit = (a & 0x80) != 0;
                a <<= 1;
                if (hi_bit)
                {
                    a ^= 0x1b;
                }
                b >>= 1;
            }
            return result;
        }
        string[,] ConvertByteMatrixToString(byte[,] byteMatrix)
        {
            int rows = byteMatrix.GetLength(0);
            int cols = byteMatrix.GetLength(1);
            string[,] stringMatrix = new string[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    stringMatrix[i, j] = byteMatrix[i, j].ToString("X2");
                }
            }

            return stringMatrix;
        }

        string[,] MixColumns(string[,] array)
        {
            byte[,] state = ConvertToByteArray(array);
            state = doMixColumns(state);
            string[,] state2 = ConvertByteMatrixToString(state);

            byte[,] doMixColumns(byte[,] tempState)
            {
                for (int col = 0; col < 4; col++)
                {
                    byte[] tempCol = new byte[4];
                    for (int row = 0; row < 4; row++)
                    {
                        tempCol[row] = tempState[row, col];
                    }

                    tempState[0, col] = (byte)(GaloisMult(tempCol[0], 2) ^ GaloisMult(tempCol[1], 3) ^ tempCol[2] ^ tempCol[3]);
                    tempState[1, col] = (byte)(tempCol[0] ^ GaloisMult(tempCol[1], 2) ^ GaloisMult(tempCol[2], 3) ^ tempCol[3]);
                    tempState[2, col] = (byte)(tempCol[0] ^ tempCol[1] ^ GaloisMult(tempCol[2], 2) ^ GaloisMult(tempCol[3], 3));
                    tempState[3, col] = (byte)(GaloisMult(tempCol[0], 3) ^ tempCol[1] ^ tempCol[2] ^ GaloisMult(tempCol[3], 2));
                }
                return tempState;
            }

            return state2;
        }
        string[,] InverseMixColumns(string[,] array)
        {
            byte[,] state = ConvertToByteArray(array);
            state = InvMixColumns(state);
            string[,] state2 = ConvertByteMatrixToString(state);

            byte[,] InvMixColumns(byte[,] tempState)
            {
                for (int col = 0; col < 4; col++)
                {
                    byte[] tempCol = new byte[4];
                    for (int row = 0; row < 4; row++)
                    {
                        tempCol[row] = tempState[row, col];
                    }

                    tempState[0, col] = (byte)(GaloisMult(tempCol[0], 0x0e) ^ GaloisMult(tempCol[1], 0x0b) ^ GaloisMult(tempCol[2], 0x0d) ^ GaloisMult(tempCol[3], 0x09));
                    tempState[1, col] = (byte)(GaloisMult(tempCol[0], 0x09) ^ GaloisMult(tempCol[1], 0x0e) ^ GaloisMult(tempCol[2], 0x0b) ^ GaloisMult(tempCol[3], 0x0d));
                    tempState[2, col] = (byte)(GaloisMult(tempCol[0], 0x0d) ^ GaloisMult(tempCol[1], 0x09) ^ GaloisMult(tempCol[2], 0x0e) ^ GaloisMult(tempCol[3], 0x0b));
                    tempState[3, col] = (byte)(GaloisMult(tempCol[0], 0x0b) ^ GaloisMult(tempCol[1], 0x0d) ^ GaloisMult(tempCol[2], 0x09) ^ GaloisMult(tempCol[3], 0x0e));
                }
                return tempState;
            }

            return state2;
        }

        string[,] Addroundkey(string[,] arr, string[,] key)
        {
            string[,] XOR = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int value1 = int.Parse(arr[i, j], NumberStyles.HexNumber);
                    int value2 = int.Parse(key[i, j], NumberStyles.HexNumber);
                    XOR[i, j] = (value1 ^ value2).ToString("X2");
                }
            }
            return XOR;
        }
        public override string Encrypt(string plainText, string key)
        {
            string[,] plainTextMatrix = new string[4, 4];
            string[,] keyMatrix = new string[4, 4];
            string[,] cipherTextMatrix = new string[4, 4];
            string cipherText = "";
            int x = 2;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    plainTextMatrix[j, i] = plainText[x] + "" + plainText[x + 1];
                    keyMatrix[j, i] = key[x] + "" + key[x + 1];
                    x += 2;
                }
            }

            plainTextMatrix = Addroundkey(plainTextMatrix, keyMatrix);

            for (int i = 0; i < 9; i++)
            {
                keyMatrix = GeneratKey(keyMatrix, i);
                plainTextMatrix = subByte(plainTextMatrix, 4, 4);
                plainTextMatrix = shiftRowsEncrypt(plainTextMatrix);
                plainTextMatrix = MixColumns(plainTextMatrix);
                plainTextMatrix = Addroundkey(plainTextMatrix, keyMatrix);
            }
            keyMatrix = GeneratKey(keyMatrix, 9);
            plainTextMatrix = subByte(plainTextMatrix, 4, 4);
            plainTextMatrix = shiftRowsEncrypt(plainTextMatrix);
            cipherTextMatrix = Addroundkey(plainTextMatrix, keyMatrix);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cipherText += cipherTextMatrix[j, i];
                }
            }
            cipherText = "0x" + cipherText;
            return cipherText;

        }
    }
}