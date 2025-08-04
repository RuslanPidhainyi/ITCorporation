namespace API.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;

        ////////////////////////////////////////////////////////////////////
        //Relationships Entities:

        //One - to - Many
        public List<Employee_Project> Employee_Projects { get; set; } = new(); // Projects 1 <-> Employee_Projects n
    }
}