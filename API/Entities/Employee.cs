namespace API.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        ////////////////////////////////////////////////////////////////////
        //Relationships Entities:

        //One - to - Many
        public List<Employee_Project> Employee_Projects { get; set; } = new(); // Employee 1 <-> Employee_Projects n

        //One - to - One
        public EmployeeDetails? EmployeeDetails { get; set; } //Employee 1 <-> EmployeeDetails 1
    }
}