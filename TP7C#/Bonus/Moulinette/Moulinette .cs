using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Moulinette
{
    class Moulinette
    {
        private List<Correction> listCorrection;
        private List<Rendu> listRendu;
        public Moulinette()
        {
            listCorrection = new List<Correction>();
            listRendu = new List<Rendu>();
            StreamWriter stream_writer = new StreamWriter("resultat");
            stream_writer.WriteLine("");
            stream_writer.Close();
            write("Moulinette v0.0</br>");
        }
        public bool init()
        {
            if (Directory.Exists("correction\\") && Directory.Exists("rendu\\"))
            {
                
                List<string> dirs = new List<string>(Directory.EnumerateDirectories("correction\\"));
                if (dirs.Count() == 0)
                    return false;
                else
                {
                    Console.WriteLine(dirs.Count+" corrections trouvés");
                    foreach (string exo in dirs)
                    {
                        listCorrection.Add(new Correction(exo));
                    }
                    dirs = new List<string>(Directory.EnumerateDirectories("rendu\\"));
                    write(dirs.Count + " rendus trouvés ");
                    int i = dirs.Count;
                    foreach (string exo in dirs)
                    {
                        Regex r = new Regex("(rendu-tp-[a-z]{2,6}_[a-z])");
                        if (r.IsMatch(exo))
                        {
                            Rendu test = new Rendu(exo);
                            test.init();
                            listRendu.Add(test);
                            i--;
                        }                       
                    }
                    write("dont " + i + " incorrects</br>");
                    return true;
                }
            }
            else
                return false;
        }
        public void execute()
        {           
            foreach (Rendu rendu in listRendu)
            {
                write("rendu de " + (Path.GetFileName(rendu.folder).Remove(0, 9)) + "</br><ul>");
                write("grade: " + (int)rendu.runCorrection(listCorrection) * 100 / listCorrection.Count + "%</ul> </br>");
                //Console.WriteLine("\nrendu de " + (Path.GetFileName(rendu.folder).Remove(0,9)));
                //Console.WriteLine("grade: " + (int)rendu.runCorrection(listCorrection) * 100 / listCorrection.Count + "%");
                
                Console.ReadLine();
            }
        }
        public void write(string str)
        {
            Console.WriteLine(str);
            File.AppendAllText("resultat", str);
        }
    }
}
