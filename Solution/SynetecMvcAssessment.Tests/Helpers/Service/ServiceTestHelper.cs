using System.Collections.Generic;
using System.Linq;
using Moq;
using SynetecMvcAssessment.Common.Helpers.Mapping;
using SynetecMvcAssessment.Core.Helpers.Mapper;
using SynetecMvcAssessment.Core.Services;
using SynetecMvcAssessment.Data.Models;
using SynetecMvcAssessment.Data.Repositories;

namespace SynetecMvcAssessment.Tests.Helpers.Service
{
    internal class ServiceTestHelper : IServiceTestHelper
    {
        public  BonusPoolService GetBonusPoolService(bool populateRepository = false)
        {
            var context = new Mock<IRepository<EmployeeDto>>();

            if (populateRepository)
            {
                context.Setup(x => x.GetAll()).Returns(GetStubEmployees());
                context.Setup(x => x.Get(3)).Returns(GetStubEmployees().LastOrDefault(x => x.Id == 3));
            }

            var mapper = new MappingHelper<DomainMapperProfile>();
            var bonusPoolService = new BonusPoolService(context.Object, mapper);
            return bonusPoolService;
        }

        public IEnumerable<EmployeeDto> GetStubEmployees() => new List<EmployeeDto>
        {
            new EmployeeDto
            {
                Id = 1,
                FirstName = "Scott",
                SecondName = "Hanselman",
                Salary = 50
            },
            new EmployeeDto
            {
                Id = 2,
                FirstName = "Julie",
                SecondName = "Lerman",
                Salary = 125
            },
            new EmployeeDto
            {
                Id = 3,
                FirstName = "Jon",
                SecondName = "Skeet",
                Salary = 25
            },
        };
    }

}

