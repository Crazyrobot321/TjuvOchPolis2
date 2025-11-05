using System.Timers;

namespace TjuvOchPolis
{
    class Thief : Person
    {
        public bool HasStolen { get; set; }
        public bool IsInPrison { get; set; } = false;
        public int NumberOfSecondsToSpendInPrison { get; set; }

        public Thief(int locationX, int locationY, int directionX, int directionY, List<String> properties, bool hasStolen, bool isinprison) : base(locationX, locationY, directionX, directionY, properties)
        {
            HasStolen = hasStolen;
            IsInPrison = isinprison;
        }

        public void StartPrisonTime()
        {
            // Skapar en timer som bara körs en gång efter specificerad tid
            var timer = new System.Timers.Timer(TimeSpan.FromSeconds(NumberOfSecondsToSpendInPrison));

            timer.Elapsed += (sender, e) => //sender = timer, e = information om när händelsen inträffade (när timern startade)
            {
                IsInPrison = false;
                Program.queue.Enqueue($"The prisoner has been released after {NumberOfSecondsToSpendInPrison} seconds");
                timer.Dispose(); // Rensar upp resurser
            };

            timer.Start();
        }

        public static void Steal(Thief thief, List<Person> people)
        {
            if (thief.IsInPrison) //Kollar om tjuven är fängslad och avslutar metoden direkt om det är sant
                return;

            List<Citizen> citizens = people.OfType<Citizen>().ToList(); //Skapar en lista med alla personer som är typen Citizen

            foreach (var citizen in citizens)
            {
                if (Person.InSameLocation(thief, citizen))
                {
                    if (citizen.Properties == null || citizen.Properties.Count == 0)
                    {
                        //Bryter metoden tidigt om medborgarens properties är 0 eller null
                        break;
                    }

                    int count = citizen.Properties.Count;
                    int rnd = Random.Shared.Next(0, count);
                    thief.Properties.Add(citizen.Properties[rnd]);
                    citizen.Properties.RemoveAt(rnd);
                    thief.HasStolen = true;
                    Program.queue.Enqueue("A thief has stolen something! ");
                    break; //Bryter koden så tjuven stjäl en sak
                }
            }
        }

    }
}
