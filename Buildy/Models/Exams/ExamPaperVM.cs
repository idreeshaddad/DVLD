using System.ComponentModel.DataAnnotations;

namespace Buildy.Models.Exams
{
    public class ExamPaperVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Grade { get; set; }

        [Required]
        public int GradeTop { get; set; }

        public int StudentId { get; set; }
        public string StudentName { get; set; }
    }
}
