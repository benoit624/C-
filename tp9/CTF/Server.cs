using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    public static class Server
    {
        public static string solve_1(string answer)
        {
            int nb = 0;
            try
            {
                nb = int.Parse(answer);
            }
            catch 
            {
                return "KO:Wrong integer format";
            }
            string[] tab = { "KO:You lose!", "KO:Nope!", "KO:LOLNO", "OK:GGWP" };
            string validation = "KO:Nice Tried!";
            if (nb >= tab.Length - 1)
                return validation;
            for (int i = 0; i < tab.Length && nb != 0; i++, nb--)
                validation = tab[i];
            return validation;
        }
        public static void solve_2(string answer, ref int secret, int incr)
        {
            int nb = 0;
            try
            {
                nb = int.Parse(answer);
            }
            catch
            {
                Console.WriteLine( "KO");
            }
            int current = secret;
            secret += incr;
            if (current == nb)
                Console.WriteLine( "OK:EZ");
            else
                Console.WriteLine(current);
        }
    }
}
