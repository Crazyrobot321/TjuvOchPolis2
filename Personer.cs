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
        
        public static int maxX = Program.width - 2;
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

        public static void Move(List<Personer> personer, bool debug)
        {
            foreach (Personer p in personer)
            {
                switch (p)
                {
                    case Citizen medborgare:
                        int RndX = Random.Shared.Next(0, 100);
                        int RndY = Random.Shared.Next(0, 100);

                        if (RndX <= 10)
                        {
                            medborgare.DirectionX = Random.Shared.Next(-1, 2);
                            continue;
                        }
                        if (RndY <= 10)
                        {
                            medborgare.DirectionY = Random.Shared.Next(-1, 2);
                            continue;
                        }

                        medborgare.LocationX += medborgare.DirectionX;
                        medborgare.LocationY += medborgare.DirectionY;

                        if (medborgare.LocationX < minX)
                            medborgare.LocationX = maxX;

                        else if (medborgare.LocationX > maxX)
                            medborgare.LocationX = minX;

                        if (medborgare.LocationY < minY)
                            medborgare.LocationY = maxY;

                        else if (medborgare.LocationY > maxY)
                            medborgare.LocationY = minY;

                        Console.SetCursorPosition(medborgare.LocationX, medborgare.LocationY);
                        if (debug)
                        {
                            Console.Write("");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("C");
                        }

                        break;
                    case Thief tjuv:
                        int RndX2 = Random.Shared.Next(0, 100);
                        int RndY2 = Random.Shared.Next(0, 100);
                        if (RndX2 <= 5)
                        {
                            tjuv.DirectionX = Random.Shared.Next(-1, 2);
                        }
                        if (RndY2 <= 5)
                        {
                            tjuv.DirectionY = Random.Shared.Next(-1, 2);
                        }

                        tjuv.LocationX += tjuv.DirectionX;
                        tjuv.LocationY += tjuv.DirectionY;

                        if (tjuv.LocationX < minX)
                            tjuv.LocationX = maxX;

                        else if (tjuv.LocationX > maxX)
                            tjuv.LocationX = minX;

                        if (tjuv.LocationY < minY)
                            tjuv.LocationY = maxY;

                        else if (tjuv.LocationY > maxY)
                            tjuv.LocationY = minY;

                        Console.SetCursorPosition(tjuv.LocationX, tjuv.LocationY);
                        if (debug)
                        {
                            Console.Write("");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("T");
                        }

                        break;
                    case Police polis:
                        int RndX3 = Random.Shared.Next(0, 100);
                        int RndY3 = Random.Shared.Next(0, 100);
                        if (RndX3 <= 3)
                        {
                            polis.DirectionX = Random.Shared.Next(-1, 2);
                        }
                        if (RndY3 <= 3)
                        {
                            polis.DirectionY = Random.Shared.Next(-1, 2);
                        }

                        polis.LocationX += polis.DirectionX;
                        polis.LocationY += polis.DirectionY;

                        //Håller värderna inom giltliga gränser och gör en pac-man effekt
                        if (polis.LocationX < minX)
                            polis.LocationX = maxX;

                        else if (polis.LocationX > maxX)
                            polis.LocationX = minX;

                        if (polis.LocationY < minY)
                            polis.LocationY = maxY;

                        else if (polis.LocationY > maxY)
                            polis.LocationY = minY;

                        Console.SetCursorPosition(polis.LocationX, polis.LocationY);
                        if (debug)
                        {
                            Console.Write("");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("P");
                        }

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
            
        }
    }

    class Thief : Personer
    {
        public bool HasStolen { get; set; }
        public Thief(int locationX, int locationY, int directionX, int directionY, List<String> properties, bool hasStolen) : base(locationX, locationY, directionX, directionY, properties)
        {
            HasStolen = hasStolen;
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
