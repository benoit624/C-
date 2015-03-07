using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moulinette
{
    class Program
    {
        static void Main(string[] args){
            Moulinette moulinette = new Moulinette();
            if (moulinette.init())
                moulinette.execute();
        }
    }
}
