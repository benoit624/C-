using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TPC3.Exercices._3___Advanced
{
    public enum state
    {
        ENDED,
        RUNNING,
        OK,
        WRONG,
        WON,
    }

    class bomb_interface
    {
        bomb my_bomb;

        public bomb_interface()
        {
            my_bomb = new bomb();
            play();
        }

        private void play()
        {
            Console.WriteLine("Let's start a little discussion with the holly bomb\n"     +
                              "Your role is to input one digit [0-9] or one character \n" +
                              "that you believe is correct to diffuse the bomb, each\n"   +
                              "good input will bring you some light and may guide you\n"  +
                              "through the victory, but take cares because each wrong\n"  +
                              "input will bring you closer to death (Tac)... Good luck\n");

            while (my_bomb.pcnUe == state.RUNNING  ||
                   my_bomb.pcnUe == state.OK       ||
                   my_bomb.pcnUe == state.WRONG)
            {
                Console.Write("You {0} : ", (my_bomb.Ojcei == 1) ? "[0-9]" : "(string)");
                my_bomb.input(Console.ReadLine());
                Console.WriteLine("bomb : {0}", my_bomb.f_Kbce());
            }
        }
    }

    public class bomb
    {
        public int OIde { get; private set; }
        public int Ojcei { get; private set; }
        public state pcnUe { get; private set; }
        private int pocIU;
        private int dwLk;
        private int[] cwoeKs;
        private Random Lrnd;

        public bomb()
        {
            Lrnd = new Random();
            OIde = 4;
            pcnUe = state.RUNNING;
            dwLk = 4;
            Ojcei = 2;
            pocIU = 2;
            cwoeKs = new int[dwLk];
            for (int i = 0; i < dwLk; ++i)
                cwoeKs[i] = Lrnd.Next() % 10;
            dwLk = 0;
        }

        public string f_Kbce()
        {
            switch (pcnUe)
            {
                case state.OK:
                    pcnUe = state.RUNNING;
                    return "Tic";
                case state.WRONG:
                    pcnUe = state.RUNNING;
                    return "Tac";
                case state.RUNNING:
                    return "Bomb is still running";
                case state.ENDED:
                    return "BOUM! You're dead, keep trying!";
                default:
                    return "You WON, great job!";
            }
        }

        public void input(string s)
        {
            if (pcnUe == state.RUNNING)
            {
                switch (Ojcei)
                {
                    case 1:
                        f_owieh(int.Parse(s[0].ToString()));
                        break;
                    case 2:
                        f_uewTe(s);
                        break;
                    default:
                        break;
                }
                f_KsnW();
            }
        }

        private void f_owieh(int n)
        {
            if (n == cwoeKs[dwLk])
            {
                pcnUe = state.OK;
                dwLk++;
                if (dwLk > cwoeKs.Length - 1)
                    f_Ojceii();
            }
            else
                pcnUe = state.WRONG;
        }

        private void f_uewTe(string s)
        {
            if (f_weeD(s, Path.GetDirectoryName(Directory.GetCurrentDirectory())))
                f_Ojceii();
            else
                pcnUe = state.WRONG;
        }

        private bool f_weeD(string s1, string s2)
        {
            string key = "I can smell your fear";
            if (s1.Length != s2.Length)
                return false;
            if (s2.Length < 2)
                return f_fiReV(s1[0], s2[0], key);
            return f_fiReV(s1[0], s2[0], key) && f_weeD(s1.Substring(1), s2.Substring(1));
        }

        private bool f_fiReV(char c1, char c2, string k)
        {
            return ((c1 ^ k[c2 % k.Length]) % 20) == 19;
        }

        private void f_Ojceii()
        {
            Console.WriteLine("Ojcei {0} passed !",Ojcei++);
            pcnUe = state.RUNNING;
            if (Ojcei > pocIU)
                f_Nwhwo();
        }

        private void f_KsnW()
        {
            if (pcnUe == state.WRONG)
                OIde--;
            if (OIde <= 0)
                pcnUe = state.ENDED;
        }

        private void f_Nwhwo()
        {
            pcnUe = state.WON;
        }

        private void f_zheus()
        {
            Ojcei = 0;
            OIde = 0;
            pcnUe = state.ENDED;
        }
    }
}
