using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Photoshop
{
    public class BMPReader 
    {
        public int height { get ; private set; }
        public int width { get ; private set; }
        private int pixel_array_offset ;
        private short bits_per_pixel ;
        private byte [] header ;
        private Color [, ] pixels ;
        public BMPReader(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                    throw new Exception("file don't exist!");
                FileStream fs = new FileStream(filename, FileMode.Open);

                header = new byte[54];
                if (!is_bitmap(fs))
                    throw new Exception("format don't suport");
                read_header(fs);
                read_pixels(fs);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }  
        }

        private bool is_bitmap(FileStream fs)
        {
            fs.Read(header, 0, 2);
            return header[0] == 0x42 && header[1] == 0x4D; 
        }

        private void read_header(FileStream fs)
        {
            fs.Read(header, 2, 52); 
            pixel_array_offset = BitConverter.ToInt32(header, 0xA);
            width = BitConverter.ToInt32(header, 0x12);
            height = BitConverter.ToInt32(header, 0x16);
            bits_per_pixel = BitConverter.ToInt16(header, 0x1C);
        }

        public void display_header()
        {
            Console.WriteLine("Width : {0} pixels", width);
            Console.WriteLine("Height : {0} pixels", height);
            Console.WriteLine("Offset : {0} bytes", pixel_array_offset);
            Console.WriteLine("Bits per pixels : {0} bits", bits_per_pixel);
        }
        public Color get_pixel(int x, int y)
        {
            return pixels[x, y];
        }
        public void set_pixel(int x, int y, Color c)
        {
            pixels[x, y] = c;
        }
        private void read_pixels(FileStream fs)
        {
            int padding = (BitConverter.ToInt32(header, 0x2)-pixel_array_offset-height*3*width)/height;
            pixels = new Color[width,height];

            for (int j = height-1; j >= 0; j--)
            {
                for (int i = 0; i < width; i++)
                {
                    if (bits_per_pixel==24)
                        pixels[i, j] = Color.FromArgb(fs.ReadByte(), fs.ReadByte(), fs.ReadByte());
                    else
                    {
                        byte[] color = new byte[4];
                        fs.Read(color, 0, 4);
                        pixels[i, j] = Color.FromArgb(color[3], color[2], color[1], color[0]);
                        padding = 0;
                    }
                        
                    //Console.WriteLine(pixels[i, j]);
                }
                for (int i = 0; i < padding; i++)
                {
                    fs.ReadByte();
                }
                
                
            }
            fs.Close();
            
        }
        public void save(string filename)
        {
            int padding = 0;
            if (bits_per_pixel == 24)
                padding = (BitConverter.ToInt32(header, 0x2) - pixel_array_offset - height * 3 * width) / height;
            FileStream fs = new FileStream(filename, FileMode.Create);
            fs.Write(header, 0, 54);
            for (int i = height-1; i >=0; i--)
			{
			    for (int j = 0; j < width; j++)
			    {
                    if (bits_per_pixel == 24)
                    {
                        fs.WriteByte(pixels[j, i].R);
                        fs.WriteByte(pixels[j, i].G);
                        fs.WriteByte(pixels[j, i].B);
                    }
                    else
                    {
                        fs.WriteByte(pixels[j, i].B);
                        fs.WriteByte(pixels[j, i].G);
                        fs.WriteByte(pixels[j, i].R);
                        fs.WriteByte(pixels[j, i].A);
                    }
                    
			    }
                for (int j = 0; j < padding; j++)
                {
                    fs.WriteByte(0x00);
                }
			}
            fs.Close();
        }
        public Color[,] Clone()
        {
            Color[,] tab = new Color[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tab[i, j] = get_pixel(i, j);
                }
            }
            return tab;
        }
        public byte get_byte(int n)
        {
            n = n % (width * height * 3);
            int x = (n / 3) % width, y = height - 1 - ((n / 3) / width);
            Color c = pixels[x,y];
            byte b = 0x00;
            switch (n % 3)
            {
                case 0:
                    b = c.R;
                    break;
                case 1:
                    b = c.G;
                    break;
                case 2:
                    b = c.B;
                    break;
                default:
                    break;
            }
            return b;
        }
        public void set_byte(int n, byte b)
        {
            n = n % (width * height*3);
            int x = (n / 3) % width, y = height-1-((n / 3) / width);
            Color c = pixels[x, y];
            //Console.WriteLine(c);
            switch (n % 3)
            {
                case 0:
                    c=Color.FromArgb(c.A,b,c.G,c.B);
                    break;
                case 1:
                    c = Color.FromArgb(c.A,c.R, b, c.B);
                    break;
                case 2:
                    c = Color.FromArgb(c.A,c.R, c.G, b);
                    break;
                default:
                    break;
            }
            //Console.Write(c);
            pixels[x, y]=c;
        }
        public bool hide_bit(bool bit, int position)
        {
            byte b = get_byte(position);
            if (((b%2==0 && bit) ||(b%2==1 && !bit && b!=0xFF)))
            {
                b += 1;
            }
            else if (b % 2 == 1 && !bit && b == 0xFF)
            {
                return false;
            }
            set_byte(position, b);
            return true;
        }

            
    }
}
