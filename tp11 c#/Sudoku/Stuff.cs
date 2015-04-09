using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    static class Stuff
    {
        public static bool TimeAfterTime(ref int days, ref int hours, ref int mins, ref int sec)
        {
            mins += sec / 60;
            sec %= 60;
            hours += mins / 60;
            mins %= 60;
            days += hours / 24;
            hours %= 24;
            return days >= 0 && hours >= 0 && mins >= 0 && sec >= 0;
        }
        public static string Compression(string source)
        {
            Dictionary<string,int> keyword = new Dictionary<string,int>();
            string word = "", compress = "";
            int j = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == ' ' || i==source.Length-1)
                {
                    if (i == source.Length - 1)
                        word += source[i];
                    j++;
                    int nb;
                    if (keyword.TryGetValue(word, out nb))
                        compress += Convert.ToString(nb);
                    else
                    {
                        keyword.Add(word, j);
                        compress += word;
                    }
                    word = "";
                    compress += ' ';
                }   
                else
                    word += source[i];
            }
            return compress;
        }
        public static string Decompression(string source)
        {
            Dictionary<string, int> keyword = new Dictionary<string, int>();
            string word = "", compress = "";
            int j = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == ' ' || i == source.Length - 1)
                {
                    if (i == source.Length - 1)
                        word += source[i];
                    j++;
                    if (word.Length > 0 && word[0] > 47 && word[0] < 58)
                    {
                        compress += keyword.Where(test => test.Value == int.Parse(word)).First().Key;
                    }
                    else
                    {
                        keyword.Add(word, j);
                        compress += word;
                    }
                    word = "";
                    compress += ' ';
                }
                else
                    word += source[i];
            }
            return compress;
        }
    }
}
