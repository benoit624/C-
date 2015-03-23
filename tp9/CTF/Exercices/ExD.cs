using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTF
{
    class ExD : ExX
    {
        public ExD()
        {
            name = "ExD";
        }
        private string rpn(string input)
        {
            int output = 0;
            char op_init = '+';
            Queue op = new Queue(4);
            Queueint nb = new Queueint(4);
            for (int i = 3; i < input.Length; i++)
            {
                if (input[i] >= 48 && input[i] < 58)
                {
                    string test = "";
                    while (i < input.Length && input[i] != ' ')
                    {
                        test += input[i];
                        i++;
                    }
                    nb.push(string_to_int(test));
                    if (!op.Empty())
                    {
                        int a = nb.top();
                        int b = nb.top();
                        output = calcul(output, calcul(a, b, op.top()), op_init);
                        op_init = ' ';
                    }
                }
                else
                {
                    if (op_init == ' ')
                        op_init = input[i];
                    else
                    {
                        op.push(input[i]);
                    }
                }
            }
            return int_to_string(output);
        }
        private int calcul(int a, int b, char op)
        {
            int result = 0;
            switch (op)
            {
                case '+':
                    result = a + b;
                    break;
                case '-':
                    result = a - b;
                    break;
                case '*':
                    result = a * b;
                    break;
                case '/':
                    if (b != 0)
                        result = a / b;
                    break;
                case '%':
                    result = a % b;
                    break;
                default:
                    break;
            }
            return result;
        }
        public override string solve(string question)
        {
            //int[] memory = new int[4];
            //bool add = false;
            //int result = 0;
            //List<int> nb = new List<int>();
            //List<char> op = new List<char>();
            //for (int i = 3; i < question.Length; i++)
            //{
            //    if (question[i] >= 48 && question[i] < 58)
            //    {
            //        add = false;
            //        string test = "";
            //        while (question[i] != ' ')
            //        {
            //            test += question[i];
            //            i++;
            //        }
            //        nb.Add(string_to_int(test));
            //    }
            //    else
            //    {
            //        add = true;
            //        op.Add(question[i]);
            //    }
            //}
            //for (int i = 0; i < nb.Count; i++)
            //{
            //    switch (op[i])
            //    {
            //        case '+':
            //            result += nb[i];
            //            break;
            //        case '-':
            //            result -= nb[i];
            //            break;
            //        case '*':
            //            result *= nb[i];
            //            break;
            //        case '/':
            //            result /= nb[i];
            //            break;
            //        case '%':
            //            result %= nb[i];
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //return int_to_string(result);

            return rpn(question);
        }
        private int string_to_int(string str)
        {
            int result = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                result += (int)(str[i] - 48) * (int)Math.Pow(10, str.Length - 1 - i);
            }
            return result;
        }
        private string int_to_string(int a)
        {
            string str = "";
            while (a != 0)
            {
                str = a % 10 + str;
                a /= 10;
            }
            return str;
        }
    }
    class Queue
    {
        private char[] memory;
        private int pointer;
        public Queue(int height)
        {
            pointer = 0;
            memory = new char[height];
        }
        public bool Empty()
        {
            return pointer == 0;
        }
        public char top()
        {
            if (pointer > 0)
            {
                char top = memory[0];
                for (int i = 1; i < pointer; i++)
                {
                    memory[i - 1] = memory[i];
                }
                pointer--;
                return top;
            }
            return '.';
        }
        public bool push(char a)
        {
            if (pointer + 1 < memory.Length)
            {
                pointer++;
                memory[pointer] = a;
                return true;
            }
            return false;
        }
    }
    class Queueint
    {
        private int[] memory;
        private int pointer;
        public Queueint(int height)
        {
            pointer = 0;
            memory = new int[height];
        }
        public int height()
        {
            return pointer;
        }
        public int top()
        {
            if (pointer > 0)
            {
                int top = memory[0];
                for (int i = 1; i < pointer; i++)
                {
                    memory[i - 1] = memory[i];
                }
                pointer--;
                return top;
            }
            return -1;
        }
        public bool push(int a)
        {
            if (pointer + 1 < memory.Length)
            {
                pointer++;
                memory[pointer] = a;
                return true;
            }
            return false;
        }
    }
}
