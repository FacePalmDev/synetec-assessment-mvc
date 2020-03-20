using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using InterviewTestTemplatev2.Controllers;
using InterviewTestTemplatev2.Helpers.Mapper;
using InterviewTestTemplatev2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SynetecMvcAssessment.Common.Helpers.Mapping;
using SynetecMvcAssessment.Core.Contracts;
using SynetecMvcAssessment.Core.Exceptions;
using SynetecMvcAssessment.Core.Models;

namespace SynetecMvcAssessment.Tests
{
    [TestClass]
    public class BonusPoolControllerTests
    {
        [TestMethod]
        public void CanCreate()
        {
            var serviceMock = new Mock<IBonusPoolService>();
            var sut = new BonusPoolController(serviceMock.Object, new MappingHelper<ApiMapperProfile>());

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void ReturnsAllEmployees()
        {
            var serviceMock = new Mock<IBonusPoolService>();
            var allStubEmployees = TestHelper.StubEmployees();
            serviceMock
                .Setup(s => s.GetAllEmployees())
                .Returns(allStubEmployees);
            
            var sut = new BonusPoolController(serviceMock.Object, new MappingHelper<ApiMapperProfile>());

            var result = sut.Index() as ViewResult;
            var actual = (result.Model as BonusPoolCalculatorViewModel).AllEmployees;

            Assert.AreEqual(Json.Encode(allStubEmployees) , Json.Encode(actual));
        }

        [TestMethod]
        public void ReturnsBonusPoolCalculation()
        {
            // arrange
            var serviceMock = new Mock<IBonusPoolService>();
            var employees = TestHelper.StubEmployees();

            var bonusPoolCalculatorResultModel = new BonusPoolCalculatorResultDomainModel
            {
                BonusPoolAllocation = 123456,
                HrEmployee = employees.Last()
            };

            serviceMock
                .Setup(s => s.Calculate(It.IsAny<BonusPoolCalculatorDomainModel>()))
                .Returns(bonusPoolCalculatorResultModel);

            // act
            var result = new BonusPoolController(serviceMock.Object, new MappingHelper<ApiMapperProfile>()).Calculate(new BonusPoolCalculatorViewModel
                {
               
                BonusPoolAmount = 100,
                SelectedEmployeeId = employees.Last().Id
            })
                as ViewResult;

            var actual = result?.Model as BonusPoolCalculatorResultDomainModel;

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(bonusPoolCalculatorResultModel.BonusPoolAllocation, actual.BonusPoolAllocation);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateThrowsExceptionIfBonusPoolCalculatorModelIsNull()
        {
            // arrange
            var serviceMock = new Mock<IBonusPoolService>();

            serviceMock
                .Setup(s => s.Calculate(It.IsAny<BonusPoolCalculatorDomainModel>()))
                .Returns(new BonusPoolCalculatorResultDomainModel());

            // act
            new BonusPoolController(serviceMock.Object, new MappingHelper<ApiMapperProfile>()).Calculate(null);
        }


        [TestMethod]
        public void CalculationWhereUserNotFoundReturns404()
        {
            var serviceMock = new Mock<IBonusPoolService>();
            var allStubEmployees = TestHelper.StubEmployees();

            serviceMock
                .Setup(s => s.GetAllEmployees())
                .Returns(allStubEmployees);
            
            serviceMock.Setup(x =>
                x.Calculate(It.IsAny<BonusPoolCalculatorDomainModel>()))
                .Throws<EmployeeNotFoundException>();

            var model = new BonusPoolCalculatorViewModel();

            var actual =
                new BonusPoolController(serviceMock.Object, new MappingHelper<ApiMapperProfile>())
                    .Calculate(model);

            Assert.IsInstanceOfType(actual, typeof(HttpNotFoundResult));
        }

    }
}
