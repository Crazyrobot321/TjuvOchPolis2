using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    internal class MovementHelper
    {   //City
        public static int maxX = Program.width + 1;
        public static int maxY = Program.height;
        public static int minX = 2;
        public static int minY = 2;
        //Prison
        public static int prisonMaxX = 20;
        public static int prisonMaxY = 32;
        public static int prisonMinX = 2;
        public static int prisonMinY = 28;

        //Personernas Riktning och rörelse
        private static void MovePerson(Person person, char symbol, ConsoleColor color, bool debug, int randomChance)
        {
            int RndX = Random.Shared.Next(0, 100);
            int RndY = Random.Shared.Next(0, 100);

            //Om slumpmässiga talet är under 10 finns chans att byta direktion
            if (RndX <= randomChance)
            {
                person.DirectionX = Random.Shared.Next(-1, 2);
            }
            if (RndY <= randomChance)
            {
                person.DirectionY = Random.Shared.Next(-1, 2);
            }
            if (debug)
            {
                Console.Write("");
            }
            else
            {
                //Tar bort föregående symbol av respektive person
                Console.SetCursorPosition(person.LocationX, person.LocationY);
                Console.Write(" ");
            }


            person.LocationX += person.DirectionX;
            person.LocationY += person.DirectionY;


            //Om personens location når mer/mindre än minX & maxX återställs location till respektive fall
            if (person.LocationX < minX)
                person.LocationX = maxX;

            else if (person.LocationX > maxX)
                person.LocationX = minX;
            //Om personens location når mer/mindre än minY & maxY återställs location till respektive fall

            if (person.LocationY < minY)
                person.LocationY = maxY;

            else if (person.LocationY > maxY)
                person.LocationY = minY;

            if (debug)
            {
                Console.Write("");
            }
            else
            {

                Console.SetCursorPosition(person.LocationX, person.LocationY);
                Console.ForegroundColor = color;
                Console.Write(symbol);
            }


        }

        public static void MovePersons(List<Person> people, bool debug)
        {
            foreach (Person p in people)
            {
                if(p is Citizen citizen)
                {
                    MovePerson(citizen, 'C', ConsoleColor.Green, debug, 10);
                }
                if(p is Thief thief)
                {
                    if (thief.IsInPrison == true)
                    {
                        MoveInPrison(thief, debug);
                    }
                    else
                    {
                        MovePerson(thief, 'T',ConsoleColor.Red, debug, 10);
                        Thief.Steal(thief, people);

                    }
                }
                if(p is Police police)
                {
                    MovePerson(police, 'P', ConsoleColor.Blue, debug, 10);
                    Police.PoliceMeetPersonCheck(people);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void MoveInPrison(Thief thief, bool debug)
        {
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
            {
                thief.LocationX = prisonMaxX;
            }

            else if (thief.LocationX > prisonMaxX)
            {
                thief.LocationX = prisonMinX;
            }
            //Om personens location når mer/mindre än minY & maxY återställs location till respektive fall

            if (thief.LocationY < prisonMinY)
            { 
                thief.LocationY = prisonMaxY; 
            }

            else if (thief.LocationY > prisonMaxY)
            {
                thief.LocationY = prisonMinY;
            }

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
}
