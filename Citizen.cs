namespace TjuvOchPolis
{
    class Citizen : Person
    {
        public bool Robbed { get; set; }
        public Citizen(int locationX, int locationY, int directionX, int directionY, List<String> properties, bool robbed) : base(locationX, locationY, directionX, directionY, properties)
        {
            Robbed = robbed;
        }
    }
}
