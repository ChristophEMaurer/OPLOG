using System;
using System.Collections.Generic;
using System.Text;

namespace OperationenSerial
{
    public class SerialLogic
    {
        private delegate char CheckDigitCalculatorDelegate(char c1, char c2, char c3, char c4, char c5);

        Random _random = new Random();

        // 6 Blöcke mit jeweils 6 Stellen = 36 Stellen
        //
        // pro Zeile ergibt sich aus den x das y
        // in jedem Block ergibt sich das z aus allen anderen
        // 5 Werten des Blockes
        //
        // 0      1      2      3      4      5
        // x----z x----z x----z x----z x----z y----z
        // -y---z --x--z ---x-z ----xz -x---z -x---z
        // ----xz ---y-z -x---z --x--z --x--z ----xz
        // ---x-z -x---z ----yz -x---z ----xz --x--z
        // --x--z ----xz ---x-z ---x-z ---y-z ---x-z
        byte[,] _pattern = 
                { 
                    {1, 0, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0},
                    {2, 5, 1, 1, 2, 2, 3, 3, 4, 4, 1, 0, 1},
                    {1, 0, 4, 5, 4, 3, 2, 2, 1, 4, 3, 1, 4},
                    {2, 0, 3, 1, 1, 5, 2, 3, 1, 4, 4, 2, 4},
                    {1, 0, 2, 1, 3, 2, 2, 3, 3, 5, 3, 4, 2}
                };

        public SerialLogic()
        {
        }


        private static char CalcCheckDigit1(char c1, char c2, char c3, char c4, char c5)
        {
            // 1 2 3 4 5
            // Summe von allen + [2] + [4] 
            // Rest von der Summe geteilt durch 7

            char checkDigit;

            int sum = 0;

            sum += Convert.ToInt32(c1);
            sum += Convert.ToInt32(c2);
            sum += Convert.ToInt32(c3);
            sum += Convert.ToInt32(c4);
            sum += Convert.ToInt32(c5);
            sum += Convert.ToInt32(c2);
            sum += Convert.ToInt32(c4);
            int remainder;

            Math.DivRem(sum, 7, out remainder);

            checkDigit = Convert.ToChar(Convert.ToInt32('0') + remainder);

            return checkDigit;
        }

        private static char CalcCheckDigit2(char c1, char c2, char c3, char c4, char c5)
        {
            // 1 2 3 4 5
            // Summe von allen + [2] + [4] 
            // Rest von der Summe geteilt durch 7

            char checkDigit;

            int sum = 0;

            sum += Convert.ToInt32(c1);
            sum += Convert.ToInt32(c2);
            sum += Convert.ToInt32(c3);
            sum += Convert.ToInt32(c4);
            sum += Convert.ToInt32(c5);
            sum += Convert.ToInt32(c1);
            sum += Convert.ToInt32(c3);
            sum += Convert.ToInt32(c5);
            int remainder;

            Math.DivRem(sum, 8, out remainder);

            checkDigit = Convert.ToChar(Convert.ToInt32('0') + remainder);

            return checkDigit;
        }

