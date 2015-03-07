using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Moulinette
{
    class Correction
    {
        private string name;
        private string folder;
        private string stdout;
        private string stderr;
        public string getName()
        {
            return name;
        }
        public string getStdout()
        {
            return stdout;
        }
        public string getStderr()
        {
            return stderr;
        }
        public Correction(string folderName)
        {
            folder = folderName;
            name = Path.GetFileName(folder);
            init();
        }
        public bool init()
        {
            if (File.Exists(folder + "\\stdout.txt") && File.Exists(folder + "\\stderr.txt"))
            {
                stdout = read_file(folder + "\\stdout.txt");
                stderr = read_file(folder + "\\stderr.txt");
                return true;
            }
            else
                return false;
        }
        private String read_file(string filename)
        {
            string str = "", line = "";
                // Instantiate a new StreamReader :
                StreamReader stream_reader = new StreamReader(filename);
                // Read the file via the StreamReader :

                while ((line = stream_reader.ReadLine()) != null)
                {
                    str += line + '\n';
                }
                // Never forget to close your StreamReader :
                stream_reader.Close();
            return str;
        }
    }
}
