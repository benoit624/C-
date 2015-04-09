using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Sudoku
    {
        private int [,] grid;
        public Sudoku(bool ask=false)
        {
            grid = new int [9,9];
            Init(0);
            RandomlyFill(ask ? Convert.ToInt32(Console.ReadLine()) : new Random().Next(68));
        }
        public void Init (int  init)
        {
            for    (int i=0; i<9;i++)
            {
                for (int j=0 ; j<9 ; j++)
                {
                    grid[i,j] = init;
                }
            }
        }
        public void Print()
        {
            for    (int i=0; i<9;i++)
            {
                Console.Write("\n");
                if (i%3==0)
                {
                    Console.Write('+');
                    for (int j=0 ; j<7*3+2 ; j++)
                        Console.Write('-');
                    Console.Write("+\n");
                }
                for (int j=0 ; j<9 ; j++)
                {
                    if (j%3==0)
                        Console.Write("| ");
                    Console.Write(grid[i,j] + " ");
                }
                Console.Write('|');
                
            }
            Console.Write("\n+");
            for (int j=0 ; j<7*3+2 ; j++)
                Console.Write('-');
            Console.Write("+\n");
        }
        public void RandomlyFill(int nb)
        {
            Random rnd = new Random();
            while(nb>0)
            {
                int x = rnd.Next(9), y = rnd.Next(9);
                if (grid[x, y] == 0)
                {
                    List<int> nbs = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        if (grid[i, y] != 0 && i / 3 != x / 3)
                            nbs.Remove(grid[i, y]);
                    }
                    for (int i = 0; i < grid.GetLength(1); i++)
                    {
                        if (grid[x, i] != 0 && i / 3 != y / 3)
                            nbs.Remove(grid[x, i]);
                    }
                    for (int i = x - x % 3; i < x - x % 3 + 3; i++)
                    {
                        for (int j = y - y % 3; j < y - y % 3 + 3; j++)
                        {
                            if (grid[i, j] != 0)
                                nbs.Remove(grid[i, j]);
                        }
                    }
                    int test = rnd.Next(nbs.Count);
                    if (nbs.Count > 0)
                    {
                        grid[x, y] = nbs.ElementAt(test);
                        nb--;
                    }    
                } 
            }
        }
        public bool solve()
        {
            Console.WriteLine("Do you want tio save ? (y/n)");
            if(Console.ReadLine()=="y")
                return IO.SaveFile(grid)? true :solve();
            return false;
        }
    }
}