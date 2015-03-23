using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class Ex8 : ExX
    {
        #region Attributes
        byte[] memory;
        #endregion
        #region Constructor
        public Ex8()
        {
            name = "Ex8";
        }
        #endregion
        #region Methods
        public override string solve(string question)
        {
            memory = new byte[]{0x00,0x00,0x00};
            return process_instr(question);
        }
        private string process_instr(string str)
        {
            string rep = "";
            int j = 0;
            for (int i = 0; i < str.Length; i++)
            {
                while (i < str.Length && str[i] != '.')
                {
                    switch (str[i])
                    {
                        case '+':
                            memory[j]++;
                            break;
                        case '-':
                            memory[j]--;
                            break;
                        case '<':
                            if(j>0)
                                j--;
                            break;
                        case '>':
                            if (j < memory.Length)
                                j++;
                            break;

                    }
                    i++;
                }
                if (i < str.Length && str[i] == '.')
                    rep += (char)memory[j];
            }
            return rep;
        }
        #endregion
    }
}
