namespace MdtrmExam.Models.Domain
{
    public class Section
    {
        public Guid SectionId { get; set; }
        public string SectionCode { get; set; }
        public Guid CourseId { get; set; }
        public Guid InstructorId { get; set; }
        public string Room { get; set; }
        public string Schedule { get; set; }
    }
}
