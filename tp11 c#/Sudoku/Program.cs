using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int sec = 2403,
                mins = -10,
                hours = 23,
                days = 1;
            Console.WriteLine(days + ":" + hours + ":" + mins + ":" + sec);
            Console.WriteLine(Stuff.TimeAfterTime(ref days,ref hours, ref mins, ref sec) ? days + ":" + hours + ":" + mins + ":" + sec : "false");
            Console.WriteLine(Stuff.Compression("le grand chien bleu hurle le ciel hurle avec le chien grand bleu orage"));
            Console.WriteLine(Stuff.Decompression(Stuff.Compression("le grand chien bleu hurle le ciel hurle avec le chien grand bleu orage")));
            int[,] tab = new int[9, 9];
            //IO.LoadFile(tab);
            Sudoku sud = new Sudoku();
            //sud.Init(0);
            //sud.Print();
            //sud.RandomlyFill(68);
            sud.Print();
            sud.solve();
            //IO.SaveFile(tab);
            Console.ReadLine();
        }
    }
}
