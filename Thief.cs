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

        public void ReleasetFromPrison()
        {
            IsInPrison = false;
        }

        public void StartPrisonTime()
        {
            var timer = new System.Timers.Timer(TimeSpan.FromSeconds(NumberOfSecondsToSpendInPrison));
            timer.Elapsed += (object obj, ElapsedEventArgs e) =>
            {
                IsInPrison = false;
                Program.queue.Enqueue($"Fången har frisläppts efter {NumberOfSecondsToSpendInPrison}");
                timer.Stop();
            };

            timer.Start();
        }

        public static void Steel(Thief thief, List<Person> personer)
        {
            if (thief.IsInPrison) //Kollar om tjuven är fängslad
                return;

            var medborgarna = personer.OfType<Citizen>(); //Filtrerar listan med typen Citizen

            foreach (var medborgare in medborgarna)
            {
                if (thief.LocationY == medborgare.LocationY && thief.LocationX == medborgare.LocationX)
                {
                    if (medborgare.Properties == null || medborgare.Properties.Count == 0)
                    {
                        //Om medborgarens properties är 0 eller null bryts loopen tidigt
                        break;
                    }

                    int count = medborgare.Properties.Count;
                    int rnd = Random.Shared.Next(0, count); // safe because count > 0
                    thief.Properties.Add(medborgare.Properties[rnd]);
                    medborgare.Properties.RemoveAt(rnd);
                    thief.HasStolen = true;
                    Program.queue.Enqueue("En tjuv har stulit något!");
                    // Stop after stealing a single item from the first matching citizen
                    break;
                }
            }
        }

    }
}
