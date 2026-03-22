
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MdtrmExam.Data;
using MdtrmExam.Models.Domain;

namespace MdtrmExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public InstructorsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instructor>>> Get()
        {
            var instructors = await _context.Instructors.ToListAsync();
            return Ok(instructors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Instructor>> GetById(Guid id)
        {
            var instructor = await _context.Instructors.FindAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }

            return Ok(instructor);
        }

        [HttpPost]
        public async Task<ActionResult<Instructor>> Post(Instructor instructor)
        {
            instructor.InstructorId = Guid.NewGuid();


            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = instructor.InstructorId }, instructor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, Instructor instructor)
        {
            var existingInstructor = await _context.Instructors.FindAsync(id);

            if (existingInstructor == null)
            {
                return NotFound();
            }

            existingInstructor.EmployeeNo = instructor.EmployeeNo;
            existingInstructor.FirstName = instructor.FirstName;
            existingInstructor.LastName = instructor.LastName;
            existingInstructor.Email = instructor.Email;
            existingInstructor.DepartmentId = instructor.DepartmentId;

            await _context.SaveChangesAsync();

            return Ok(existingInstructor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var instructor = await _context.Instructors.FindAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}