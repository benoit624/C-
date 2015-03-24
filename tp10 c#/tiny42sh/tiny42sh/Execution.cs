using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tiny42sh
{
    public static class Execution
    {
        static private int execute_command(string[] cmd)
        {
            Keyword token;
            if (cmd!=null && cmd.Length > 0 && (token = Interpreter.is_keyword(cmd[0])) != Keyword.error)
            {
                switch (token)
                {
                    case Keyword.ls:
                        execute_ls(cmd);
                        break;
                    case Keyword.cd:
                        execute_cd(cmd);
                        break;
                    case Keyword.cat:
                        execute_cat(cmd);
                        break;
                    case Keyword.touch:
                        execute_touch(cmd);
                        break;
                    case Keyword.rm:
                        execute_rm(cmd);
                        break;
                    case Keyword.rmdir:
                        execute_rmdir(cmd);
                        break;
                    case Keyword.clear:
                        Console.Clear();
                        break;
                }
                return 1;
            }
            Console.WriteLine("invalid command");
            return 0;
        }
        static public int execute_input(string[][] input)
        {
            int test=0;
            for (int i = 0; i < input.Length; i++)
            {
                test = Execution.execute_command(input[i]);
            }
            return test;
        }
        static private void show_content( string entry) 
        {
            if (File.Exists(entry))
                Console.WriteLine(entry);
            else if (Directory.Exists(entry))
            {
                Console.WriteLine(entry);
                IEnumerable<string> folders = Directory.EnumerateDirectories(entry);
                print_enum(folders, entry);
                IEnumerable<string> files = Directory.EnumerateFiles(entry);
                print_enum(files, entry);
            }
            else
                Console.WriteLine("ls: "+entry + " => No such file or directory");
        }
        private static void print_enum(IEnumerable<string> print,string indent)
        {
            foreach (string file in print)
            {
                for (int i = 0; i < indent.Length; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(file.Substring(indent.Length));
            }
        }
        static private void execute_ls(string[] cmd)
        {
            if (cmd.Length < 2)
                show_content("./");
            for (int i = 1; i < cmd.Length; i++)
            {
                show_content(cmd[i]);
                Console.WriteLine();
            }
        }
        static private void execute_cat(string[] cmd)
        {
            if (cmd.Length < 2)
                Console.WriteLine("cat: Invalid number of arguments");
            for (int i = 1; i < cmd.Length; i++)
            {
                if (File.Exists(cmd[i]))
                {
                    Console.WriteLine("cat: {0}: ",cmd[i]);
                    string [] text = File.ReadAllLines(cmd[i]);
                    for (int j = 0; j < text.Length; j++)
                    {
                        Console.WriteLine("{0} : {1}", j, text[j]);
                    }
                }
                else
                {
                    Console.WriteLine("cat: {0}: Is not a file", cmd[i]);
                }
            }
        }
        static private void execute_cd(string[] cmd)
        {
            if (cmd.Length > 1 && Directory.Exists(cmd[1]))
                Directory.SetCurrentDirectory(cmd[1]);
            else
            {
                Console.WriteLine("cd: Invalid number of arguments");
            }
        }
        static private void execute_touch(string[] cmd)
        {
            if (cmd.Length < 2)
                Console.WriteLine("cd: Invalid number of arguments");
            else
            {
                for (int i = 1; i < cmd.Length; i++)
                {
                    try
                    {
                        if (File.Exists(cmd[i]))
                        {
                            File.SetLastAccessTime(cmd[i], DateTime.Now);
                            Console.WriteLine(cmd[i] + ": " + File.GetLastAccessTime(cmd[i]));
                        }
                        if (Directory.Exists(cmd[i]))
                        {
                            Directory.SetLastAccessTime(cmd[i], DateTime.Now);
                            Console.WriteLine(cmd[i] + ": " + File.GetLastAccessTime(cmd[i]));
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("cd: {0} Programme en cour d'execution", cmd[i]);
                    }     
                }
            }
        }
        static private void execute_rm(string[] cmd)
        {
            if (cmd.Length < 2)
                Console.WriteLine("rm: Invalid number of arguments");
            else
            {
                for (int i = 1; i < cmd.Length; i++)
                {                
                    if (File.Exists(cmd[i]))
                    {
                        File.Delete(cmd[i]);
                        Console.WriteLine("rm: " + cmd[i] + ": remove");
                    }                   
                }
            }
        }
        static private void execute_rmdir(string[] cmd)
        {
            if (cmd.Length < 2)
                Console.WriteLine("rm: Invalid number of arguments");
            else
            {
                for (int i = 1; i < cmd.Length; i++)
                {
                    if (Directory.Exists(cmd[i]))
                    {
                        Directory.Delete(cmd[i]);
                        Console.WriteLine("rm: " + cmd[i] + ": remove");
                    }
                }
            }
        }
    }
}
