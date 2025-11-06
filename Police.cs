namespace TjuvOchPolis
{
    class Police : Person
    {
        public Police(int locationX, int locationY, int directionX, int directionY, List<String> properties) : base(locationX, locationY, directionX, directionY, properties)
        {

        }

        public void Arrest(Thief thief)
        {
            // Kollar om tjuven redan är i fängelse
            if (thief.IsInPrison)
                return;

            int numberOfStolenProperties = thief.Properties.Count;
            //Ska finnas minst 1 stulen properties
            if (numberOfStolenProperties <= 0)
                numberOfStolenProperties = 1;

            int numberOfSecondsInPrison = numberOfStolenProperties * 10;

            // Polisen beslagtar allt från tjuven
            Properties.AddRange(thief.Properties);
            thief.Properties.Clear();

            // Skicka till fängelse och starta timer
            thief.IsInPrison = true;
            thief.NumberOfSecondsToSpendInPrison = numberOfSecondsInPrison;
            Program.queue.Enqueue($"A thief has been sentenced to {numberOfSecondsInPrison} seconds in prison");

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
                    if (Person.InSameLocation(police, thief))
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
                    if (Person.InSameLocation(police, citizen))
                    {
                        Program.queue.Enqueue("A Cop greet a citizen ");
                    }
                }
            }
        }

        
    }
}
