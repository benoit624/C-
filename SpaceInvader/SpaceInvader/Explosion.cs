using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    class Explosion :Objet
    {
        public int r;
        public Explosion(string design,int x,int y):base(design,x,y)
        {
            r=1;
            color = ConsoleColor.Yellow;
        }
        public void update(int time)
        {
            if (r < 6)
            {
                if (time % 3 == 0)
                {
                    for (int i = 5; i < Console.WindowWidth - 5; i++)
                    {
                        for (int j = 1; j < Console.WindowHeight - 3; j++)
                        {
                            if (Math.Pow(i - this.x, 2) + Math.Pow(j - this.y, 2) <= Math.Pow(r, 2)+3 && Math.Pow(i - this.x, 2) + Math.Pow(j - this.y, 2) >= Math.Pow(r, 2)-3&& r<5)
                            {
                                Console.SetCursorPosition(i, j);
                                Console.ForegroundColor = this.color;
                                Console.Write(this.design);
                            }
                            else if (Math.Pow(i - this.x, 2) + Math.Pow(j - this.y, 2) <= Math.Pow(r + 1, 2)+3 && Math.Pow(i - this.x, 2) + Math.Pow(j - this.y, 2) >= Math.Pow(r - 1, 2)-3)
                            {
                                Console.SetCursorPosition(i, j);
                                Console.Write(" ");
                            }
                        }
                    }
                    r++;
                }
            }
            
        }
    }
}
