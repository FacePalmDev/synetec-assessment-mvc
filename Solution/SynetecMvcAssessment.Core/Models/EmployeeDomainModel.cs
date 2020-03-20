namespace SynetecMvcAssessment.Core.Models
{
    public class EmployeeDomainModel
    {
        public int Id { get; set; } // todo map
        public string FirstName { get; set; }// todo map
        public string SecondName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public int HrDepartmentId { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public string FullName { get; set; } // todo map
    }
}
