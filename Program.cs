using System.Collections;
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
            while (debug == false)
            {
               Console.SetCursorPosition(0, 0);
                City.RenderGameBoard(hasRan,100,25);

                Prison.RenderPrison(hasRan, 20, 5);
               
                MovementHelper.MovePersons(personer, false);
                                       
                Status(personer);
               
                Console.SetCursorPosition(0, height + 9); //nedanför fängelset

                NewsFeed();

                hasRan = true;
                if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == 'd') //Kollar om d är tryckt utan att pausa loopen och sätter bool debug = true
                {
                    debug = true;
                    //Medans debug är true loopar programmet men skriver inte ut staden och skriver ut personernas information
                    while (debug)
                    {
                        Console.Clear();
                        Debugging.Debugs(personer);
                        Console.SetCursorPosition(0, height + 3);
                        if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == 'd') //Kollar om d är tryckt utan att pausa och sätter bool debug till false
                        {
                            debug = false; //Fortsätter "main" loopen
                            hasRan = false;
                            Console.Clear();
                        }
                        Thread.Sleep(100);
                    }
                }
            Thread.Sleep(100);
            }

        }
        //Kolla upp - Delete everything below cursor exemple
        public static void ClearArea(int top, int left, int height, int width)
        {
            ConsoleColor colorBefore = Console.BackgroundColor;
            try
            {
                Console.BackgroundColor = ConsoleColor.Black;
                string spaces = new string(' ', width);
                for (int i = 0; i < height; i++)
                {
                    try
                    {
                        Console.SetCursorPosition(left, top + i);
                        Console.Write(spaces);
                    }
                    catch (Exception)
                    {

                    
                    }
                   
                }
            }
            finally
            {
                Console.BackgroundColor = colorBefore;
            }
        }

        public static void Status(List<Person> personer)
        {
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
            Console.Write($"There is {Citizens.Count<Citizen>()} citizens     ");
            Console.SetCursorPosition(25,29);
            Console.Write($"There is {Thieves.Where(x => x.IsInPrison == false).Count()} thiefs in city   ");
            Console.SetCursorPosition(25, 30);
            Console.Write($"There is {Thieves.Where(x => x.IsInPrison == true).Count()} thiefs in prison    ");
            Console.SetCursorPosition(25, 31);
            Console.Write($"There is {Coppers.Count<Police>()} polices     ");
          
        }
        public static void NewsFeed()
        {
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
            if(queue.Count > 0)
            {
                foreach (var item in queue.ToArray().Take(5))
                {
                    Console.WriteLine(item.ToString().PadRight(40));
                }
            }
            else
            {
                Console.WriteLine("No news ");
            }
            if(queue.Count > 2)
            {
                while (queue.Count > 2)
                {
                    queue.Dequeue();
                } 
            }

        }
    }
}