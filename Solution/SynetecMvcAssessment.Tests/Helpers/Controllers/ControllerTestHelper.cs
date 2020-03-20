using System.Collections.Generic;
using SynetecMvcAssessment.Core.Models;

namespace SynetecMvcAssessment.Tests.Helpers.Controllers
{
    internal class ControllerTestHelper : IControllerTestHelper
    {

        public IEnumerable<EmployeeDomainModel> GetStubEmployees() =>
            new List<EmployeeDomainModel>()
            {
                new EmployeeDomainModel()
                {
                    Id = 1,
                    FirstName = "Robert",
                    SecondName = "Martin",
                    Salary = 50
                },
                new EmployeeDomainModel()
                {
                    Id = 2,
                    FirstName = "Ada",
                    SecondName = "Lovelace",
                    Salary = 90
                },
                new EmployeeDomainModel()
                {
                    Id = 3,
                    FirstName = "Cynthia",
                    SecondName = "Breazeal",
                    Salary = 100
                }
            };

    }
}
