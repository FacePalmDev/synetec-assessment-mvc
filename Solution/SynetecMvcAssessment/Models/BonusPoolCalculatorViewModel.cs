using System.Collections.Generic;

namespace InterviewTestTemplatev2.Models
{
    public class BonusPoolCalculatorViewModel
    {

        public int BonusPoolAmount { get; set; }
        public IEnumerable<HrEmployeeViewModel> AllEmployees { get; set; }        
        public int SelectedEmployeeId { get; set; }

    }
}