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
        static void div(ref float result, float value)
        {
            result /= value;
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
            for (int i = 0; i < n; i++)
            {
                Un += r;
            }
        }
        static void geom(ref float Un, float q, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Un *= q;
            }
        }
        static void swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
        static int mintab(int[] tab, ref int min)
        {
            int tmp = min,n=-1;
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] < min || (min < tab[i] && tmp == min))
                {
                    min = tab[i];
                    n = i;
                }
            }
            return n;
        }
        static int maxtab(int[] tab, ref int max)
        {
            int tmp = max, n = -1;
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] > max || (max > tab[i] && tmp == max))
                {
                    max = tab[i];
                    n = i;
                }
            }
            return n;
        }
        static void bubblesort(int[] tab)
        {

        }
        static void Main(string[] args)
        {
            float result = 3;
            int [] tab=new int []{3,2,1,4,5};
            add(ref result, 3);
            Console.WriteLine(result);
            sub(ref result, 4);
            Console.WriteLine(result);
            mul(ref result, 3);
            Console.WriteLine(result);
            div(ref result, 3);
            Console.WriteLine(result);
            if (!pow(ref result, -3))
                Console.WriteLine("false");
            if (pow(ref result, 3))
                Console.WriteLine(result);
            arit(ref result,5, 3);
            Console.WriteLine(result);
            geom(ref result, 5, 3);
            Console.WriteLine(result);
            int a=3,b=2;
            Console.WriteLine("a = " + a);
            Console.WriteLine("b = " + b);
            swap(ref a, ref b);
            Console.WriteLine("a = " + a);
            Console.WriteLine("b = " + b);
            bubblesort(tab);
        }
    }
}
