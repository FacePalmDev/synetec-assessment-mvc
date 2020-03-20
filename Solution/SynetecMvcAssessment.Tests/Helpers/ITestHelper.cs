using System.Collections.Generic;

namespace SynetecMvcAssessment.Tests.Helpers
{
    internal interface ITestHelper<TEmployeeModel>
    {
        IEnumerable<TEmployeeModel> GetStubEmployees();
    }
}