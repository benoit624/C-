using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPC3.Exercices._1___Basics
{
    class ex1
    {
        /// <summary>
        /// my_nbr <- 41
        /// 	tmp <-0
        /// 	tmp <- 164
        /// 	nb <- 42
        /// 	tmp <- 174 
        /// my_nbr <-42
        /// display "Function ex1 terminated."
        /// </summary>
        public static unsafe void run()
        {
            int my_nbr = 41;
            utils.fex1(&my_nbr);
            Console.WriteLine("Function ex1 terminated.");
        }
    }
}
