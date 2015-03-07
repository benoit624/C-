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
    class Exo
    {
        public string name{get;private set;}
        private string folder;
        public string stdout{get;private set;}
        public string stderr{get;private set;}
        public Exo(string folderName)
        {
            folder = folderName;
            name = Path.GetFileName(folder);
        }
        public bool execute()
        {
            if (File.Exists(folder+"\\exo.exe"))
            {
                
                ProcessStartInfo pStart = new ProcessStartInfo();
                pStart.FileName = folder +"\\exo.exe";
                //Enable the stdout getter of the process
                pStart.RedirectStandardOutput = true;
                //Enable the stderr getter of the process
                pStart.RedirectStandardError = true;
                pStart.UseShellExecute = false;
                try
                {
                    Process p = Process.Start(pStart);
                    //Get the process stdout stream
                    stdout = p.StandardOutput.ReadToEnd();
                    //Get the process stderr stream
                    stderr = p.StandardError.ReadToEnd();
                    Regex r = new Regex("\r");
                    stderr = r.Replace(stderr, "");
                    stdout = r.Replace(stdout, "");
                }
                catch (Exception)
                {
                    return false;
                }
                
                
                return true;
            }
            else
                return false;
        }
    }
}
