using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tiny42sh
{
    public enum Keyword
    {
        ls,
        cd,
        cat,
        exit,
        clear,
        error
    }
    public static class Interpreter
    {
        static public string[][] parse_input(string input)
        {
            string[] temp = split(input, ';');
            string[][] instructions = new string[temp.Length][];
            for (int i = 0; i < instructions.Length; i++)
            {
                instructions[i] = split(temp[i], ' ');
            }
            return instructions;
        }
        static public Keyword is_keyword(string word)
        {
            switch (word)
            {
                case "ls":
                    return Keyword.ls;
                case "cd":
                    return Keyword.cd;
                case "cat":
                    return Keyword.cat;
                case "exit":
                    return Keyword.exit;
                case "clear":
                    return Keyword.clear;
                default:
                    return Keyword.error;
            }
        }
        private static string[] split(string input, char occurence)
        {
            if (input != null)
            {
                string[] temp = new string[100];
                int j = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (i + 1 >= input.Length && input[i] == occurence) { }
                    else if (input[i] == occurence)
                        j++;
                    else
                        temp[j] += input[i];
                }
                string[] result = new string[j + 1];
                for (int i = 0; i < j + 1; i++)
                {
                    //Console.WriteLine("rang: "+i+" valeur: "+temp[i]);
                    result[i] = temp[i];
                }
                return result;
            }
            return null;
            
        }
    }
}
