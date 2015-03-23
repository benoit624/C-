using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class Parse
    {
        public static int string_to_int(string str)
        {
            int result = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                result += (int)(str[i] - 48) * (int)Math.Pow(10, str.Length - 1 - i);
            }
            return result;
        }
        public static string int_to_string(int a)
        {
            string str = "";
            while (a != 0)
            {
                str = a % 10 + str;
                a /= 10;
            }
            return str;
        }
    }
}
