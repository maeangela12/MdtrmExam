namespace MdtrmExam.Models.Domain
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Units { get; set; }
    }
}
