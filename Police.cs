namespace TjuvOchPolis
{
    class Police : Person
    {
        public int CaughtThieves { get; set; }
        public Police(int locationX, int locationY, int directionX, int directionY, List<String> properties, int caughtThieves) : base(locationX, locationY, directionX, directionY, properties)
        {
            CaughtThieves = caughtThieves;
        }

        public void Arrest(Thief thief)
        {
            var numberOfStolenProperties = thief.Properties.Count;
            var numberOfSecondsInPrison = numberOfStolenProperties * 10;
            Properties.AddRange(thief.Properties);
            thief.Properties.Clear();
            thief.IsInPrison = true;
            thief.NumberOfSecondsToSpendInPrison = numberOfSecondsInPrison;
            thief.StartPrisonTime();
        }

        public static void PoliceMeetPersonCheck(List<Person> persons)
        {
            var thiefs = persons.OfType<Thief>();
            var polices = persons.OfType<Police>();
            var citizens = persons.OfType<Citizen>();

            foreach (var police in polices)
            {
                foreach (var thief in thiefs)
                {
                    if (InSameLocation(police, thief))
                    {
                        if (thief.HasStolen)
                        {
                            police.Arrest(thief);

                            Program.queue.Enqueue("Polisen haffar en skurk!");
                            Program.bustedthief.Add(thief);
                        }
                        else
                        {
                            Program.queue.Enqueue("Polisen möter en tjuv!");
                        }
                    }

                }
                foreach (var citizen in citizens)
                {
                    if (InSameLocation(police, citizen))
                    {
                        Program.queue.Enqueue("Polisen hälsar på en medborgare");
                    }
                }
            }
        }

        private static bool InSameLocation(Person Person1, Person Person2)
        {
            return Person1.LocationY == Person2.LocationY &&
                   Person1.LocationX == Person2.LocationX;
        }
    }
}
