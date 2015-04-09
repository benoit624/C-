using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Sudoku
{
    class IO
    {
        public static bool SaveFile(int[,] tab)
        {
            //Console.Write("Which name for the Sudoku to save : ");
            //string Filename = Console.ReadLine();
            //if (!File.Exists(Filename))
            //{
            //    FileStream Sudo = File.Open(Filename, FileMode.Create);
            //    for (int i = 0; i < tab.GetUpperBound(0)+1; i++)
            //    {
            //        for (int j = 0; j < tab.GetUpperBound(1)+1; j++)
            //        {
            //            Sudo.WriteByte((byte)(tab[i, j] + 0x30));
            //        }
            //    }
            //    Sudo.Close();
            //    return true;
            //}
            //return false;
            Console.Write("Which name for the Sudoku to save : ");
            string Filename = Console.ReadLine();
            if (!File.Exists(Filename))
            {
                FileStream Sudo = File.Open(Filename, FileMode.Create);
                        for(int i=0; i<9;i++)
            {
                Sudo.WriteByte((byte)('\n'));
                if (i%3==0)
                {
                    Sudo.WriteByte((byte)('+'));
                    for (int j=0 ; j<7*3+2 ; j++)
                        Sudo.WriteByte((byte)('-'));
                    Sudo.WriteByte((byte)('+'));
                    Sudo.WriteByte((byte)('\n'));

                }
                for (int j=0 ; j<9 ; j++)
                {
                    if (j%3==0)
                    {
                        Sudo.WriteByte((byte)('|'));
                        Sudo.WriteByte((byte)(' '));
                    }  
                    Sudo.WriteByte((byte)(tab[i, j] + 0x30));
                    Sudo.WriteByte((byte)(' '));
                }
                Sudo.WriteByte((byte)('|'));
                
            }
            Sudo.WriteByte((byte)('+'));
            Sudo.WriteByte((byte)('\n'));
            for (int j=0 ; j<7*3+2 ; j++)
                Sudo.WriteByte((byte)('-'));
            Sudo.WriteByte((byte)('\n'));
            Sudo.WriteByte((byte)('+'));
                Sudo.Close();
                return true;
            }
            return false;
        }
        private static bool FileToTab(string Filename,int [,] tab)
        {
            if (File.Exists(Filename))
            {
                FileStream Sudo = File.Open(Filename, FileMode.Open);
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        byte b = 0x01;
                        while((b<0x30 || b>0x39 ) && b!=0x00)   
                            b = (byte)Sudo.ReadByte();
                        if (b == 0x00)
                        {
                            Sudo.Close();
                            return false;
                        }  
                        tab[i, j] = (int)b - 0x30;
                        Console.Write(tab[i, j]);
                    }
                }
                Sudo.Close();
                return true;
            }
            return false;
        }
        public static void LoadFile(int[,] tab)
        {
            tab = new int[9, 9];
            Console.Write("Which name for the Sudoku to load : ");
            string Filename = Console.ReadLine();
            if (!FileToTab(Filename,tab))
                LoadFile(tab);
        }
    }
}
