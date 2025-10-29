namespace TjuvOchPolis
{
    class Citizen : Person
    {
        public Citizen(int locationX, int locationY, int directionX, int directionY, List<String> properties) : base(locationX, locationY, directionX, directionY, properties)
        {
            //properties.AddRange("Keys", "Mobile", "Wallet", "Watch", "Jewlery");
        }
    }
}
