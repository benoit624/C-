using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class Ex6 : ExX
    {
        public Ex6()
        {
            name = "Ex6";
        }
        public override string solve(string question)
        {
            string rep = "";
            char test = question[3];
            List<int> list = new List<int>();
            for (int i=3; i <question.Length; i++)
            {
                while (i < question.Length && question[i] != test)
                {
                    rep += question[i];
                    i++;
                }
                if (rep != "")
                {
                    i--;
                    list.Add(Parse.string_to_int(rep));
                    rep = "";
                }
            }
            sort(list);
            rep = "";
            foreach (int li in list)
            {
                rep += test + Parse.int_to_string(li);
            }
            return rep;
        }
        private void sort(List<int> array)
        {
            for (int i = 1; i < array.Count; i++)
            {
                int j = 0;
                while (j < i)
                {
                    if (array[i] < array[j])
                    {
                        array[j] ^= array[i];
                        array[i] ^= array[j];
                        array[j] ^= array[i];
                    }
                    j++;
                }
            }
        }
    }

}


