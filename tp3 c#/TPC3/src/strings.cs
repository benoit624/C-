using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TPC3
{
    static class strings
    {
        /// <summary>
        /// This function compute the length of a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns>int that represent the length of the string</returns>
        public static unsafe int mystrlen(char *str)
        {
            int *l = (int *)Marshal.AllocHGlobal(sizeof(int));
            utils.fex2(l);
            while (true)
            {
                ++str;
                utils.fex1(l);
                if (*str == '\0')
                    break;
            }
            return *l;
        }

        /// <summary>
        /// This function copy one string and return it.
        /// </summary>
        /// <param name="str">String to be copied</param>
        /// <returns>Copy of the original string</returns>
        public static unsafe sbyte *mystrcpy(char *str)
        {
            int len = 0;
            int i;
            // Jedi mode : You don't see the next five lines.
            while (*(str + len) != 0x0)
                ++len;
            sbyte *copied_string = (sbyte *)Marshal.AllocHGlobal(sizeof(sbyte) * len << 1);
            for (int j = 0; j < len << 1; ++j)
                copied_string[j] = 42;

            for (i = 0; i < len; ++i)
                copied_string[i] = (sbyte)str[i];
            return copied_string;
        }
    }
}
