using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class Ex5 : ExX
    {
        public Ex5()
        {
            name = "Ex5";
        }
        public override string solve(string question)
        {
            string rep = "";
            for (int i = question.Length-1; i > 1; i--)
            {
                if (i==2 || question[i] == ' ')
                {
                    int j = i+1;
                    while (question.Length > j && question[j] != ' ')
                    {
                        rep += question[j];
                        j++;
                    }
                    if(i!=2)
                        rep += ' ';
                }       
            }
            return rep;
        }
    }
}
