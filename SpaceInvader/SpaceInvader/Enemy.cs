using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    class Enemy : Character
    {
        public int speed;
        public Random r;
        public Enemy(string design, int x, int y,int life ,int speed)
            : base(design, x, y)
        {
            this.life = life;
            r=new Random();
            this.speed = speed;
            this.color = ConsoleColor.Magenta;
        }
        public void update(int time, Player player)//constructeur modifié
        {
            this.time = time;
            this.shoot("|", 1, this.speed);
            for (int i = 0; i < this.shots.Count; ++i)
            {
                this.shots[i].update(time);
                this.delete_shot(this.shots[i],player);    
            }
            this.move();
            this.print();
         }
        public void death(Player player)
        {
            this.clear_shots();
            switch (this.r.Next(4))
            {
                case 0:
                    player.shots.Add(new Shot(1, "|X|", this.x + length / 2, this.y + 1, "bonus"));
                    break;
                case 1:
                    player.shots.Add(new Shot(1, "|0|", this.x + length / 2, this.y + 1, "vie"));
                    break;
                default:
                break;
            }
        }
        public void move()
        {
            if (this.time % 400 == 0)//1200
            {
                this.mvt(1);
            }
            
        }
        public void mvt(int i)
        {
            if (r.Next(2) == 1)
                this.refresh(x - 1, y + i);
            else
                this.refresh(x + 1, y + i);
        }
    }
}
