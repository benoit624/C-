using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_refs
{
    class Program
    {
        static void add(ref float result, float value)
        {
            result += value;
        }
        static void sub(ref float result, float value)
        {
            result -= value;
        }
        static void mul(ref float result, float value)
        {
            result *= value;
        }
        static bool div(ref float result, float value)
        {
            if (value==0)
            {
                return false;
            }
            else
            {
                result /= value;
                return true;
            }
            
        }
        static bool pow(ref float result, int p)
        {
            if (p < 0)
                return false;
            else
            {
                result = (float)Math.Pow(result, p);
                return true;
            }
        }
        static void arit(ref float Un, float r, int n)
        {
            Un += n * r;
        }
        static void geom(ref float Un, float q, int n)
        {
                if (pow(ref q, n))
                    Un *= q;
        }
        static void swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
        private static void affbool(bool boolean, float value)
        {
            if (boolean)
            {
                Console.Write("return ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("true");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(", result is equal to ");              
            }
            else
            {
                Console.Write("return ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("false");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" so :( result is equal to ");
            }
            Console.ForegroundColor=ConsoleColor.Cyan;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
            
        }
        private static void aff(float value)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("result is equal to ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        static void Main(string[] args)
        {
            float result = 3;
            int a = 3, b = 2;
            add(ref result, 3);
            aff(result);
            sub(ref result, 4);
            aff(result);
            mul(ref result, 3);
            aff(result);
            affbool(div(ref result, 3),result);
            affbool(pow(ref result, -3), result);
            affbool(pow(ref result, 3), result);
            arit(ref result, 5, 3);
            aff(result);
            geom(ref result, 5, 3);
            aff(result);
            Console.WriteLine("a = " + a);
            Console.WriteLine("b = " + b);
            swap(ref a, ref b);
            Console.WriteLine("a = " + a);
            Console.WriteLine("b = " + b);
            Console.ReadLine();
        }
    }
}
