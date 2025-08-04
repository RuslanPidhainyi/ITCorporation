using API.DTOs;
using API.Models;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<ProjectDto?> GetByIdAsync(int id);
        Task<ProjectWithEmployeesDto?> GetWithEmployeesAsync(int id);
        Task<Project?> GetEntityByIdAsync(int id);
        Task AddAsync(Project project);
        Task<bool> EditUsingProcedureAsync(int id, CreateUpdateProjectDto projectDto);
        void Delete(Project project);
        Task<bool> SaveChangesAsync();
        Task<bool> AddEmployeeToProjectAsync(int projectId, int employeeId);
        Task<bool> RemoveEmployeeFromProjectAsync(int projectId, int employeeId);
        Task<bool> ExistsAsync(int projectId);
    }
}