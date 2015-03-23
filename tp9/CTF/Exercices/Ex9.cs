using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class Ex9 : ExX
    {
        #region Attributes
        byte[] memory;
        int pointer;
        int ip;
        string output;
        string code;
        #endregion
        #region Constructor
        public Ex9()
        {
            name = "Ex9";
        }
        #endregion
        #region Methods
        public override string solve(string question)
        {
            memory = new byte[] { 0x00, 0x00, 0x00 };
            process_instr(question);
            return code;
        }
        private void process_instr(string str)
        {
            code = "";
            pointer = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '[')
                {
                    do
                    {
                        for (int ip = i; ip < str.Length && str[ip] != ']'; ip++)
                        {
                            trad(str[ip]);
                        }
                    } while (memory[pointer]>0);
                    i++;
                }
                trad(str[i]);
            }
        }
        private void trad(char cha)
        {
            switch (cha)
            {
                case '+':
                    memory[pointer]++;
                    break;
                case '-':
                    memory[pointer]--;
                    break;
                case '<':
                    if (pointer > 0)
                        pointer--;
                    break;
                case '>':
                    if (pointer < memory.Length)
                        pointer++;
                    break;
                case '.':
                    code += (char)memory[pointer];
                    break;

            }
        }
        #endregion
    }
}
