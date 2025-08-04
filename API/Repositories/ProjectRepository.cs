using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProjectRepository(AppDbContext context, IMapper mapper) : IProjectRepository
    {
        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await context.Projects
                .OrderBy(p => p.Id)
                .ToListAsync();

            return mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var project = await context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null) return null;
            else return mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectWithEmployeesDto?> GetWithEmployeesAsync(int id)
        {
            var project = await context.Projects
                .Include(p => p.Employee_Projects)
                .ThenInclude(ep => ep.Employee)
                .ThenInclude(e => e.EmployeeDetails)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null) return null;
            else return mapper.Map<ProjectWithEmployeesDto>(project);
        }

        public async Task<Project?> GetEntityByIdAsync(int id)
        {
            return await context.Projects.FindAsync(id);
        }

        public async Task AddAsync(Project project)
        {
            await context.Projects.AddAsync(project);
        }

        public async Task<bool> EditUsingProcedureAsync(int id, CreateUpdateProjectDto projectDto)
        {
            var connectionDb = context.Database.GetDbConnection();
            await connectionDb.OpenAsync();

            using var cmd = connectionDb.CreateCommand();
            cmd.CommandText = "CALL SP_Update_Project(@id, @name, @status)";

            var paramId = cmd.CreateParameter();
            paramId.ParameterName = "id";
            paramId.Value = id;
            cmd.Parameters.Add(paramId);

            var paramName = cmd.CreateParameter();
            paramName.ParameterName = "name";
            paramName.Value = projectDto.Name;
            cmd.Parameters.Add(paramName);

            var paramStatus = cmd.CreateParameter();
            paramStatus.ParameterName = "status";
            paramStatus.Value = projectDto.Status;
            cmd.Parameters.Add(paramStatus);

            await cmd.ExecuteNonQueryAsync();
            return true;
        }

        public void Delete(Project project)
        {
            context.Projects.Remove(project);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddEmployeeToProjectAsync(int projectId, int employeeId)
        {
            if (await context.Employee_Projects.AnyAsync(ep => ep.ProjectId == projectId && ep.EmployeeId == employeeId)) return false;

            context.Employee_Projects.Add(new Employee_Project { ProjectId = projectId, EmployeeId = employeeId });
            return await SaveChangesAsync();
        }

        public async Task<bool> RemoveEmployeeFromProjectAsync(int projectId, int employeeId)
        {
            var entity = await context.Employee_Projects
                .FirstOrDefaultAsync(ep => ep.ProjectId == projectId && ep.EmployeeId == employeeId);

            if (entity == null) return false;

            context.Employee_Projects.Remove(entity);
            return await SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int projectId)
        {
            return await context.Projects.AnyAsync(p => p.Id == projectId);
        }
    }
}