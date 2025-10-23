using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    internal class Debugging
    {
        public static void Debugs(List<Personer> personer)
        {
            for (int i = 0; i < personer.Count; i++)
            {
                Personer p = personer[i];
                Console.WriteLine($"{p.GetType().Name} befinner sig vid ({p.LocationX}) ({p.LocationY}) och går mot riktningen ({p.DirectionX})({p.DirectionY}) och har {string.Join(", ", p.Properties)}");
            }
        }
    }
}
