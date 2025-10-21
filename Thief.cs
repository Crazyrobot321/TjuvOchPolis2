using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    internal class Thief : Person
    {
        public List<string> StoldItems { get; set; }
        public bool Arrested { get; set; }
        public Thief()
        {
            StoldItems = new List<string>();
        }

        public void Steel(Citizen citizen)
        {
            if(citizen.Belognings.Count > 0)
            {
                var item = citizen.Belognings[0];
                StoldItems.Add(item);

                citizen.Belognings.RemoveAt(0);
            }
        }
    }
}
