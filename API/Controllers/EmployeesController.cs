using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(IEmployeeRepository repo, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var employees = await repo.GetAllAsync();

            if (employees == null) return NotFound();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            var employee = await repo.GetByIdAsync(id);

            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(CreateUpdateEmployeeDto employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);
            await repo.AddAsync(employee);

            var success = await repo.SaveChangesAsync();
            if (!success) return BadRequest("Failed to create employee");

            var result = mapper.Map<EmployeeDto>(employee);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDto>> Update(int id, CreateUpdateEmployeeDto employeeDto)
        {
            var employee = await repo.GetEntityByIdAsync(id);
            if (employee == null) return NotFound();

            repo.Edit(employee, employeeDto);

            var success = await repo.SaveChangesAsync();

            if (success) return Ok(mapper.Map<EmployeeDto>(employee));
            return BadRequest("Failed to update employee");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var employee = await repo.GetEntityByIdAsync(id);
            if (employee == null) return NotFound();

            repo.Delete(employee);
            var success = await repo.SaveChangesAsync();

            if (success) return NoContent();
            return BadRequest("Failed to delete employee");
        }
    }
}
