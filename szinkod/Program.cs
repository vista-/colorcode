using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace szinkod
{
    class Pixel
    {
        public int R = 0;
        public int G = 0;
        public int B = 0;
        public Pixel(int Red, int Green, int Blue)
        {
            this.R = Red;
            this.G = Green;
            this.B = Blue;
        }
         public bool Equals(Pixel obj)
        {
             if(obj == null)
             {
                 return false;
             }
             else
             {
                 if (this.R == obj.R && this.G == obj.G && this.B == obj.B)
                 {
                     return true;
                 }
                 else
                 {
                     return false;
                 }
             }
             
        }
    }
    class Program
    {
        public static Pixel[,] picture = new Pixel[50,50];
        static void Main(string[] args)
        {
            readFile();
            //1
            Console.Write("Írjon be egy RGB színkódot! (formátum: Vörös Zöld Kék): ");
            string[] inputPixel = Console.ReadLine().Split(' ');
            Pixel pix = new Pixel(Convert.ToInt16(inputPixel[0]), Convert.ToInt16(inputPixel[1]), Convert.ToInt16(inputPixel[2]));
            bool found = false;
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    Pixel currPix = picture[i, j];
                    if(pix.Equals(picture[i,j]))
                    {
                        found = true;
                    }
                }
            }
            //kiírjuk
            Console.WriteLine("A beírt színkód " + (found ? "megtalálható" : "nem található meg") + " a képen.");
            //2
            writePic(picture);
            Pixel threefiveeight = picture[34, 7];
            int inRow = 0;
            int inColoumn = 0;
            for (int i = 0; i < 50; i++)
            {
                if(threefiveeight.Equals(picture[34,i]))
                {
                    inRow++;
                }
            }
            for (int i = 0; i < 50; i++)
            {
                if(threefiveeight.Equals(picture[i,7]))
                {
                    inColoumn++;
                }
            }
            Console.WriteLine("Sorban: " + Convert.ToString(inRow) + " Oszlopban: " + Convert.ToString(inColoumn));
            //3
            Pixel red = new Pixel(255, 0, 0);
            int redCount = 0;
            Pixel green = new Pixel(0, 255, 0);
            int greenCount = 0;
            Pixel blue = new Pixel(0, 0, 255);
            int blueCount = 0;
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if(red.Equals(picture[i,j]))
                    {
                        redCount++;
                    }
                    if(green.Equals(picture[i,j]))
                    {
                        greenCount++;
                    }
                    if(blue.Equals(picture[i,j]))
                    {
                        blueCount++;
                    }
                }
            }
            if(redCount >= greenCount)
            {
                if(redCount >= blueCount)
                {
                    Console.WriteLine("Vörös");
                }
                else
                {
                    Console.WriteLine("Kék");
                }
            }
            else
            {
                if (greenCount > blueCount)
                    Console.WriteLine("Zöld");
                else
                    Console.WriteLine("Kék");
            }
            //4
            Pixel[,] keretes = picture;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    keretes[i, j] = new Pixel(0, 0, 0);
                }
            }
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    keretes[i, j] = new Pixel(0, 0, 0);
                }
            }
            for (int i = 47; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    keretes[i, j] = new Pixel(0, 0, 0);
                }
            }
            for (int i = 0; i < 50; i++)
            {
                for (int j = 47; j < 50; j++)
                {
                    keretes[i, j] = new Pixel(0, 0, 0);
                }
            }
            //5
            StreamWriter writer = new StreamWriter("keretes.txt");
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    writer.WriteLine(Convert.ToString(keretes[i, j].R) + " " + Convert.ToString(keretes[i, j].G) + " " + Convert.ToString(keretes[i, j].B));
                }
            }
            writer.Close();
            writePic(keretes);
            //6
            Pixel yellow = new Pixel(255, 255, 0);
            int[] beginning = new int[2];
            int[] end = new int[2];
            int count = 0;
            Pixel prevPix = null;
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (count == 0)
                    {
                        if (yellow.Equals(keretes[i, j]))
                        {
                            beginning[0] = i;
                            beginning[1] = j;
                            count++;
                        }
                    }
                    else
                    {
                        if (!yellow.Equals(keretes[i, j]))
                        {
                            if (yellow.Equals(prevPix))
                            {
                                if (j != 0)
                                {
                                    end[0] = i;
                                    end[1] = j - 1;
                                }
                                else
                                {
                                    end[0] = i - 1;
                                    end[1] = j - 1;
                                }
                            }
                        }
                        else
                        {
                            count++;
                        }
                    }
                    prevPix = picture[i, j];
                }
            }
            Console.WriteLine("Kezd: {0}, {1}", Convert.ToString(beginning[0]), Convert.ToString(beginning[1]+1));
            Console.WriteLine("Vége: {0}, {1}", Convert.ToString(end[0]), Convert.ToString(end[1]+1));
            Console.WriteLine("Képpontok száma: {0}", Convert.ToString(count));
            Console.ReadLine();
        }

        private static void writePic(Pixel[,] pic)
        {
            StreamWriter writeout = new StreamWriter("writeout.txt");
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    writeout.Write(Convert.ToString(pic[i, j].R) + Convert.ToString(pic[i, j].G) + Convert.ToString(pic[i, j].B) + " ");
                }
                writeout.WriteLine();
            }
            writeout.Close();
        }

        private static void readFile()
        {
            StreamReader read = new StreamReader("kep.txt");
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    string[] currline = read.ReadLine().Split(' ');
                    picture[i, j] = new Pixel(Convert.ToInt16(currline[0]), Convert.ToInt16(currline[1]), Convert.ToInt16(currline[2])); //Takes 3 RGB vals
                }
            }
            read.Close();
        }
    }
}
