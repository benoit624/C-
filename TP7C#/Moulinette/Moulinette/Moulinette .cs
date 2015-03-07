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
            Console.WriteLine("Moulnette v0.0");
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
                    Console.Write(dirs.Count + " rendus trouvés ");
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
                    Console.WriteLine("dont " + i + " incorrects");
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
                Console.WriteLine("\nrendu de " + (Path.GetFileName(rendu.folder).Remove(0,9)));
                Console.WriteLine("grade: "+(int)rendu.runCorrection(listCorrection)*100/listCorrection.Count+"%");
                Console.ReadLine();
            }
        }
    }
}
