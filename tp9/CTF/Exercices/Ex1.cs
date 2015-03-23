using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class Ex1:ExX
    {
        public Ex1()
        {
            name = "Ex1";
        }
        public override string solve(string question)
        {
            string rep = "";
            for (int i = 0; i < question.Length; i++)
            {
                if (question[i] == ';')
                {
                    while (i+1 < question.Length && question[i+1] != ';')
                    {
                        i++;
                        rep += question[i];
                    }
                    break;
                }
            }
            return  rep;
        }
    }
}
