using SynetecMvcAssessment.Core.Models;

namespace SynetecMvcAssessment.Core.Contracts
{
    public interface IBonusPoolService: IRetrievable<EmployeeDomainModel>, ICalculatable<BonusPoolCalculatorDomainModel, BonusPoolCalculatorResultDomainModel>
    {

    }

}