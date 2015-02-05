using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Photoshop
{
    class Program
    {
        private static bool IsValid(int x, int y, int width, int height)
        {
            return x < width && y < height && x > 0 && y > 0;
        }
        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
        public static BMPReader binarize(BMPReader image, int threshold)
        {
            for (int i = 0; i < image.height; i++)
            {
                for (int j = 0; j < image.width; j++)
                {
                    Color color = image.get_pixel(j, i);
                    if ((color.R + color.G + color.B) / 3 < threshold)
                        image.set_pixel(j, i, Color.White);
                    else
                        image.set_pixel(j, i, Color.Black);

                }
            }
            return image;
        }
        public static BMPReader to_grey(BMPReader image)
        {
            for (int i = 0; i < image.height; i++)
            {
                for (int j = 0; j < image.width; j++)
                {
                    Color color = image.get_pixel(j, i);
                    int grey=(int)(color.R*.3f + color.G*.59 + color.B*.11);
                    image.set_pixel(j, i, Color.FromArgb(grey,grey,grey,grey));

                }
            }
            return image;
        }
        public static BMPReader convolution(BMPReader image, float[,] mat)
        {
            Color[,] image1 = image.Clone();
            for (int x = 0; x < image.width; x++)
            {
                for (int y = 0; y < image.height; y++)
                {
                    int matx = mat.GetLength(0) / 2;
                    float R = 0f, G = 0f, B = 0f;
                    for (int i = -matx; i < matx; i++)
                    {
                        int maty = mat.GetLength(1) / 2;
                        for (int j = -maty; j < maty; j++)
                        {
                            if (IsValid(x-i,y-j,image.width,image.height))
                            {
                                int a=matx+i, b=maty+j;
                                R += mat[a, b] * image1[x - i, y - j].R;
                                G += mat[a, b] * image1[x - i, y - j].G;
                                B += mat[a, b] * image1[x - i, y - j].B;
                            }
                        }
                    }

                    image.set_pixel(x,y,Color.FromArgb(image1[x, y].A, Clamp((int)R, 0, 255), Clamp((int)G, 0, 255), Clamp((int)B, 0, 255)));

                }
            }
            return image;
        }
        static void Main(string[] args)
        {
            BMPReader bmp1 = new BMPReader("24bit.bmp");
            BMPReader bmp = new BMPReader("lena.bmp");
            BMPReader bmp2 = new BMPReader("a.bmp");
            BMPReader bmp3 = new BMPReader("b.bmp");
            BMPReader bmp4 = new BMPReader("black.bmp");
            bmp2.display_header();/*
            for (int i = 0; i < bmp.width; i++)
            {
                for (int j = 0; j < bmp.height; j++)
                {
                    if (Math.Pow(i - 300, 2) + Math.Pow(j -300, 2) <= Math.Pow(200, 2) + 800 && Math.Pow(i - 300, 2) + Math.Pow(j - 300, 2) >= Math.Pow(200, 2) - 800)
                    {
                        bmp.set_pixel(i, j, Color.Magenta);
                    }
                }
            }*/
            binarize(bmp,150);
            to_grey(bmp1);
            to_grey(bmp2);
            /*float[,] mat = new float[3, 3] { { 0, 1, 0 },
                                             { 0, 0, 0 },
                                             { 0, 0, 0 } };*/
            /*float [,] mat = new float[3, 3] { { -1, -1, -1 },
                                    { -1, 4, -1 },
                                    { -1, -1, -1 } };*/
             
            /*float[,] mat = new float[3, 3] { { -2, -2, 0 },
                                             { -2, 7, 7 },
                                             { 0, 7, 4 } };*/
            /*float[,] mat = new float[5, 5] { { .1f, .1f, .1f , .1f, .1f},
                                             { .1f, .0f, .0f , .0f, .1f},
                                             { .1f, .0f, .3f , .0f, .1f},
                                             { .1f, .0f, .0f , .0f, .1f},
                                             { .1f, .1f, .1f , .1f, .1f}};*/
            float[,] mat = new float[5, 3]{{-4,-4,0},
                                         {0,6,6},
                                         {3,0,3},
                                         {3,-3,3},
                                         {-6,-6,0}};
            /*float[,] mat = new float[3, 3] { { .4f, .3f, .3f },
                                             { .3f, 0, .3f },
                                             { .3f, .3f, .4f } };*/
            /*float[,] mat = new float[3, 3] { { .6f, .2f, 0f },
                                             { .2f, 0, .2f },
                                             { 0f, .2f, .6f } };*/
            convolution(bmp1, mat);
            Stegano test = new Stegano(bmp2);
            test.stegano_hide("coucou comment vas tu ? pour ma part j'écris des message dans des images!!!");
            Console.WriteLine(test.stegano_find());
            /*test.stegano_encipher("c", "a");
            Console.WriteLine(test.stegano_decipher("a"));*/
            
            
            
            
            bmp.save("clone.bmp");
            bmp1.save("clone1.bmp");
            bmp2.save("clone2.bmp");
            bmp3.save("clone3.bmp");
            bmp4.save("clone4.bmp");
            Console.WriteLine((int)'Z');
            Console.ReadLine();
        }
    }
}
