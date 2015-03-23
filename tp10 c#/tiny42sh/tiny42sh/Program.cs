using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tiny42sh
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[][] test = Interpreter.parse_input("ls ./ C:/");
            //Execution.execute_input(test);
            while (true)
                Execution.execute_input(Interpreter.parse_input(Console.ReadLine()));
        }
    }
}
