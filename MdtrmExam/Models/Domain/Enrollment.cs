namespace MdtrmExam.Models.Domain
{
    public class Enrollment
    {

        public Guid EnrollmentId { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public string Semester { get; set; }
        public double Grade { get; set; }
    }
}
