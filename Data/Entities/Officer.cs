using DVLD.Enums;
namespace DVLD.Data.Entities
{
    public class Officer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BadgeNumber  { get; set; }
        public Rank Rank { get; set; }
    }
}
