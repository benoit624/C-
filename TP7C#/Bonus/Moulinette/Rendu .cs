using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Moulinette
{
    class Rendu
    {
        public string folder { get; private set; }
        private List<Exo> listExo;
        public Rendu(string folderName)
        {
            folder = folderName;
            listExo = new List<Exo>();
        }
        public bool init()
        {
            if (Directory.Exists(folder))
            {
                List<string> dirs = new List<string>(Directory.EnumerateDirectories(folder));
                foreach (string exo in dirs)
                {
                    listExo.Add(new Exo(exo));
                }

                return true;
            }
            else
                return false;
        }
        public int runCorrection(List<Correction> listCorrection)
        {
            int solves = 0;
            for (int i = 0; i < listCorrection.Count; i++)
            {
                bool test=false;
                for (int j = 0; j < listExo.Count; j++)
                {
                    if (listCorrection[i].getName() == listExo[j].name)
                    {
                        test = true;
                        if (listExo[j].execute())
                        {
                            if (listCorrection[i].getStderr() == listExo[j].stderr && listExo[j].stdout == listCorrection[i].getStdout())
                            {
                                write("<li>" + listExo[j].name + ": OK</li>");
                                solves++;
                            }
                            else
                                write("<li>" + listExo[j].name + ": FAIL</li>");
                        }
                        else
                            write("<li>" + listExo[j].name + ": error execute()!</li>");
                        break;
                    }
                }
                if(!test)
                    write("<li>" + listCorrection[i].getName() + ": not found!</li>");
            }
            return solves;
        }
        public void write(string str)
        {
            Console.WriteLine(str);
            File.AppendAllText("resultat", str);
        }
    }
}
