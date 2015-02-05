using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    class Shot :Objet
    {
        public int direction;
        public bool collision;
        public string type;
        public Shot( int direction,string design,int x,int y,string type):base(design,x,y)
        {
            this.direction = direction;
            y = y + direction;
            this.type = type;
            color = ConsoleColor.DarkGreen;
        }
        public void update(int time)
        {
            if (time%5==0)
                this.refresh(this.x, this.y + direction);
        }
    }
}
