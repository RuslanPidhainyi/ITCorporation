namespace API.DTOs
{
    public class ProjectWithEmployeesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;
        public List<EmployeeDto> Employees { get; set; } = new();
    }
}