using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class Ex3 : ExX
    {
        public Ex3()
        {
            name = "Ex3";
        }
        public override string solve(string question)
        {
            string rep = "";
            string rep2 = "";
            int i = 2;
            while (i + 1 < question.Length && question[i + 1] != '+')
            {
                i++;
                rep += question[i];
            }
            i++;
            while (i + 1 < question.Length)
            {
                i++;
                rep2 += question[i];
            }
            int a = Parse.string_to_int(rep) + Parse.string_to_int(rep2);
            return Parse.int_to_string(a);
        }

    }
}
