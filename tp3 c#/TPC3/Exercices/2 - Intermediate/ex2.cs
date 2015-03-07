using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPC3.Exercices._2___Intermediate
{
    class ex2
    {
        public static unsafe void run()
        {
            string to_be_copied = "There is a bar at foo square";
            fixed (char* ptr1 = to_be_copied)
            {
                sbyte* buffer = strings.mystrcpy(ptr1);
                string copied_string = new string(buffer);
                Console.WriteLine(copied_string);
            }
        }
    }
}
