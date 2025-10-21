using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TjuvOchPolis
{
    internal class Program
    {
        public static int height = 25;
        public static int width = 100;
        public static bool hasRan = false;
        static void Main(string[] args)
        {
            List<String> properties = new List<String> { "Keys", "Mobile", "Wallet", "Watch", "Jewlery"};
            List<String> seizedGoods = new List<String>();
            List<String> StolenItems = new List<String>();
            List<Personer> personer = new List<Personer>();
            bool hasRan = false;
            bool debug = false;

            for(int i = 0; i < 20; i++)
            {
                personer.Add(new Citizen(Random.Shared.Next(3, width - 2), Random.Shared.Next(3, height - 2), Random.Shared.Next(-1, 2), Random.Shared.Next(-1, 2), properties));
            }
            for(int i = 0; i < 10; i++)
            {
                personer.Add(new Thief(Random.Shared.Next(3, width - 2), Random.Shared.Next(3, height - 2), Random.Shared.Next(-1, 2), Random.Shared.Next(-1, 2), StolenItems, false));
            }
            for(int i = 0; i < 4; i++)
            {
                personer.Add(new Police(Random.Shared.Next(3, width - 2), Random.Shared.Next(3, height - 2), Random.Shared.Next(-1, 2), Random.Shared.Next(-1, 2), seizedGoods, 0));
            }
            while (!debug)
            {
                Console.Clear();
                RenderGameBoard();
                Personer.Move(personer, false);
                //Personer.CollisionCheck(personer, false);
                hasRan = true;

                Console.SetCursorPosition(0, height);
                for (int row = 0; row < width + 2; row++)
                {
                    if (row == 5)
                    {
                        Console.Write(" STATUS ");
                        row += " STATUS ".Length - 1;
                    }
                    else
                        Console.Write("=");
                }
                Console.WriteLine();
                if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == 'd') //Kollar om d är tryckt utan att pausa loopen
                {
                    debug = true;
                    while (debug)
                    {
                        Console.Clear();
                        //Debug.Debugs(personer);
                        Personer.Move(personer, true);
                        Console.SetCursorPosition(0, height);
                        if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == 'd')
                        {
                            debug = false;
                        }
                        Thread.Sleep(1000);
                    }
                }
                Thread.Sleep(1000);
            }

        }

        private static void RenderGameBoard()
        {
            //spelytan för alla karaktärer ska vara 100x25, därav väggar runt staden
            var gameHeight = height + 2; //102
            var gameWidth = width + 2; // 27

            for (int line = 0; line < gameHeight; line++)
            {
                for (int row = 0; row < width+2; row++)
                {
                    var isFirstLine = line == 0;
                    var isLastLine = line == gameHeight - 1;
                    var isFirstRow = row == 0;
                    var isLastRow = row == gameWidth - 1;

                    if (isFirstLine|| isLastLine)
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
}