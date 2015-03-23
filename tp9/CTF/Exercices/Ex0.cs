using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class Ex0 : ExX
    {
        public Ex0()
        {
            name = "Ex0";
        }
        public override string solve(string question)
        {
            
            if ("OK:Hello World!" == question)
                return "Salut le Monde !";
            return "";
        }
    }
}
