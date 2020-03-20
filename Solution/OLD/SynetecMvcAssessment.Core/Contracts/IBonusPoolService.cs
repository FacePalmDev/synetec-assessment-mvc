using System.Collections.Generic;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Models;

namespace SynetecMvcAssessment.Core.Contracts
{
    public interface IBonusPoolService
    {
        List<HrEmployee> GetAllEmployees();
        BonusPoolCalculatorResultModel Calculate(BonusPoolCalculatorViewModel viewModel);
    }
}