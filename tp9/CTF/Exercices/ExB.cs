using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class ExB : ExX
    {
        private int[] test;
        private int pointer;
        public ExB()
        {
            name = "ExB";
            test = new int[2];
            pointer = 0;
        }
        public override string solve(string question)
        {
            if (pointer < 2)
            {
                question=question.Substring(3);
                if (question.Length < 5)
                {
                    test[pointer] = Parse.string_to_int(question);
                    pointer++;
                }
            }
            if (pointer >= 2)
                return Parse.int_to_string(test[1] * 2 - test[0]);
            return "0";
        }
    }
}
