using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

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
                personer.Add(new Thief(posX, posY, dirX, dirY, StolenItems, false, false));
            }
            for(int i = 0; i < 4; i++)
            {
                int posX = Random.Shared.Next(3, width - 2);
                int posY = Random.Shared.Next(3, height - 2);
                int dirX = Random.Shared.Next(-1, 2);
                int dirY = Random.Shared.Next(-1, 2);
                personer.Add(new Police(posX, posY, dirX, dirY, seizedGoods, 0));
            }
            Console.ReadLine();
            //Medans debug boolen är falsk loopar programmet
            bool prisonDebug = false;
            while (debug == false)
            {
                City.RenderGameBoard(hasRan,100,25);
                Personer.Move(personer, false);
                //Personer.CollisionCheck(personer, false);

                Console.SetCursorPosition(0, height + 2);
                Prison.RenderPrison(hasRan, 20, 5);
                Status();
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
                        Console.SetCursorPosition(0, height + 2);
                        if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == 'd') //Kollar om d är tryckt utan att pausa och sätter bool debug till false
                        {
                            debug = false; //Fortsätter "main" loopen
                        }
                       
                            Thread.Sleep(100);
                    }
                }
                
                   
                hasRan = true;
                Thread.Sleep(100);
            }

        }

        public static void Status()
        {
            for (int j = 0; j < width + 2; j++)
            {

                if (j == 5)

                {

                    Console.Write(" STATUS ");

                    j += " STATUS ".Length - 1;

                }

                else

                    Console.Write("=");

            }





        }
    }
}