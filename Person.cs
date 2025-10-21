using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    internal class Person
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        private Random random = new Random();
        public Person()
        {         
            PositionX = random.Next(1, 101);
            PositionY = random.Next(1, 26);
        }

        public void SetNewLocation()
        {
            int RandomDirection = random.Next(1, 9);
            //Här gör vi om heltalet till enum av typen direction
            Direction direction = (Direction)RandomDirection;

            switch (direction)
            {
                case Direction.Up:
                    PositionY = -1;
                    break;
                case Direction.Right:
                    PositionX += 1;
                    break;
                case Direction.Down:
                    PositionY += 1;
                    break;
                case Direction.Left:
                    PositionX -= 1;
                    break;
                case Direction.UpAndRight:
                    PositionY -= 1;
                    PositionX += 1;
                    break;
                case Direction.DownAndRight:
                    PositionY += 1;
                    PositionX += 1;
                    break;
                case Direction.DownAndLeft:
                    PositionY += 1;
                    PositionX -= 1;
                    break;
                case Direction.UpAndLeft:
                    PositionY -= 1;
                    PositionX -= 1;
                    break;
            }


            //Har karaktären kommit för högt upp?
            if (PositionY < 1)
            {
                PositionY = 26;
            }


            //Har karaktären kommit för långt ner?
            if (PositionY > 26)
            {
                PositionY = 1;
            }

            //Har karaktären kommit för långt till höger?
            if (PositionX > 101)
            {
                PositionX = 1;
            }

            //Har karaktären kommit för långt till vänster?
            if (PositionX < 1)
            {
                PositionX = 101;
            }
        }
    }
   
    
}
