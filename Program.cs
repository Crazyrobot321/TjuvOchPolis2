using System.Runtime.CompilerServices;

namespace TjuvOchPolis
{
    internal class Program
    {
        public static int height = 25;
        public static int width = 100;
        static void Main(string[] args)
        {
            List<String> properties = new List<String> { "Keys", "Mobile", "Wallet", "Watch", "Jewlery"};
            List<String> seizedGoods = new List<String>();
            List<String> StolenItems = new List<String>();
            List<Personer> personer = new List<Personer>();

            for(int i = 0; i < 20; i++)
            {
                personer.Add(new Citizen(Random.Shared.Next(1, width - 2), Random.Shared.Next(1, height - 2), Random.Shared.Next(-1, 2), Random.Shared.Next(-1, 2), properties));
            }
            for(int i = 0; i < 10; i++)
            {
                personer.Add(new Thief(Random.Shared.Next(1, width - 2), Random.Shared.Next(1, height - 2), Random.Shared.Next(-1, 2), Random.Shared.Next(-1, 2), StolenItems, false));
            }
            for(int i = 0; i < 4; i++)
            {
                personer.Add(new Police(Random.Shared.Next(1, width - 2), Random.Shared.Next(1, height - 2), Random.Shared.Next(-1, 2), Random.Shared.Next(-1, 2), seizedGoods, 0));
            }
            while (true)
            {
                RenderGameBoard();
                Thread.Sleep(500);
                Console.ReadKey();
            }

        }

        private static void RenderGameBoard()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y == 0 || y == height - 1)
                    {
                        if (y == 0 && x == 5)
                        {
                            Console.Write(" CITY ");
                            x += " CITY ".Length - 1;
                        }
                        else
                        {
                            Console.Write("=");
                        }
                    }

                    else if (x == 0 || x == width - 1)
                    {
                        Console.Write("||");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                //int foundOneCharacter = 0;
                //bool foundOnePerson = false;
                //bool foundOneThief = false;
                //bool foundOneCop = false;

                //if (foundOneCharacter == 0)
                //{
                //    Console.Write(" ");
                //}

                //else
                //{
                //    if (foundOneCharacter >1)
                //    {
                //        Console.Write("¤");
                //    }
                //    else
                //    {
                //        if (foundOnePerson)
                //        {
                //            Console.Write("C");
                //        }
                //        else if (foundOneThief)
                //        {
                //            Console.Write("T");
                //        }
                //        else if (foundOneCop)
                //        {
                //            Console.Write("P");
                //        }

                //    }
                //}

                    Console.WriteLine();
            }
        }
    }
}