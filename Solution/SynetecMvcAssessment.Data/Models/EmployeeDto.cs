using System;

namespace SynetecMvcAssessment.Data.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int HrDepartmentId { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public string FullName { get; set; }
    }
}
