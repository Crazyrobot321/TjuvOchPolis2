using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    public class Personer
    {
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }
        public List<String> Properties{ get; set; }
        
        public static int maxX = Program.width + 1;
        public static int maxY = Program.height;
        public static int minX = 2;
        public static int minY = 2;

        public static int prisonMaxX = 20;
        public static int prisonMaxY = 5;
        public static int prisonMinX = 2;
        public static int prisonMinY = 2;

        public Personer(int locationX, int locationY, int directionX, int directionY, List<String>properties)
        {
            LocationX = locationX;
            LocationY = locationY;
            DirectionX = directionX;
            DirectionY = directionY;
            Properties = new List<string>(properties); //Skapar en ny lista med samma innehåll så att varje person får sin egen kopia av listan (inte delar samma referens)
        }

        private static void MovePerson(Personer personer, char symbol, ConsoleColor color, bool debug, int randomChance)
        {
            int RndX = Random.Shared.Next(0, 100);
            int RndY = Random.Shared.Next(0, 100);

            //Om slumpmässiga talet är under 10 finns chans att byta direktion
            if (RndX <= randomChance)
            {
                personer.DirectionX = Random.Shared.Next(-1, 2);
            }
            if (RndY <= randomChance)
            {
                personer.DirectionY = Random.Shared.Next(-1, 2);
            }
            if(debug)
            {
                Console.Write("");
            }
            else
            {
                //Tar bort föregående symbol av respektive person
                Console.SetCursorPosition(personer.LocationX, personer.LocationY);
                Console.Write(" ");
            }
                

            personer.LocationX += personer.DirectionX;
            personer.LocationY += personer.DirectionY;


            //Om personens location når mer/mindre än minX & maxX återställs location till respektive fall
            if (personer.LocationX < minX)
                personer.LocationX = maxX;

            else if (personer.LocationX > maxX)
                personer.LocationX = minX;
            //Om personens location når mer/mindre än minY & maxY återställs location till respektive fall

            if (personer.LocationY < minY)
                personer.LocationY = maxY;

            else if (personer.LocationY > maxY)
                personer.LocationY = minY;

            Console.SetCursorPosition(personer.LocationX, personer.LocationY);
            if (debug)
            {
                Console.Write("");
            }
            else
            {
                Console.ForegroundColor = color;
                Console.Write(symbol);
            }
            

        }

        internal static void Move(List<Personer> personer, bool debug)
        {
            foreach (Personer p in personer)
            {
                switch (p)
                {
                    case Citizen medborgare:
                        MovePerson(medborgare, symbol: 'C', color: ConsoleColor.Green, debug: debug, randomChance: 10);
                        break;
                    case Thief tjuv:
                        if(tjuv.IsInPrison == true)
                        {
                            MoveInPrison(tjuv, debug: debug, randomChance: 10);
                        }
                        else
                        {
                            MovePerson(tjuv, symbol: 'T', color: ConsoleColor.Red, debug: debug, randomChance: 10);
                            // Call steal for the specific thief only
                            Thief.Steel(tjuv, personer);
                            
                        }
                        break;

                    case Police polis:
                        MovePerson(polis, symbol: 'P', color: ConsoleColor.Blue, debug: debug, randomChance: 10);
                        Police.Busted(personer);
                        break;
                    default:
                        break;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void MoveInPrison(Thief thief, bool debug, int randomChance)
        {
            int RndX = Random.Shared.Next(0, 100);
            int RndY = Random.Shared.Next(0, 100);

            //Om slumpmässiga talet är under 10 finns chans att byta direktion
            if (RndX <= randomChance)
            {
                thief.DirectionX = Random.Shared.Next(-1, 2);
            }
            if (RndY <= randomChance)
            {
                thief.DirectionY = Random.Shared.Next(-1, 2);
            }
            if (debug)
            {
                Console.Write("");
            }
            else
            {
                //Tar bort föregående symbol av respektive person
                Console.SetCursorPosition(thief.LocationX, thief.LocationY);
                Console.Write(" ");
            }


            thief.LocationX += thief.DirectionX;
            thief.LocationY += thief.DirectionY;


            //Om personens location når mer/mindre än minX & maxX återställs location till respektive fall
            if (thief.LocationX < prisonMinX)
                thief.LocationX = prisonMaxX;

            else if (thief.LocationX > prisonMaxX)
                thief.LocationX = prisonMinX;
            //Om personens location når mer/mindre än minY & maxY återställs location till respektive fall

            if (thief.LocationY < prisonMinY)
                thief.LocationY = prisonMaxY;

            else if (thief.LocationY >prisonMaxY)
                thief.LocationY = prisonMinY;

            Console.SetCursorPosition(thief.LocationX, thief.LocationY);
            if (debug)
            {
                Console.Write("");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("T");
            }
        }

       
    }

    class Citizen : Personer
    {
        public Citizen(int locationX, int locationY,int directionX,int directionY, List<String> properties) : base(locationX,locationY,directionX,directionY,properties)
        {
            //properties.AddRange("Keys", "Mobile", "Wallet", "Watch", "Jewlery");
        }
    }

    class Thief : Personer
    {
        public bool HasStolen { get; set; }
        public bool IsInPrison { get; set; } = false;
        public Thief(int locationX, int locationY, int directionX, int directionY, List<String> properties, bool hasStolen, bool isinprison) : base(locationX, locationY, directionX, directionY, properties)
        {
            HasStolen = hasStolen;
            IsInPrison = isinprison;
        }

        public static void Steel(Thief thief, List<Personer> personer)
        {
            if (thief.IsInPrison) //Kollar om tjuven är fängslad
                return;

            var medborgarna = personer.OfType<Citizen>(); //Filtrerar listan med typen Citizen

            foreach (var medborgare in medborgarna)
            {
                if (thief.LocationY == medborgare.LocationY && thief.LocationX == medborgare.LocationX)
                {
                    if (medborgare.Properties == null || medborgare.Properties.Count == 0)
                    {
                        //Om medborgarens properties är 0 eller null bryts loopen tidigt
                        break;
                    }

                    int count = medborgare.Properties.Count;
                    int rnd = Random.Shared.Next(0, count); // safe because count > 0
                    thief.Properties.Add(medborgare.Properties[rnd]);
                    medborgare.Properties.RemoveAt(rnd);
                    thief.HasStolen = true;
                    Program.queue.Enqueue("En tjuv har stulit något!");
                    // Stop after stealing a single item from the first matching citizen
                    break;
                }
            }
        }

    }

    class Police : Personer
    {
        public int CaughtThieves { get; set; }
        public Police(int locationX, int locationY, int directionX, int directionY, List<String> properties, int caughtThieves) : base(locationX, locationY, directionX, directionY, properties)
        {
            CaughtThieves = caughtThieves;
        }
        
        public static void Busted(List<Personer> personer)
        {
            var tjuvar = personer.OfType<Thief>();
            var poliser = personer.OfType<Police>();
            var medborgare = personer.OfType<Citizen>();

            foreach (var polis in poliser)
            {
                foreach (var tjuv in tjuvar)
                {
                    if (polis.LocationY == tjuv.LocationY && polis.LocationX == tjuv.LocationX && tjuv.HasStolen == true)
                    {
                        polis.Properties.AddRange(tjuv.Properties);
                        tjuv.Properties.Clear();
                        tjuv.IsInPrison = true;
                        Program.queue.Enqueue("Polisen haffar en skurk!");
                        Program.bustedthief.Add(tjuv);
                        
;                    }
                    else if(polis.LocationY == tjuv.LocationY && polis.LocationX == tjuv.LocationX && tjuv.HasStolen == false)
                    {
                        Program.queue.Enqueue("Polisen möter en tjuv!");
                    }
                }
                //personer.Remove(tjuv);
                foreach (var person in medborgare)
                {
                    if(polis.LocationY == person.LocationY &&  polis.LocationX == person.LocationX)
                    {
                        Program.queue.Enqueue("Polisen hälsar på en medborgare");
                    }
                }
            }
        }
    }
}
