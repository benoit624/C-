using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    class Player : Character
    {
        public List<Explosion> explosions;
        public int bonus;
        public int score;
        public Player(string design, int x, int y)
            : base(design, x, y)
        {
            explosions = new List<Explosion>();
            this.score = 0;
            this.life = 50;
            color = ConsoleColor.Cyan;
        }
        public void update(int time, List<Enemy> characs)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(10, Console.WindowHeight - 1);
            Console.Write("    ");
            Console.SetCursorPosition(10, Console.WindowHeight - 1);
            Console.Write(this.score);
            Console.SetCursorPosition(Console.WindowWidth / 4 + 17, Console.WindowHeight - 1);
            Console.Write("    ");
            Console.SetCursorPosition(Console.WindowWidth / 4 + 17, Console.WindowHeight - 1);
            Console.Write(this.nb_shots);
            Console.SetCursorPosition(3 * Console.WindowWidth / 4 + 7, Console.WindowHeight - 1);
            Console.Write("    ");
            Console.SetCursorPosition(3 * Console.WindowWidth / 4 + 7, Console.WindowHeight - 1);
            Console.Write(this.bonus);
            Console.SetCursorPosition(2 * Console.WindowWidth / 4 + 6, Console.WindowHeight - 1);
            Console.Write("    ");
            Console.SetCursorPosition(2 * Console.WindowWidth / 4 + 6, Console.WindowHeight - 1);
            Console.Write(this.life);
            this.time = time;
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].update(time);
                if (explosions[i].r > 5)
                {
                    explosions.Remove(explosions[i]);
                }
            }
            for (int i = 0; i < this.shots.Count; ++i)
            {
                this.shots[i].update(time);
                if (shots.Count > i)
                    this.delete_shot(this.shots[i], this);
                if (shots.Count > i)
                    this.delete_shot(this.shots[i], characs);
                    
            }
            for (int i = 0; i < characs.Count; i++)
            {
                if (characs[i].life<=0)
                {
                    characs[i].death(this);
                    this.explosions.Add(new Explosion("·", characs[i].x + characs[i].length / 2, characs[i].y));
                    characs.Remove(characs[i]);
                    this.score += 10;
                }
            }
            this.mouvement();
            this.print();
        }

        public void mouvement()
        {
            ConsoleKeyInfo keyboard;
            if (Console.KeyAvailable == true)
            {
                keyboard = Console.ReadKey(true);
                if (keyboard.Key.Equals(ConsoleKey.Z))
                {
                    this.shoot("°", -1, 100);
                }
                if (keyboard.Key.Equals(ConsoleKey.S))
                {
                    if (this.bonus>0)
                    {
                        this.bonus--;
                        this.shoot("_", -1, 5);
                    }  
                }
                else if (keyboard.Key.Equals(ConsoleKey.D) && x < Console.WindowWidth-this.length-3)
                {
                    this.refresh(this.x + 1, this.y);
                }
                else if (keyboard.Key.Equals(ConsoleKey.Q) && x > 4)
                {
                    this.refresh(this.x - 1, this.y);
                }
            }
            
        }
    }
}
