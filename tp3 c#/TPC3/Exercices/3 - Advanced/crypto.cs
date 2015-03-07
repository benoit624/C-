using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPC3.Exercices._3___Advanced
{
    class crypto_box
    {
        string ciphered;
        string key;
        int n;
        public crypto_box()
        {
            ciphered = "bw wvu urr'k demd hfz xf vfbl, ak oirlk iuon ksn mf blblj";
            key = "tryharder";
            n = ciphered.Length * key.Length % 42;
        }

        public string get_clear_text()
        {
            string r = pass2(ciphered.ToLower(), n);
            r = pass1(ciphered.ToLower(), key.ToLower(), 1);
            return r;
        }

        private string pass1(string s, string k, int m)
        {
            string r = String.Empty;
            int j = 0;
            for (int i = 0; i < s.Length; ++i)
                r += (char.IsLetter(s[i])) ? Convert.ToChar(((s[i] - 0x61) + (m * k[j++ % k.Length] - m * 0x61) + 0x1A) % 0x1A + 0x61) : s[i];
            return r;
        }

        private string pass2(string s, int n)
        {
            string r = String.Empty;
            for (int i = 0; i < s.Length; ++i)
                r += (char.IsLetter(s[i])) ? Convert.ToChar((s[i] + n - 0x61 + 0x1A) % 0x1A + 0x61) : s[i];
            return r;
        }
    }
}
