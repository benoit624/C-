using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class Ex4 : ExX
    {
        public Ex4()
        {
            name = "Ex4";
        }
        public override string solve(string question)
        {
            string rep = "";
            string rep2 = "";
            int a = 0;
            if (question.Length > 3)
            {
                char test = question[3];
                int i=3;
                while (i + 1 < question.Length && question[i + 1] != test)
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
                switch (test)
                {
                    case '+':
                        a = Parse.string_to_int(rep) + Parse.string_to_int(rep2);
                        break;
                    case '-':
                        a = Parse.string_to_int(rep) - Parse.string_to_int(rep2);
                        break;
                    case '*':
                        a = Parse.string_to_int(rep) * Parse.string_to_int(rep2);
                        break;
                    case '/':
                        a = Parse.string_to_int(rep) / Parse.string_to_int(rep2);
                        break;
                    case '%':
                        a = Parse.string_to_int(rep) % Parse.string_to_int(rep2);
                        break;
                    default:
                        break;
                }
            }
            return Parse.int_to_string(a);
        }
    }
}
