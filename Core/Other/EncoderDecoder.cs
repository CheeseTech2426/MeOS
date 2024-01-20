using System;
using System.Text;

namespace MeOS.Core.Other
{
    public class EncoderDecoder
    {
        public static string Encode(string text)
        {
            StringBuilder encodedText = new StringBuilder();

            foreach (char c in text)
            {
                int id = GetCharacterId(c);

                encodedText.Append(id.ToString("D2"));
                encodedText.Append(",");
            }
            return encodedText.ToString().TrimEnd(',');
        }

        public static string Decode(string encodedText)
        {
            StringBuilder decodedText = new StringBuilder();

            string[] idStrings = encodedText.Split(',');

            foreach (string idString in idStrings)
            {
                if (int.TryParse(idString, out int id))
                {
                    char c = GetCharacterFromId(id);
                    decodedText.Append(c);
                }
            }

            return decodedText.ToString();
        }


        public static bool CheckIfDoubleEncoded(string file)
        {
            string decode1 = Decode(file);
            for (int i = 0; i < 9; i++)
            {
                if (GetFirstThreeChars(decode1)[2] == ',')
                {
                    if (decode1.StartsWith(i.ToString()))
                        return true;
                }
            }
            return false;
        }

        public static string GetFirstThreeChars(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 3)
            {
                return input;
            }

            return input.Substring(0, 3);
        }


        private static char GetCharacterFromId(int id)
        {
            switch (id)
            {
                case 00: return 'A';
                case 01: return 'B';
                case 02: return 'C';
                case 03: return 'D';
                case 04: return 'E';
                case 05: return 'F';
                case 06: return 'G';
                case 07: return 'H';
                case 08: return 'I';
                case 09: return 'J';
                case 10: return 'K';
                case 11: return 'L';
                case 12: return 'M';
                case 13: return 'N';
                case 14: return 'O';
                case 15: return 'P';
                case 16: return 'Q';
                case 17: return 'R';
                case 18: return 'S';
                case 19: return 'T';
                case 20: return 'U';
                case 21: return 'V';
                case 22: return 'W';
                case 23: return 'X';
                case 24: return 'Y';
                case 25: return 'Z';
                case 26: return 'a';
                case 27: return 'b';
                case 28: return 'c';
                case 29: return 'd';
                case 30: return 'e';
                case 31: return 'f';
                case 32: return 'g';
                case 33: return 'h';
                case 34: return 'i';
                case 35: return 'j';
                case 36: return 'k';
                case 37: return 'l';
                case 38: return 'm';
                case 39: return 'n';
                case 40: return 'o';
                case 41: return 'p';
                case 42: return 'q';
                case 43: return 'r';
                case 44: return 's';
                case 45: return 't';
                case 46: return 'u';
                case 47: return 'v';
                case 48: return 'w';
                case 49: return 'x';
                case 50: return 'y';
                case 51: return 'z';
                case 52: return '0';
                case 53: return '1';
                case 54: return '2';
                case 55: return '3';
                case 56: return '4';
                case 57: return '5';
                case 58: return '6';
                case 59: return '7';
                case 60: return '8';
                case 61: return '9';
                case 62: return '.';
                case 63: return ',';
                case 64: return '!';
                case 65: return '?';
                case 66: return ':';
                case 67: return ';';
                case 68: return '(';
                case 69: return ')';
                case 70: return '[';
                case 71: return ']';
                case 72: return '{';
                case 73: return '}';
                case 74: return '<';
                case 75: return '>';
                case 76: return '-';
                case 77: return '+';
                case 78: return '=';
                case 79: return '/';
                case 80: return '\\';
                case 81: return '#';
                case 82: return '@';
                case 83: return '$';
                case 84: return '%';
                case 85: return '&';
                case 86: return '*';
                case 87: return '^';
                case 88: return '|';
                case 89: return '~';
                case 90: return ' ';
                case 91: return '\'';
                case 92: return '\n';
                default: return '\0'; // Return null character for invalid IDs
            }
        }

        public static int GetCharacterId(char c)
        {
            switch (c)
            {
                case 'A': return 00;
                case 'B': return 01;
                case 'C': return 02;
                case 'D': return 03;
                case 'E': return 04;
                case 'F': return 05;
                case 'G': return 06;
                case 'H': return 07;
                case 'I': return 08;
                case 'J': return 09;
                case 'K': return 10;
                case 'L': return 11;
                case 'M': return 12;
                case 'N': return 13;
                case 'O': return 14;
                case 'P': return 15;
                case 'Q': return 16;
                case 'R': return 17;
                case 'S': return 18;
                case 'T': return 19;
                case 'U': return 20;
                case 'V': return 21;
                case 'W': return 22;
                case 'X': return 23;
                case 'Y': return 24;
                case 'Z': return 25;
                case 'a': return 26;
                case 'b': return 27;
                case 'c': return 28;
                case 'd': return 29;
                case 'e': return 30;
                case 'f': return 31;
                case 'g': return 32;
                case 'h': return 33;
                case 'i': return 34;
                case 'j': return 35;
                case 'k': return 36;
                case 'l': return 37;
                case 'm': return 38;
                case 'n': return 39;
                case 'o': return 40;
                case 'p': return 41;
                case 'q': return 42;
                case 'r': return 43;
                case 's': return 44;
                case 't': return 45;
                case 'u': return 46;
                case 'v': return 47;
                case 'w': return 48;
                case 'x': return 49;
                case 'y': return 50;
                case 'z': return 51;
                case '0': return 52;
                case '1': return 53;
                case '2': return 54;
                case '3': return 55;
                case '4': return 56;
                case '5': return 57;
                case '6': return 58;
                case '7': return 59;
                case '8': return 60;
                case '9': return 61;
                case '.': return 62;
                case ',': return 63;
                case '!': return 64;
                case '?': return 65;
                case ':': return 66;
                case ';': return 67;
                case '(': return 68;
                case ')': return 69;
                case '[': return 70;
                case ']': return 71;
                case '{': return 72;
                case '}': return 73;
                case '<': return 74;
                case '>': return 75;
                case '-': return 76;
                case '+': return 77;
                case '=': return 78;
                case '/': return 79;
                case '\\': return 80;
                case '#': return 81;
                case '@': return 82;
                case '$': return 83;
                case '%': return 84;
                case '&': return 85;
                case '*': return 86;
                case '^': return 87;
                case '|': return 88;
                case '~': return 89;
                case ' ': return 90;
                case '\'': return 91;
                case '\n': return 92;
                default: return -10;
            }
        }
    }
}

