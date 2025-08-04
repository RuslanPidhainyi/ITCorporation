using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository(AppDbContext context, IMapper mapper) : IEmployeeRepository
    {
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await context.Employees
                .Include(e => e.EmployeeDetails)
                .ToListAsync();

            return mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await context.Employees
                .Include(e => e.EmployeeDetails)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null) return null;
            else return mapper.Map<EmployeeDto>(employee);
        }

        public async Task<Employee?> GetEntityByIdAsync(int id)
        {
            return await context.Employees
                .Include(e => e.EmployeeDetails)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
        }

        public void Delete(Employee employee)
        {
            context.Employees.Remove(employee);
        }

        public void Edit(Employee employee, CreateUpdateEmployeeDto employeeDto)
        {
            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;

            if (employee.EmployeeDetails == null)
            {
                employee.EmployeeDetails = new EmployeeDetails();
            }

            employee.EmployeeDetails.Email = employeeDto.Email;
            employee.EmployeeDetails.Role = employeeDto.Role;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<bool> ExistsAsync(int employeeId)
        {
            return await context.Employees.AnyAsync(e => e.Id == employeeId);
        }
    }
}