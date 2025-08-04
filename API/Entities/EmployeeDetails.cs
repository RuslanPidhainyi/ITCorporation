using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class EmployeeDetails
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;

        ////////////////////////////////////////////////////////////////////
        //Relationships Entities:

        //One - to - One
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!; // EmployeeDetails 1 <-> Employee 1 
    }
}