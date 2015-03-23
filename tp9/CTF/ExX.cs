using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    abstract class ExX
    {
        protected string name;
        protected const string login = "forget_c";
        protected const string error = "KO:Fail!";
        public ExX()
        {
        }
        public abstract string solve(string question);
        public string format(string msg)
        {
            msg = login + "|" + name + ":" + msg;
            return msg;
        }
    }
}
