using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController(IProjectRepository projectRepo, IEmployeeRepository employeeRepo, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
        {
            var projects = await projectRepo.GetAllAsync();

            if(projects == null) return NotFound();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetById(int id)
        {
            var project = await projectRepo.GetByIdAsync(id);

            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpGet("{id}/with-employees")]
        public async Task<ActionResult<ProjectWithEmployeesDto>> GetWithEmployees(int id)
        {
            var project = await projectRepo.GetWithEmployeesAsync(id);

            if(project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Create(CreateUpdateProjectDto projectDto)
        {
            var project = mapper.Map<Project>(projectDto);
            await projectRepo.AddAsync(project);

            var success = await projectRepo.SaveChangesAsync();
            if (!success) return BadRequest("Failed to create project");

            var result = mapper.Map<ProjectDto>(project);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectDto>> Update(int id, CreateUpdateProjectDto projectDto)
        {
            var project = await projectRepo.GetEntityByIdAsync(id);
            if (project == null) return NotFound();

            var success = await projectRepo.EditUsingProcedureAsync(id, projectDto);
            if(!success) return BadRequest("Failed to update project");

            var updatedProject = await projectRepo.GetByIdAsync(id);
            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var project = await projectRepo.GetEntityByIdAsync(id);
            if (project == null) return NotFound();

            projectRepo.Delete(project);

            var success = await projectRepo.SaveChangesAsync();
            
            if (success) return NoContent();
            return BadRequest("Failed to delete project");
        }

        [HttpPost("{projectId}/employees/{employeeId}")]
        public async Task<ActionResult> AddEmployeeToProject(int projectId, int employeeId)
        {
            var projectExists = await projectRepo.ExistsAsync(projectId);
            if (!projectExists) return NotFound("Project not found");

            var employeeExists = await employeeRepo.ExistsAsync(employeeId);
            if (!employeeExists) return NotFound("Employee not found");

            var project = await projectRepo.AddEmployeeToProjectAsync(projectId, employeeId);

            if (project) return Ok();
            return BadRequest("Employee already in project or invalid IDs");
        }

        [HttpDelete("{projectId}/employees/{employeeId}")]
        public async Task<ActionResult> RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            var projectExists = await projectRepo.ExistsAsync(projectId);
            if (!projectExists) return NotFound("Project not found");

            var employeeExists = await employeeRepo.ExistsAsync(employeeId);
            if (!employeeExists) return NotFound("Employee not found");

            var project = await projectRepo.RemoveEmployeeFromProjectAsync(projectId, employeeId);

            if (project) return NoContent();
            return NotFound("Employee not found in project");
        }

        [HttpPut("simulate-not-found-project/{id}")]
        public async Task<ActionResult<ProjectDto>> SimulateNotFoundProjectById(int id, CreateUpdateProjectDto projectDto)
        {
            var project = await projectRepo.GetEntityByIdAsync(id);
            if (project != null) return NotFound();

            var success = await projectRepo.EditUsingProcedureAsync(id, projectDto);
            if (!success) return BadRequest("Failed to update project");

            var updatedProject = await projectRepo.GetByIdAsync(id);
            return Ok(updatedProject);
        }

        [HttpPost("simulate-bad-request-create-project")]
        public async Task<ActionResult<ProjectDto>> SimulateBadRequestCreate(CreateUpdateProjectDto projectDto)
        {
            var project = mapper.Map<Project>(projectDto);
            await projectRepo.AddAsync(project);

            var success = await projectRepo.SaveChangesAsync();
            if (success) return BadRequest("Failed to create project");

            var result = mapper.Map<ProjectDto>(project);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}