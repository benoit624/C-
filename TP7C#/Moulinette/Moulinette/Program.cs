using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moulinette
{
    class Program
    {
        static void Main(string[] args)
        {
            Moulinette moulinette = new Moulinette();
            if (moulinette.init())
            {
                moulinette.execute();
            }
        }
    }
}
