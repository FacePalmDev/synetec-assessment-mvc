namespace InterviewTestTemplatev2.Models
{
    public class HrEmployeeViewModel
    {
        public uint Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public uint HrDepartmentId { get; set; }
        public string JobTitle { get; set; }
        public uint Salary { get; set; }
        public string FullName { get; set; } 
    }
}