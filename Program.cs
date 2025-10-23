using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TjuvOchPolis
{
    internal class Program
    {
        //CITY
        public static int height = 25;
        public static int width = 100;
        public static bool hasRan = false;
        static void Main(string[] args)
        {
            //Skapar personernas tillhörigheter
            List<String> properties = new List<String> {"Keys", "Mobile", "Wallet", "Watch", "Jewlery"};
            ;
            List<String> seizedGoods = new List<String>();
            List<String> StolenItems = new List<String>();
            List<Personer> personer = new List<Personer>();
            bool debug = false;
            //Skapar personer med slumpmässig placering inom spelplanen och slumpmässig riktning
            for(int i = 0; i < 20; i++)
            {
                int posX = Random.Shared.Next(3, width - 2);
                int posY = Random.Shared.Next(3, height - 2);
                int dirX = Random.Shared.Next(-1, 2);
                int dirY = Random.Shared.Next(-1, 2);
                personer.Add(new Citizen(posX, posY, dirX, dirY, properties));
            }
            for(int i = 0; i < 10; i++)
            {
                int posX = Random.Shared.Next(3, width - 2);
                int posY = Random.Shared.Next(3, height - 2);
                int dirX = Random.Shared.Next(-1, 2);
                int dirY = Random.Shared.Next(-1, 2);
                personer.Add(new Thief(posX, posY, dirX, dirY, StolenItems, false));
            }
            for(int i = 0; i < 4; i++)
            {
                int posX = Random.Shared.Next(3, width - 2);
                int posY = Random.Shared.Next(3, height - 2);
                int dirX = Random.Shared.Next(-1, 2);
                int dirY = Random.Shared.Next(-1, 2);
                personer.Add(new Police(posX, posY, dirX, dirY, seizedGoods, 0));
            }
            //Medans debug boolen är falsk loopar programmet
            while (debug == false)
            {
                Console.Clear();
                RenderGameBoard();
                Personer.Move(personer, false);
                //Personer.CollisionCheck(personer, false);

                Console.SetCursorPosition(0, height); //Nollar positionen av cursorn
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
                if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == 'd') //Kollar om d är tryckt utan att pausa loopen och sätter bool debug = true
                {
                    debug = true;
                    //Medans debug är true loopar programmet men skriver inte ut staden och skriver ut personernas information
                    while (debug)
                    {
                        Console.Clear();
                        Debugging.Debugs(personer);
                        Personer.Move(personer, true);
                        Console.SetCursorPosition(0, height);
                        if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == 'd') //Kollar om d är tryckt utan att pausa och sätter bool debug till false
                        {
                            debug = false; //Fortsätter "main" loopen
                        }
                        Thread.Sleep(1000);
                    }
                }
                Thread.Sleep(100);
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
}