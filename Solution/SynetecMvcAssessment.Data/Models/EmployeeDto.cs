using System;

namespace SynetecMvcAssessment.Data.Models
{
    public class EmployeeDto
    {
        public uint Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public uint HrDepartmentId { get; set; }
        public string JobTitle { get; set; }
        public uint Salary { get; set; }
        public string FullName { get; set; }
    }
}
