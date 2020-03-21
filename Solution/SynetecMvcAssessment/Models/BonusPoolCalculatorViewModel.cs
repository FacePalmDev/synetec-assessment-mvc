using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InterviewTestTemplatev2.Models
{
    public class BonusPoolCalculatorViewModel
    {

        // I know that the validation won't work in these instances as the inputs are 
        // determined by the controls but I wanted to show that I've thought about this. 
        [Required(ErrorMessage = "This field is required")]
        public uint BonusPoolAmount { get; set; } //todo: Consider making this decimal? Seek clarification here.
        public IEnumerable<HrEmployeeViewModel> AllEmployees { get; set; }       
        [Required(ErrorMessage = "This field is required")]
        public uint SelectedEmployeeId { get; set; }

    }
}