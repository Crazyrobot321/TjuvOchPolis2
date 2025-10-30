using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    internal class Places
    {
        public bool HasRan { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Places(bool hasRan, int width, int height) 
        {
            HasRan = hasRan;
            Width = width;
            Height = height;
        }

        internal static void RenderGameBoard(bool hasRan, int width, int height)
        {
            //spelytan för alla karaktärer ska vara 100x25, därav väggar runt staden
            var gameHeight = height + 2; //102
            var gameWidth = width + 2; // 27

            if (!hasRan)
            {
                for (int line = 0; line < gameHeight; line++)
                {
                    for (int row = 0; row < width + 2; row++)
                    {
                        var isFirstLine = line == 0;
                        var isLastLine = line == gameHeight - 1;
                        var isFirstRow = row == 0;
                        var isLastRow = row == gameWidth - 1;

                        if (isFirstLine || isLastLine)
                        {
                            if (isFirstLine && row == 5)
                            {
                                var headline = " CITY ";
                                Console.Write(headline);
                                row += headline.Length - 1;
                            }
                            else
                            {
                                Console.Write("=");
                            }
                        }


                        else if (isFirstRow || isLastRow)
                        {
                            Console.Write("||");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
            }
           
        }
        internal static void RenderPrison(bool hasRan, int width, int height)
        {
            //spelytan för alla karaktärer ska vara 20*5, därav väggar runt fängelset
            var gameHeight = height + 2; //22
            var gameWidth = width + 2; // 7
            if (!hasRan)
            {
                for (int line = 0; line < gameHeight; line++)
                {
                    for (int row = 0; row < width + 2; row++)
                    {
                        var isFirstLine = line == 0;
                        var isLastLine = line == gameHeight - 1;
                        var isFirstRow = row == 0;
                        var isLastRow = row == gameWidth - 1;

                        if (isFirstLine || isLastLine)
                        {
                            if (isFirstLine && row == 2)
                            {
                                var headline = " PRISON ";
                                Console.Write(headline);
                                row += headline.Length - 1;
                            }
                            else
                            {
                                Console.Write("=");
                            }
                        }


                        else if (isFirstRow || isLastRow)
                        {
                            Console.Write("||");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
            }
           
        }
    }

    internal class City : Places
    {
        public City(bool hasRan, int width, int height): base(hasRan, width, height)
        {
            
        }
    }

    internal class Prison : Places
    {
        public Prison(bool hasRan, int width, int height): base(hasRan, width, height)
        {
            
        }
    }
}
