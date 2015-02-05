using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class Program
    {
        public static string xor_encode(string str, string key)
        {
            string str2 = "";
            for (int i = 0; i < str.Length; i++)
            {
                    str2 += (char)(str[i] ^ key[i % key.Length]) ;
            }
            return str2;
        }
        public static string rotN(string str, int n)
        {
            string str2 = "";
            for (int i = 0; i < str.Length; i++)
            {
                char a = str[i];
                if (a>96 && a<123)
                {
                    a = (char)((a - 95 + n) % 26 + 95);
                }
                else if (a > 64 && a < 91)
                {
                    a = (char)((a - 64 + n) % 26 + 64);
                }
                str2 += a;
            }
            return str2;
        }
        public static string vignere_encode(string message, string key)
        {
            string str2 = "";
            for (int i = 0; i < message.Length; i++)
            {
                char a = message[i];
                if (a > 96 && a < 123)
                {
                    a = (char)((message[i] - 95 + key[i % key.Length] - 95) % 26 + 95);
                }
                else if (a > 64 && a < 91)
                {
                    a = (char)((message[i] - 64 + key[i % key.Length] - 64) % 26 + 64);
                }
                str2 += a;
            }
            return str2;
        }
        public static string vignere_decode(string message, string key)
        {
            string str2 = "";
            for (int i = 0; i < message.Length; i++)
            {
                char a = message[i];
                int sup=0;
                if (key[i % key.Length] > message[i])
                    sup = 26;
                if (a > 96 && a < 123)
                {
                    a = (char)((message[i] - key[i % key.Length]+sup) % 26 + 95);
                }
                else if (a > 64 && a < 91)
                {
                    a = (char)((message[i] - key[i % key.Length]) % 26 + 64);
                }
                str2 += a;
            }
            return str2;
        }

        static void Main(string[] args)
        {
            string test = xor_encode("coucou", "test");
            Console.WriteLine(test);
            Console.WriteLine(xor_encode(test, "test"));
            Console.WriteLine(rotN("AZaz coucou!?", 3));
            Console.WriteLine(vignere_encode("coucou", "a"));
            Console.WriteLine(vignere_encode(vignere_decode("coucou", "aze"), "aze"));
            char[]teste =new char[]{'a','z','e','r','t','y','u','i','o','p','q','s','d','f','g','h','j','k','l','m','w','x','c','v','b','n'};
            Console.WriteLine(encode_Monoalphabetique("abcde", teste));
            Console.WriteLine(decode_Monoalphabetique(encode_Monoalphabetique("abcde", teste),teste));
            Console.ReadLine();
        }
        public static string encode_Monoalphabetique(string message, char[] alpha)
        {
            string str2 = "";
            if (alpha.Length==26)
            {
                for (int i = 0; i < message.Length; i++)
                {
                    int a = 0;
                    char b = message[i];
                    if (b > 96 && b < 123)
                    {
                        a = b - 97;
                        str2 += alpha[a];
                    }
                    else if (b > 64 && b < 91)
                    {
                        a = b - 65;
                        str2 += alpha[a];
                    }
                    else
                    {
                        str2 += b;
                    }
                    
                }
            }
            return str2;
        }
        public static string decode_Monoalphabetique(string message, char[] alpha)
        {
            string str2 = "";
            if (alpha.Length == 26)
            {
                for (int i = 0; i < message.Length; i++)
                {
                    int a = -1;
                    char b = message[i];
                    if (b > 96 && b < 123)
                    {
                        int j = 0;
                        do
                        {
                            if (b == alpha[j])
                                a = j + 97;
                            j++;
                        } while (a == -1);
                        str2 += (char)a;
                    }
                    else
                    {
                        str2 += b;
                    }

                }
            }
            return str2;
        }
    }
}
