using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    class Character : Objet
    {
        public List<Shot> shots;
        protected int nb_shots;
        public int last_shot_time;
        public int life; // 23 maximum on the default console window
        public int time; // int containing the game time
        public Character(string design, int x, int y): base(design, x, y)
        {
            shots = new List<Shot>();// list containing all the ship’s active shots
            
            this.nb_shots = 0;
        }
        public void shoot(string design, int direction, int speed)
        {
            if(this.last_shot_time+speed<=this.time)
            {
                this.shots.Add(new Shot(direction, design,this.x+length/2,this.y+direction,"shot"));
                this.last_shot_time=this.time;
                this.nb_shots++;
                
            }
            
            
        }
        
        public void clear_shots()
        {
            for (int i = 0; i < this.shots.Count; i++)
            {
                Shot s = this.shots[i];
                Console.SetCursorPosition(s.x, s.y);
                Console.Write(s.refresh_str);
                shots.Remove(s);
            }
            Console.SetCursorPosition(this.x, this.y);
            Console.Write(this.refresh_str);
        }
        public void delete_shot(Shot shot, List<Enemy> enemies)
        {
            for (int i = 0; i < enemies.Count; ++i)
                if (shot.y == enemies[i].y && shot.x >= enemies[i].x
                && shot.x <= enemies[i].x + enemies[i].length)
                {
                    shot.collision = true;
                    enemies[i].life -= 10;
                    enemies[i].mvt(0);
                    Console.SetCursorPosition(enemies[i].x, enemies[i].y + 1);
                    Console.Write("   ");
                    Console.SetCursorPosition(enemies[i].x, enemies[i].y + 1);
                    Console.Write(enemies[i].life);
                }
            if ((shot.y < 2 && shot.direction == -1)
            || (shot.y > Console.WindowHeight - 5 && shot.direction == 1)
            || shot.collision)
            {
                Console.SetCursorPosition(shot.x, shot.y);
                Console.Write(shot.refresh_str);
                shots.Remove(shot);
            }
        }
        public void delete_shot(Shot shot, Player player)
        {
            if (shot.y == player.y && shot.x >= player.x
            && shot.x <= player.x + player.length)
            {
                shot.collision = true;
                /*dtection de lobjet rentré en contact avec le player*/
                switch (shot.type)
                {
                    case "shot":
                        player.life -= 10;
                        break;
                    case "bonus":
                        player.bonus += 50;
                        break;
                    case "vie":
                        player.life += 10;
                        break;
                    default:
                        break;
                }
            }
            if ((shot.y < 1 && shot.direction == -1)
            || (shot.y > Console.WindowHeight - 5 && shot.direction == 1)
            || shot.collision)
            {
                if(shot.x>=0 && shot.y>=0)
                    Console.SetCursorPosition(shot.x, shot.y);
                Console.Write(shot.refresh_str);
                shots.Remove(shot);
            }
        }
    }
}
