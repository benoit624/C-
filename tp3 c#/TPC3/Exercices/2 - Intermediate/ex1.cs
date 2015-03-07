using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPC3.Exercices._2___Intermediate
{
    class ex1
    {
        public static unsafe void run()
        {
            string str1 = "EPITA";
            string str2 = "Gimme a 9";
            string str3 = "Program testing can be used to show the presence of bugs," +
                          "but never to show their absence! -- Edsger Dijkstra";

            fixed (char *ptr1 = str1) // Jedi mode : You don't see this line.  -- Marwan Burelle
            {
                Console.WriteLine("The length of the string :\n\"{0}\" is {1}\n",
                    *ptr1, strings.mystrlen(ptr1));
            }
            fixed (char* ptr2 = str2)
            {
                Console.WriteLine("The length of the string :\n\"{0}\" is {1}\n",
                    *ptr2, strings.mystrlen(ptr2));
            }
            fixed (char* ptr3 = str3)
            {
                Console.WriteLine("The length of the string :\n\"{0}\" is {1}\n",
                    *ptr3, strings.mystrlen(ptr3));
            }
        }
    }
}
