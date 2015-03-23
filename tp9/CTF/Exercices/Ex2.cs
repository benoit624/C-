using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class Ex2 : ExX
    {
        public Ex2()
        {
            name = "Ex2";
        }
        public override string solve(string question)
        {
            string rep = "";
            if (question.Length > 4)
            {
                char test = question[3];
                for (int i = 4; i < question.Length; i++)
                {
                    if (question[i] == test)
                    {
                        while (i + 1 < question.Length && question[i + 1] != test)
                        {
                            i++;
                            rep += question[i];
                        }
                        break;
                    }
                }
            }
            return rep;
        }
    }
}
