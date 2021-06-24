namespace MB.T.DVLD.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public int InspectorId { get; set; }
        public Inspector Inspector { get; set; }
    }
}
