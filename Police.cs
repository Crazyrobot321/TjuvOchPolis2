namespace TjuvOchPolis
{
    class Police : Person
    {
        public Police(int locationX, int locationY, int directionX, int directionY, List<String> properties) : base(locationX, locationY, directionX, directionY, properties)
        {

        }

        public void Arrest(Thief thief)
        {
            var numberOfStolenProperties = thief.Properties.Count;

            if(numberOfStolenProperties <= 0)
            {
                numberOfStolenProperties = 1;
            }
            var numberOfSecondsInPrison = numberOfStolenProperties * 10;
            Properties.AddRange(thief.Properties);
            thief.Properties.Clear();
            thief.IsInPrison = true;
            thief.NumberOfSecondsToSpendInPrison = numberOfSecondsInPrison;
            Program.queue.Enqueue("A thief has been sentenced to " + numberOfSecondsInPrison + " seconds in prison");
            thief.StartPrisonTime();
        }

        public static void PoliceMeetPersonCheck(List<Person> persons)
        {
            List<Thief> thiefs = persons.OfType<Thief>().ToList(); //Skapar en lista med alla personer som är typen Thief
            List<Police> polices = persons.OfType<Police>().ToList();
            List<Citizen> citizens = persons.OfType<Citizen>().ToList(); 

            foreach (var police in polices)
            {
                foreach (var thief in thiefs)
                {
                    if (InSameLocation(police, thief))
                    {
                        if (thief.HasStolen)
                        {
                            police.Arrest(thief);
                            Program.queue.Enqueue("A Cop caught a thief! ");
                            break;
                        }
                        else
                        {
                            Program.queue.Enqueue("A Cop met a thief! ");
                        }
                    }

                }
                foreach (var citizen in citizens)
                {
                    if (InSameLocation(police, citizen))
                    {
                        Program.queue.Enqueue("A Cop greet a citizen ");
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
