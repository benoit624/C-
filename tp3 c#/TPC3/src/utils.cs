using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPC3
{
    class utils
    {

        public static unsafe void fex2(int *nb)
        {
            *nb += 0xCAFE;
            *nb ^= *nb;
            *nb *= 0xDEAD;
        }

        public static unsafe void fex1(int *nb)
        {
            int tmp = *nb << 2;
            ++(*nb);
            tmp |= *nb;
        }

        public static unsafe void fex3(int *a, int *b)
        {
            *a ^= *b;
            *b ^= *a;
            *a ^= *b;
        }

        public static unsafe void fex4(int *a, int *b)
        {
            if (0 == *b % 2)
                *a <<= *b;
            else
                *a *= *b;
        }
    }
}
