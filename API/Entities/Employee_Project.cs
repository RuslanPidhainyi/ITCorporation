namespace API.Models
{
    public class Employee_Project
    {
        ////////////////////////////////////////////////////////////////////
        //Relationships Entities:

        //Many - to - Many 
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!; // Employee n <-> Projects n

        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!; // Employee n <-> Projects n
    }
}