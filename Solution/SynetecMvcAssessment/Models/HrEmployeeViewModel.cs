namespace InterviewTestTemplatev2.Models
{
    public class HrEmployeeViewModel
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public int HrDepartmentId { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public string FullName { get; set; } 
    }
}