namespace MdtrmExam.Models.Domain
{
    public class Instructor
    {
        public Guid InstructorId { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
