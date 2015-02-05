using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    class Program
    {
        private static int get_param(ref string player_design, ref string enemy_design)
        {
            int number = -1;
            Console.Write("Enter your player design :");
            player_design = Console.ReadLine();
            Console.Write("Enter your enemy design :");
            enemy_design = Console.ReadLine();
            do
            {
                Console.WriteLine("Choose your difficulty : ");
                string value = Console.ReadLine();
                try
                {
                     number=Int32.Parse(value);
                }
                catch (Exception)
                {
                    Console.WriteLine("difficulty impossible");         
                }
            } while (number<1);

            return number;
        }
        /*
         *****************PLATEAU DE JEU*********************************
         */
        public static void environment()
        {
            Console.SetCursorPosition(3, 0);
            Console.Write("┌");
            Console.SetCursorPosition(Console.WindowWidth - 3, 0);
            Console.Write("┐");
            Console.SetCursorPosition(3, Console.WindowHeight - 3);
            Console.Write("└");
            Console.SetCursorPosition(Console.WindowWidth - 3, Console.WindowHeight - 3);
            Console.Write("┘");
            for (int i =4; i < Console.WindowWidth-3; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("─");
                Console.SetCursorPosition(i, Console.WindowHeight - 3);
                Console.Write("─");
            }
            for (int i = 1; i < Console.WindowHeight - 3; i++)
            {
                Console.SetCursorPosition(3, i);
                Console.Write("│");
                Console.SetCursorPosition( Console.WindowWidth - 3,i);
                Console.Write("│");
            }
            Console.SetCursorPosition(3, Console.WindowHeight-1);
            Console.Write("score:");
            Console.SetCursorPosition(Console.WindowWidth/4, Console.WindowHeight-1);
            Console.Write("number of shoot: ");
            Console.SetCursorPosition(2 * Console.WindowWidth / 4+1, Console.WindowHeight-1);
            Console.Write("life: ");
            Console.SetCursorPosition(3 * Console.WindowWidth / 4, Console.WindowHeight-1);
            Console.Write("bonus: ");
        }
        public static void play()
        {
            string a = "|-|", b = "/-\\";
            int diff = get_param(ref a, ref b);
            Console.Clear();
            Player p2 = new Player(a, Console.WindowWidth / 2 - 3, Console.WindowHeight - 4);
            List<Enemy> enemies = new List<Enemy>();
            int width = 1;
            while (p2.life > 0)
            {
                width++;
                /*boucle de création d'enemies*/
                for (int i = 1; i < width; i++)
                {
                    enemies.Add(new Enemy(b, i * (Console.WindowWidth - 5) / width, i % 3 + 1, width * 10, 1000 / diff));
                }
                int j = 0;
                /* dessin du plateau */
                environment();
                /*boucle des vagues d'enemies*/
                while (p2.life > 0 && enemies.Count > 0)
                {
                    j++;
                    System.Threading.Thread.Sleep(10);
                    p2.update(j, enemies);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].y > Console.WindowHeight - 5)
                            p2.life = 0;
                        enemies[i].update(j, p2);
                    }
                }
                /*ecran de pause entre deux vagues*/
                Console.Clear();
                Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2);
                if (p2.life <= 0)
                    Console.Write("YOU LOOSE THE WAVE NUMBER {0}!!", width - 1);
                else
                    Console.Write("YOU WIN THE WAVE NUMBER {0}!!", width - 1);
                Console.Read();
                p2.time = 0;
                p2.last_shot_time = p2.time;
                Console.Clear();

            }
            /*ecran de fin*/
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2);
            Console.Write("YOUR SCORE IS {0}!!", p2.score);
            System.Threading.Thread.Sleep(3000);
        }
        static void Main(string[] args)
        {
            play();

        }
    }
}
