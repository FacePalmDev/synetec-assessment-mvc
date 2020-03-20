using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynetecMvcAssessment.Common.Helpers.Mapping;
using SynetecMvcAssessment.Core.Exceptions;
using SynetecMvcAssessment.Core.Helpers.Mapper;
using SynetecMvcAssessment.Core.Models;
using SynetecMvcAssessment.Data.Models;
using SynetecMvcAssessment.Tests.Helpers.Service;

namespace SynetecMvcAssessment.Tests
{
    [TestClass]
    public class BonusPoolServiceTests
    {
        private readonly IServiceTestHelper _testHelper;
        private IEnumerable<EmployeeDto> _stubEmployees;

        public BonusPoolServiceTests()
        {
           _testHelper = new ServiceTestHelper();
           _stubEmployees = _testHelper.GetStubEmployees();
        }

        [TestMethod]
        public void CanCreate()
        {
            var sut = _testHelper.GetBonusPoolService();
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void ReturnsAllEmployees()
        {
            var sut = _testHelper.GetBonusPoolService(populateRepository: true);

            var actual = sut.GetAllEmployees();
            var expected = _stubEmployees;
            
            // A cheeky way to compare two complex objects 👌
            Assert.AreEqual(Json.Encode(expected), Json.Encode(actual));
        }

        [TestMethod]
        [ExpectedException(typeof(EmployeeNotFoundException))]
        public void CalculationWhereUserNotFoundThrowsEmployeeNotFoundException()
        {
            var sut = _testHelper.GetBonusPoolService(populateRepository: true);

            var bonusPoolCalculatorDomainModel = new BonusPoolCalculatorDomainModel()
            {
                BonusPoolAmount = 500,
                SelectedEmployeeId = 100
            };

            sut.Calculate(bonusPoolCalculatorDomainModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateThrowsExceptionIfBonusPoolCalculatorModelIsNull()
        {
            var sut = _testHelper.GetBonusPoolService(populateRepository: true);

            sut.Calculate(null);
        }

        [TestMethod]
        public void ReturnsBonusPoolCalculation()
        {
            var sut = _testHelper.GetBonusPoolService(populateRepository: true);


            const int employeeId = 3;
            var actual = sut.Calculate(
                new BonusPoolCalculatorDomainModel
                {
                    BonusPoolAmount = 500,
                    SelectedEmployeeId = employeeId
                });

            var expected = new BonusPoolCalculatorResultDomainModel
            {
                BonusPoolAllocation = 62,
                HrEmployee = new MappingHelper<DomainMapperProfile>()
                    .Map<EmployeeDomainModel>(_testHelper.GetStubEmployees().FirstOrDefault(x => x.Id == employeeId))
            };

            Assert.AreEqual(Json.Encode(expected), Json.Encode(actual));
        }

    }
}
