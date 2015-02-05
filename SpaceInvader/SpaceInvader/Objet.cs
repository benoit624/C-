using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
        
    class Objet
    {
        public string design;
        public int x;
        public int y;
        public string refresh_str;
        public int length;
        protected ConsoleColor color;//colors of design
        public Objet(string design, int x, int y)
        {
            length = design.Length;
            this.design = design;
            this.x = x;
            this.y = y;
            this.refresh_str=this.fill_refresh_str();
            color = ConsoleColor.White;
        }
        public string fill_refresh_str()
        {
            string str = "";
            for (int i = 0; i < this.design.Length; i++)
            {
                str += " ";
            }
            return str;
        }
        public void refresh(int x,int y)
        {
            if (this.y > 0 && this.x > 0 && this.y < Console.WindowHeight-3 && this.x < Console.WindowWidth-2-length)
            {
                Console.SetCursorPosition(this.x, this.y);
                Console.Write(refresh_str);
                this.x = x;
                this.y = y;
                this.print();
            }
        }
        public void print()
        {
            if (this.y > 0 && this.x > 0 && this.y < Console.WindowHeight - 3 && this.x < Console.WindowWidth - length-2)
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(x, y);
                Console.Write(design);
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
    }
}
