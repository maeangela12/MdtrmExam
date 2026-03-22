
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MdtrmExam.Data;
using MdtrmExam.Models.Domain;

namespace MdtrmExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public SectionsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section>>> Get()
        {
            var sections = await _context.Sections.ToListAsync();
            return Ok(sections);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Section>> GetById(Guid id)
        {
            var section = await _context.Sections.FindAsync(id);

            if (section == null)
            {
                return NotFound();
            }

            return Ok(section);
        }

        [HttpPost]
        public async Task<ActionResult<Section>> Post(Section section)
        {
            section.SectionId = Guid.NewGuid();

            _context.Sections.Add(section);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = section.SectionId }, section);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, Section section)
        {
            var existingSection = await _context.Sections.FindAsync(id);

            if (existingSection == null)
            {
                return NotFound();
            }

            existingSection.SectionCode = section.SectionCode;
            existingSection.CourseId = section.CourseId;
            existingSection.InstructorId = section.InstructorId;
            existingSection.Room = section.Room;
            existingSection.Schedule = section.Schedule;

            await _context.SaveChangesAsync();

            return Ok(existingSection);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var section = await _context.Sections.FindAsync(id);

            if (section == null)
            {
                return NotFound();
            }

            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}