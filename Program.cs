using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace TjuvOchPolis
{
    internal class Program
    {
        //CITY
        public static int height = 25;
        public static int width = 100;
        public static bool hasRun = false;
        public static Queue queue = new Queue();
        static void Main(string[] args)
        {
            //Skapar personernas tillhörigheter
            List<String> properties = new List<String> {"Keys", "Mobile", "Wallet", "Watch", "Jewlery"};
            List<String> seizedGoods = new List<String>();
            List<String> StolenItems = new List<String>();
            List<Person> personer = new List<Person>();
            bool debug = false;

            //Skapar personer med slumpmässig placering inom spelplanen och slumpmässig riktning
            for(int i = 0; i < 20; i++)
            {
                int posX = Random.Shared.Next(3, width - 2);
                int posY = Random.Shared.Next(3, height - 2);
                int dirX = Random.Shared.Next(-1, 2);
                int dirY = Random.Shared.Next(-1, 2);
                personer.Add(new Citizen(posX, posY, dirX, dirY, properties, false));
            } //Medborgare
            for(int i = 0; i < 10; i++)
            {
                int posX = Random.Shared.Next(3, width - 2);
                int posY = Random.Shared.Next(3, height - 2);
                int dirX = Random.Shared.Next(-1, 2);
                int dirY = Random.Shared.Next(-1, 2);
                personer.Add(new Thief(posX, posY, dirX, dirY, StolenItems, false, false));
            } //Tjuvar
            for(int i = 0; i < 4; i++)
            {
                int posX = Random.Shared.Next(3, width - 2);
                int posY = Random.Shared.Next(3, height - 2);
                int dirX = Random.Shared.Next(-1, 2);
                int dirY = Random.Shared.Next(-1, 2);
                personer.Add(new Police(posX, posY, dirX, dirY, seizedGoods));
            } //Poliser
            Console.ReadLine();
            //Medans debug boolen är falsk körs programmet
            while (!debug)
            {
                Console.SetCursorPosition(0, 0);

                City.RenderGameBoard(hasRun, 100, 25);

                Prison.RenderPrison(hasRun, 20, 5);

                MovementHelper.MovePersons(personer, false);

                Status(personer);

                Console.SetCursorPosition(0, height + 9); //nedanför fängelset

                NewsFeed();

                hasRun = true;
                if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == 'd') //Kollar om d är tryckt utan att pausa loopen och sätter bool debug = true
                {
                    debug = true;
                    //Medans debug är true loopar programmet men skriver inte ut staden och skriver ut personernas information
                    while (debug)
                    {
                        Console.Clear();
                        Debugging.Debugs(personer);
                        MovementHelper.MovePersons(personer, true);
                        if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == 'd') //Kollar om d är tryckt utan att pausa och sätter bool debug till false
                        {
                            debug = false; //Fortsätter "main" loopen
                            hasRun = false;
                            Console.Clear();
                        }
                        Thread.Sleep(100);
                    }
                }
            Thread.Sleep(100);
            }

        }

        public static void Status(List<Person> personer)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(25, 27);
            for (int j = 0; j <( width -23); j++)
            {
                if (j == 5)
                {
                    Console.Write(" STATUS ");
                    j += " STATUS ".Length - 1;
                }
                else
                    Console.Write("=");
            }
            var Citizens = personer.OfType<Citizen>();
            var Thieves = personer.OfType<Thief>();
            var Coppers = personer.OfType<Police>();
            Console.SetCursorPosition(25, 28);
            Console.Write($"There are {Citizens.Count()} citizens     ");
            Console.SetCursorPosition(25,29);
            Console.Write($"There are {Citizens.Where(x => x.Robbed == true).Count()} robbed citizens");
            Console.SetCursorPosition(25,30);
            Console.Write($"There are {Thieves.Where(x => x.IsInPrison == false).Count()} thiefs in city   "); //Skriver ut de tjuvarna som inte är i fängelse
            Console.SetCursorPosition(25, 31);
            Console.Write($"There are {Thieves.Where(x => x.IsInPrison == true).Count()} thiefs in prison    "); //Skriver ut de tjuvarna som är i fängelse
            Console.SetCursorPosition(25, 32);
            Console.Write($"There are {Coppers.Count()} polices     ");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void NewsFeed()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int j = 0; j < width + 2; j++)
            {
                if (j == 5)
                {
                    Console.Write(" NEWSFEED ");
                    j += " NEWSFEED ".Length - 1;
                }
                else
                    Console.Write("=");
            }
            Console.WriteLine();
            if (queue.Count == 0)
            {
                Console.WriteLine("No news");
            }
            else
            {
                int index = 1;
                foreach (var item in queue.ToArray().Take(10)) //Gör om queue till en Array och plockar 10 object
                {
                    Console.WriteLine($"({index++}) - {item.ToString().PadRight(60)}"); //Gör om item till string och fyller 60 karaktärer åt höger
                }

                // Börja ta bort queue innehåll när det finns mer än 10 händelser
                while (queue.Count > 10)
                    queue.Dequeue();
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

    }

}