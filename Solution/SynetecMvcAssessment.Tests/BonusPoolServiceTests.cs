using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using InterviewTestTemplatev2.Controllers;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SynetecMvcAssessment.Core.Exceptions;
using SynetecMvcAssessment.Core.Models;
using SynetecMvcAssessment.Core.Services;

namespace SynetecMvcAssessment.Tests
{
    [TestClass]
    public class BonusPoolServiceTests
    {
        [TestMethod]
        public void CanCreate()
        {
            var context = new Mock<IRep>();
            Assert.IsNotNull(new BonusPoolService(context.Object));
        }

        [TestMethod]
        public void ReturnsAllEmployees()
        {
            var hrEmployees = TestHelper.StubEmployees();
            var mockContext = GeneratePopulatedContext(hrEmployees);

            var sut = new BonusPoolService(mockContext.Object);

            var actual = sut.GetAllEmployees();
            var expected = TestHelper.StubEmployees();

            // A cheeky way to compare two complex objects 👌
            Assert.AreEqual(Json.Encode(expected), Json.Encode(actual));

        }

        private static Mock<MvcInterviewV3Entities1> GeneratePopulatedContext(List<HrEmployee> hrEmployees)
        {
            var mockEmployees = TestHelper.GetMockDbSet(hrEmployees.AsQueryable());
            var mockContext = TestHelper.GetMockContext(mockEmployees);
            return mockContext;
        }

        [TestMethod]
        public void ReturnsBonusPoolCalculation()
        {
            var hrEmployees = TestHelper.StubEmployees();
            var mockContext = GeneratePopulatedContext(hrEmployees);
            
            var sut = new BonusPoolService(mockContext.Object);


            const int employeeId = 3;
            var actual = sut.Calculate(
                new BonusPoolCalculatorDomainModel
                {
                    BonusPoolAmount = 500, 
                    SelectedEmployeeId = employeeId
                });

            var expected = new BonusPoolCalculatorResultDomainModel
            {
                BonusPoolAllocation = 357, 
                HrEmployee = hrEmployees.FirstOrDefault(x=>x.Id == employeeId)
            };

            Assert.AreEqual(Json.Encode(expected),Json.Encode(actual));
        }

        [TestMethod]
        [ExpectedException(typeof(EmployeeNotFoundException))]
        public void CalculationWhereUserNotFoundThrowsEmployeeNotFoundException()
        {
            var hrEmployees = TestHelper.StubEmployees();
            var mockContext = GeneratePopulatedContext(hrEmployees);

            var sut = new BonusPoolService(mockContext.Object);
  
            sut.Calculate(new BonusPoolCalculatorDomainModel()
            {
                BonusPoolAmount = 500,
                SelectedEmployeeId = 2
            });

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateThrowsExceptionIfBonusPoolCalculatorModelIsNull()
        {
            var hrEmployees = TestHelper.StubEmployees();
            var mockContext = GeneratePopulatedContext(hrEmployees);
            var sut = new BonusPoolService(mockContext.Object);

            sut.Calculate(null);
        }


    }
}
