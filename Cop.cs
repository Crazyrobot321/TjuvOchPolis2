using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    internal class Cop : Person
    {
        public List<string> LostAndFound { get; set; } 

        public Cop()
        {
            LostAndFound = new List<string>();
        }

        public void Arrest(Thief thief)
        {
            LostAndFound.AddRange(thief.StoldItems);
            thief.StoldItems.Clear();
            thief.Arrested = true;
        }
    }
}
