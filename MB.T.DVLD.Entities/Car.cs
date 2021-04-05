namespace MB.T.DVLD.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Manu { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }

        public int? DriverId { get; set; }
        public Driver Driver { get; set; }
        public int? InsuranceId { get; set; }
        public Insurance Insurance { get; set; }
    }
}
