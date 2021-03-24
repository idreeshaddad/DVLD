namespace DVLD.Data.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Manu { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public Driver Driver { get; set; }
    }
}
