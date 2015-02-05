using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ex1
{
    class Program
    {
        static String read_file(string filename)
        {
            string str = "", line = "";
            if (File.Exists(filename))
            {
                // Instantiate a new StreamReader :
                StreamReader stream_reader = new StreamReader(filename);
                // Read the file via the StreamReader :

                while ((line = stream_reader.ReadLine()) != null)
                {
                    str += line + '\n';
                }
                // Never forget to close your StreamReader :
                stream_reader.Close();
            }
           
            return str;
        }
        static void write_to_file(string filename, string[] lines)
        {
            StreamWriter stream_writer = new StreamWriter(filename);
            for (int i = 0; i < lines.Length; i++)
            {
                stream_writer.WriteLine(lines[i]);
            }
            stream_writer.Close();
        }
        static void Main(string[] args)
        {
            string[] textes = new string[10];
            for (int i = 0; i < textes.Length; i++)
            {
                textes[i] = "acdc";
            }
            write_to_file("acdc.txt", textes);
            Console.WriteLine(read_file("acdc.txt"));
            Console.ReadLine();
        }
    }
}
