using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<Employee?> GetEntityByIdAsync(int id);
        Task AddAsync(Employee employee);
        void Delete(Employee employee);
        Task<bool> SaveChangesAsync();
        void Edit(Employee employee, CreateUpdateEmployeeDto dto);
        Task<bool> ExistsAsync(int employeeId);
    }
}