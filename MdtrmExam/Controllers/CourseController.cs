
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MdtrmExam.Data;
using MdtrmExam.Models.Domain;

namespace MdtrmExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CourseController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> Get()
        {
            var courses = await _context.Courses.ToListAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetById(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult<Course>> Post(Course course)
        {
            course.CourseId = Guid.NewGuid();
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, course);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, Course course)
        {
            var existingCourse = await _context.Courses.FindAsync(id);
            if (existingCourse == null)
            {
                return NotFound();
            }

            existingCourse.Code = course.Code;
            existingCourse.Title = course.Title;
            existingCourse.Units = course.Units;

            await _context.SaveChangesAsync();
            return Ok(existingCourse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}