        public bool ValidateSerialNumber(string serialNumber)
        {
            bool success = false;

            if (serialNumber == null || serialNumber.Length != 36)
            {
                goto exit;
            }

            StringBuilder[] sb = new StringBuilder[6];
            for (int i = 0; i < 6; i++)
            {
                sb[i] = new StringBuilder(serialNumber.Substring(i * 6, 6));
            }

            success = ValidateSerialNumber(sb, _pattern);

            if (success)
            {
                for (int i = 0; i < 6; i++)
                {
                    char partCheckDigit = 'x';

                    partCheckDigit = CalcCheckDigit1(
                            sb[i][0],
                            sb[i][1],
                            sb[i][2],
                            sb[i][3],
                            sb[i][4]);

                    if (partCheckDigit != sb[i][5])
                    {
                        success = false;
                        break;
                    }
                }
            }

        exit:
            return success;
        }
        private bool ValidateSerialNumber(StringBuilder[] sb, byte[,] pattern)
        {
            bool success = true;

            for (int i = 0; i < 5; i++)
            {
                char checkDigit = 'x';

                if (pattern[i, 0] == 1)
                {
                    checkDigit = CalcCheckDigit1(
                        sb[pattern[i, 1]][pattern[i, 2]],
                        sb[pattern[i, 3]][pattern[i, 4]],
                        sb[pattern[i, 5]][pattern[i, 6]],
                        sb[pattern[i, 7]][pattern[i, 8]],
                        sb[pattern[i, 9]][pattern[i, 10]]);
                }
                else if (pattern[i, 0] == 2)
                {
                    checkDigit = CalcCheckDigit2(
                        sb[pattern[i, 1]][pattern[i, 2]],
                        sb[pattern[i, 3]][pattern[i, 4]],
                        sb[pattern[i, 5]][pattern[i, 6]],
                        sb[pattern[i, 7]][pattern[i, 8]],
                        sb[pattern[i, 9]][pattern[i, 10]]);
                }

                if (checkDigit != sb[pattern[i, 11]][pattern[i, 12]])
                {
                    success = false;
                    break;
                }
            }

            return success;
        }
//#if false && DEBUG
#if true || CHRISTOPH_MAURER_INCLUDE_GENERATE
        //
        // Hier werden Seriennummern generiert. Dieser Code darf nur unter bestimmten Bedingungen überhaupt kompiliert werden.
        //
        public string Generate()
        {
            int i;

            // 0      1      2      3      4      5      
            // XXXXXX XXXXXX XXXXXX XXXXXX XXXXXX XXXXXX

            // Quersumme von 1.1, 2.2, 3.3, 4.4, 5.5 mod 
            StringBuilder []sb = new StringBuilder[6];
            for (i = 0; i < 6; i++)
            {
                sb[i] = new StringBuilder("xxxxxx");
            }

            SetRandomCharsWithCheckDigit(sb, _pattern);

            StringBuilder serialNumber = new StringBuilder();
            for (i = 0; i < 6; i++)
            {
                SetPartCheckDigit(sb[i]);
                serialNumber.Append(sb[i]);
            }

            return serialNumber.ToString();
        }
        private void SetRandomCharsWithCheckDigit(StringBuilder[] sb, byte[,] pattern)
        {
            for (int i = 0; i < 5; i++)
            {
                if (pattern[i, 0] == 1)
                {
                    SetRandomCharsWithCheckDigit(sb,
                        pattern[i, 1], pattern[i, 2], pattern[i, 3], pattern[i, 4], pattern[i, 5], pattern[i, 6],
                        pattern[i, 7], pattern[i, 8], pattern[i, 9], pattern[i, 10], pattern[i, 11], pattern[i, 12],
                        CalcCheckDigit1);
                }
                else
                {
                    SetRandomCharsWithCheckDigit(sb,
                        pattern[i, 1], pattern[i, 2], pattern[i, 3], pattern[i, 4], pattern[i, 5], pattern[i, 6],
                        pattern[i, 7], pattern[i, 8], pattern[i, 9], pattern[i, 10], pattern[i, 11], pattern[i, 12],
                        CalcCheckDigit2);
                }
            }
        }
        private char GetRandomChar()
        {
            int i = 0;

            // 48: '0'
            // 57: '9'
            // 65: 'A'
            // 90: 'Z'
            while (i < 48 || (i > 57 && i < 65) || i > 90)
            {
                i = _random.Next(48, 91);
            }

            return Convert.ToChar(i);
        }
        private void SetPartCheckDigit(StringBuilder sb)
        {
            sb[5] = CalcCheckDigit1(sb[0], sb[1], sb[2], sb[3], sb[4]);
        }
        private void SetRandomCharsWithCheckDigit(StringBuilder[] sb,
            int sb1, int pos1,
            int sb2, int pos2,
            int sb3, int pos3,
            int sb4, int pos4,
            int sb5, int pos5,
            int sb6, int pos6,
            CheckDigitCalculatorDelegate f)
        {
            sb[sb1][pos1] = GetRandomChar();
            sb[sb2][pos2] = GetRandomChar();
            sb[sb3][pos3] = GetRandomChar();
            sb[sb4][pos4] = GetRandomChar();
            sb[sb5][pos5] = GetRandomChar();

            char checkDigit = f(sb[sb1][pos1], sb[sb2][pos2], sb[sb3][pos3], sb[sb4][pos4], sb[sb5][pos5]);

            sb[sb6][pos6] = checkDigit;
        }
#else
        public string Generate()
        {
            return null;
        }
#endif
    }
}
