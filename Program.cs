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
        public static List<Thief> bustedthief = new List<Thief>();
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
            while (debug == false)
            {
                City.RenderGameBoard(hasRan,100,25);
                Personer.Move(personer, false);
               
                Console.SetCursorPosition(0, height + 3);
                Prison.RenderPrison(hasRan, 20, 5);
                Console.SetCursorPosition(0, height + 10);
                Status(personer);
                Console.WriteLine();
                NewsFeed();
                Console.WriteLine();
                
                hasRan = true;
                if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == 'd') //Kollar om d är tryckt utan att pausa loopen och sätter bool debug = true
                {
                    debug = true;
                    //Medans debug är true loopar programmet men skriver inte ut staden och skriver ut personernas information
                    while (debug)
                    {
                        Console.Clear();
                        Debugging.Debugs(personer);
                        Personer.Move(personer, true);
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

        public static void Status(List<Personer> personer)
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
            var Citizens = personer.OfType<Citizen>();
            var Thieves = personer.OfType<Thief>();
            var Coppers = personer.OfType<Police>();
            Console.WriteLine($"\nDet finns {Citizens.Count<Citizen>()} medborgare");
            Console.WriteLine($"\nDet finns {Thieves.Count<Thief>()} av {Thieves.Count<Thief>()} tjuvar");
            Console.WriteLine($"\nDet finns {Coppers.Count<Police>()} poliser");
            for(int j = 0;j < width + 2;j++)
            {
                Console.Write("=");
            }
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
            if(queue.Count > 0 && queue.Count < 5)
            {
                foreach (var item in queue)
                {
                    Console.WriteLine(item.ToString().PadRight(40));
                }
            }
            else
            {
                Console.WriteLine("Inga nya händelser ");
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