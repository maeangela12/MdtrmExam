
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MdtrmExam.Data;
using MdtrmExam.Models.Domain;

namespace MdtrmExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public EnrollmentsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> Get()
        {
            var enrollment = await _context.Enrollments.ToListAsync();
            return Ok(enrollment);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetById(Guid id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {

                return NotFound();

            }

            return Ok(enrollment);
        }

        [HttpPost]
        public async Task<ActionResult<Enrollment>> Post(Enrollment enrollment)
        {
            enrollment.EnrollmentId = Guid.NewGuid();

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = enrollment.EnrollmentId }, enrollment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, Enrollment enrollment)
        {
            var existingEnrollment = await _context.Enrollments.FindAsync(id);

            if (existingEnrollment == null)
            {
                return NotFound();
            }

            existingEnrollment.StudentId = enrollment.StudentId;
            existingEnrollment.CourseId = enrollment.CourseId;
            existingEnrollment.Semester = enrollment.Semester;
            existingEnrollment.Grade = enrollment.Grade;

            await _context.SaveChangesAsync();

            return Ok(existingEnrollment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}