using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class ExC : ExX
    {
        public int a;
        private int[] borne;
        public ExC()
        {
            a = 50;
            borne = new int[2] { 0, 100 };
            name = "ExC";
        }
        public override string solve(string question)
        {
            if (question=="+")
            {
                borne[0] = a;
                a=(borne[1]+a)/2;
            }
            else if(question=="-")
            {
                borne[1] = a;
                a = (borne[0] + a) / 2;
            }
            return int_to_string(a);
        }
        private int string_to_int(string str)
        {
            int result = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                result += (int)(str[i] - 48) * (int)Math.Pow(10, str.Length - 1 - i);
            }
            return result;
        }
        private string int_to_string(int a)
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
