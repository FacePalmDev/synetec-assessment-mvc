using System.Collections.Generic;

using SynetecMvcAssessment.Core.Models;

namespace SynetecMvcAssessment.Core.Contracts
{
    public interface IBonusPoolService
    {
        IEnumerable<EmployeeDomainModel> GetAllEmployees();
        BonusPoolCalculatorResultDomainModel Calculate(BonusPoolCalculatorDomainModel viewModel);
    }
}