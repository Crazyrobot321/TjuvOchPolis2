using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public static int maxX = Program.width - 1;
        public static int maxY = Program.height - 2;
        public static int minX = 2;
        public static int minY = 1;

        public Personer(int locationX, int locationY, int directionX, int directionY, List<String>properties)
        {
            LocationX = locationX;
            LocationY = locationY;
            DirectionX = directionX;
            DirectionY = directionY;
            Properties = properties;
        }

        private static void MovePerson(Personer personer, char symbol, ConsoleColor color, bool debug)
        {
            //int RndX = Random.Shared.Next(0, 100);
            //int RndY = Random.Shared.Next(0, 100);

            ////Om slumpmässiga talet är under 10 finns chans att byta direktion
            //if (RndX <= 10)
            //{
            //    personer.DirectionX = Random.Shared.Next(-1, 2);
            //}
            //if (RndY <= 10)
            //{
            //    personer.DirectionY = Random.Shared.Next(-1, 2);
            //}

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

                        MovePerson(medborgare, symbol:'C', color:ConsoleColor.Green, debug: debug);
                        break;
                    case Thief tjuv:
                        MovePerson(tjuv, symbol: 'T', color: ConsoleColor.Red, debug: debug);

                        break;
                    case Police polis:
                        MovePerson(polis, symbol: 'P', color: ConsoleColor.Blue, debug: debug);
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
            properties.AddRange("Keys", "Mobile", "Wallet", "Watch", "Jewlery");

        }
    }

    class Thief : Personer
    {
        public bool HasStolen { get; set; }
        public Thief(int locationX, int locationY, int directionX, int directionY, List<String> properties, bool hasStolen) : base(locationX, locationY, directionX, directionY, properties)
        {
            HasStolen = hasStolen;

        }
        public void Steel(Citizen citizen)
        {
            
        }
    }

    class Police : Personer
    {
        public int CaughtThieves { get; set; }
        public Police(int locationX, int locationY, int directionX, int directionY, List<String> properties, int caughtThieves) : base(locationX, locationY, directionX, directionY, properties)
        {
            CaughtThieves = caughtThieves;
        }
    }
}
