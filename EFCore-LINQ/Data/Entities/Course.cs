namespace EFCore_LINQ.Data.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public int? StudentId { get; set; }
        public Student Student { get; set; }
    }
}
