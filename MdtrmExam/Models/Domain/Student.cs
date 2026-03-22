namespace MdtrmExam.Models.Domain
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string StudentNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; }
    }
}
