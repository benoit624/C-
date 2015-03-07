using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_arrays
{
    class Program
    {
        static void swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
        static int mintab(int[] tab, ref int min)
        {
            int n = -1;
            for (int i = 0; i < tab.Length; i++)
            {
                if ((tab[i] < min) || (n==-1))
                {
                    min = tab[i];
                    n = i;
                }
            }
            return n;
        }
        static int maxtab(int[] tab, ref int max)
        {
            int n = -1;
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] > max || (n == -1))
                {
                    max = tab[i];
                    n = i;
                }
            }
            return n;
        }
        static int bubblesort(int[] tab)
        {
            bool swapped;
            int h = 0;
            do
            {
                swapped = false;
                for (int i = 0; i < tab.Length - 1; i++)
                {
                    afftab(tab);
                    h++;
                    if (tab[i] > tab[i + 1])
                    {
                        swap(ref tab[i], ref tab[i + 1]);
                        swapped = true;
                    }
                }
            } while (swapped == true);
            return h;
        }
        private static int part(int[] tab, int first, int last, int pivot)
        {
            int j;
            swap(ref tab[pivot], ref tab[last]);
            j = first;
            for (int i = first; i < last - 1; i++)
            {
                if (tab[i] <= tab[last])
                {
                    swap(ref tab[i], ref tab[j]);
                    j++;
                }
            }
            swap(ref tab[j], ref tab[last]);
            return j;
        }
        private static int heapsort(int [] tab)
        {
            int length=tab.Length,h=0;
            for (int i = 0; i < length; i++)
			{
                afftab(tab);
                int n = minheapsort(tab,i,ref h);
                swap(ref tab[i], ref tab[n]);
			}
            return h;
            
        }
        static int minheapsort(int[] tab,int j,ref int h)
        {
            int n = -1,min=0;
            for (int i = j; i < tab.Length; i++)
            {
                h++;
                if ((tab[i] < min) || (n == -1))
                {
                    min = tab[i];
                    n = i;
                }
            }
            return n;
        }
        static void afftab(int[] tab)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                Console.Write(tab[i] + " ");
            }
            Console.WriteLine();
        }
        private static void aff(int pos,int value)
        {
            Console.WriteLine("value is equal to {0} and his position is {1}",value,pos);
        }
        private static int[] gen(int n)
        {
            int[] tab = new int[n] ;
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                tab[i] = rnd.Next(100);
            }
            return tab;
        }
        static void Main(string[] args)
        {
            int[] tab = gen(10);
            int min=0;
            afftab(tab);
            aff(mintab(tab, ref min), min);
            aff(maxtab(tab, ref min), min);
            Console.WriteLine("speed: "+heapsort(gen(10)));
            Console.WriteLine("speed: " +bubblesort(gen(10)));
            afftab(tab);
            heapsort(tab);
            afftab(tab);
        }
    }
}
