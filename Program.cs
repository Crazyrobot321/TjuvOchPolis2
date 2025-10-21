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
                personer.Add(new Citizen(Random.Shared.Next(width), Random.Shared.Next(height), Random.Shared.Next(-1, 2), Random.Shared.Next(-1, 2), properties));
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

                Console.WriteLine();
            }
        }
    }
}