namespace Buildy.Data.Entities
{
    public class ExamPaper
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
        public int? StudentId { get; set; }
    }
}
