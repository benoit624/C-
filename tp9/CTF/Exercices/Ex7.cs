using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class Ex7:ExX
    {
        #region Constructor
        public Ex7()
        {
            name = "Ex7";
        }
        #endregion
        #region Methods
        public override string solve(string question)
        {
            return process_instr(question);
        }
        private string process_instr(string str)
        {
            byte a = 0x00;
            string rep = "";
            for (int i = 0; i < str.Length; i++)
            {
                while (i < str.Length && str[i] != '.')
                {
                    switch (str[i])
                    {
                        case '+':
                            a++;
                            break;
                        case'-':
                            a--;
                            break;

                    }
                    i++;
                }
                if(i<str.Length && str[i]=='.')
                    rep += (char)a;
            }
            return rep;
        }
        #endregion
    }
}
