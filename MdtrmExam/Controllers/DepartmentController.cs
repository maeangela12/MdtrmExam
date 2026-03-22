
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MdtrmExam.Data;
using MdtrmExam.Models.Domain;

namespace MdtrmExam .Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public DepartmentsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Get()
        {
            var departments = await _context.Departments.ToListAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetById(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpPost]
        public async Task<ActionResult<Department>> Post(Department department)
        {
            department.DepartmentId = Guid.NewGuid();

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = department.DepartmentId }, department);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(id);

            if (existingDepartment == null)
            {
                return NotFound();
            }

            existingDepartment.Name = department.Name;
            existingDepartment.Code = department.Code;
            existingDepartment.Office = department.Office;

            await _context.SaveChangesAsync();

            return Ok(existingDepartment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}