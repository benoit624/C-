using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPC3.Exercices._1___Basics
{
    class ex2
    {
        /// <summary>
        /// my_nbr <- 42
        /// 	nb <- 42
        /// 	nb <- 52008
        /// 	nb <- 0
        /// my_nbr <- 0
        /// display "Function ex2 terminated."
        /// </summary>
        public static unsafe void run()
         {
            int my_nbr = 42;
            utils.fex2(&my_nbr);
            Console.WriteLine("Function ex2 terminated.");
        }
    }
}
