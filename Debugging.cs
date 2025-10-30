using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    internal class Debugging
    {
        public static void Debugs(List<Person> people)
        {
            for (int i = 0; i < people.Count; i++)
            {
                Person p = people[i];
                Console.WriteLine($"{p.GetType().Name} is at ({p.LocationX}) ({p.LocationY}) and walkning towards direction ({p.DirectionX})({p.DirectionY}) and has {string.Join(", ", p.Properties)}");
            }
        }


    }
}
