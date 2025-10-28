using System;
using System.Collections.Generic;
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

            //Tar bort föregående symbol av respektive person
            Console.SetCursorPosition(personer.LocationX, personer.LocationY);
            Console.WriteLine(" ");

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
                        MovePerson(tjuv, symbol: 'T', color: ConsoleColor.Red, debug: debug, randomChance: 10);
                        Thief.Steel(personer);
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
        public static void Steel(List<Personer> personer)
        {
            var tjuvar = personer.OfType<Thief>(); //Filtrerar listan och tar bara med objekt som är av typen Thief
            var medborgarna = personer.OfType<Citizen>();

            foreach (var tjuv in tjuvar)
            {
                foreach(var medborgare in medborgarna)
                {
                    if (tjuv.LocationY == medborgare.LocationY && tjuv.LocationX == medborgare.LocationX)
                    {
                        int test = medborgare.Properties.Count;
                        int rnd = Random.Shared.Next(test);
                        if(medborgare.Properties.Count == 0)
                        {
                            break;
                        }
                        tjuv.Properties.Add(medborgare.Properties[rnd]);
                        medborgare.Properties.RemoveAt(rnd);
                        //foreach (var item in tjuv.Properties)
                        //{
                        //    Console.SetCursorPosition(0, Program.height + 2);
                        //    Console.WriteLine($"\nEn tjuv stal {item}");
                        //    Thread.Sleep(100);
                        //    Console.Write(" ");
                        //}
                        tjuv.HasStolen = true;
                    }
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

        List<Personer> prisonPopulation = new List<Personer>();
        
        public static void Busted(List<Personer> personer)
        {
            var tjuvar = personer.OfType<Thief>();
            var poliser = personer.OfType<Police>();

            foreach (var polis in poliser)
            {
                foreach (var tjuv in tjuvar)
                {
                    if (polis.LocationY == tjuv.LocationY && polis.LocationX == tjuv.LocationX && tjuv.HasStolen == true)
                    {
                        polis.Properties.AddRange(tjuv.Properties);
                        tjuv.Properties.Clear();
                        tjuv.IsInPrison = true;
                    }
                }
            }
        }
    }
}
