using SynetecMvcAssessment.Core.Services;
using SynetecMvcAssessment.Data.Models;

namespace SynetecMvcAssessment.Tests.Helpers.Service
{
    internal interface IServiceTestHelper : ITestHelper<EmployeeDto>
    {
        BonusPoolService GetBonusPoolService(bool populateRepository = false);
      
    }
}