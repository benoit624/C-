using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photoshop
{
    class Stegano
    {
        public BMPReader img { get; private set; }
        public Stegano(BMPReader img)
        {
            this.img = img;
        }
        private int bin(int n, int pos)
        {
            int lenght = 16;
            for (int i = pos; i < pos + lenght; i++)
            {
                Console.Write(n % 2 + " " + i + " ; ");
                if (n % 2 == 1)
                {
                    img.hide_bit(true, i);

                }
                else
                    if (!img.hide_bit(false, i))
                        img.set_byte(i, 0xFE);
                n /= 2;
            }
            Console.WriteLine();
            return lenght;
        }
        private int dec(int pos)
        {
            int lenght = 16;
            int n = 1, resutat = 0;

            for (int i = pos; i < pos + lenght; i++)
            {
                int h = img.get_byte(i) % 2;
                if (h == 1)
                    resutat += n;
                n *= 2;

            }
            return resutat;
        }
        public void stegano_hide(string message)
        {
            int length = message.Length;
            int pos = 0;
            pos = bin(length, pos);
            for (int j = 0; j < length; j++)
            {
                pos = bin(message[j], (j + 1) * pos*4);

            }

        }
        public string stegano_find()
        {
            int length = dec(0);
            int pos = 16;
            string str = "";
            for (int i = 0; i < length; i++)
            {
                str += (char)dec((i + 1) * pos*4);
            }
            return str;
        }
        /*public void stegano_encipher(string message, string key)
        {
            int pos = 0;
            pos = bin(message.Length, pos);
            for (int i = 0; i < message.Length; i++)
            {
                string test = Convert.ToString(message[i] ^ key[0], 2);
                for (int j = 0; j < 16; j++)
                {
                    pos++;
                    if (message.Length >= j)
                    {
                        if (test[j] == '0')
                            img.hide_bit(true, pos);
                        else
                            img.hide_bit(false, pos);
                    }
                    else
                    {
                        img.hide_bit(false, pos);
                    }
                }
            }
        }
        public string stegano_decipher(string key)
        {
            int length = dec(0);
            int pos = 16;
            string str = "";
            int a = 0;
            for (int i = 0; i < length; i++)
            {
                a++;
                str += Convert.ToString((char)dec(a * pos) ^ key[0], 10);

            }
            return str;
        }*/
    }
}
        // @return : if the bit was hidden or not
       
