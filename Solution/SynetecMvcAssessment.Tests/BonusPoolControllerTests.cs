using System;
using System.Collections.Generic;
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
using SynetecMvcAssessment.Tests.Helpers.Controllers;

namespace SynetecMvcAssessment.Tests
{
    [TestClass]
    public class BonusPoolControllerTests
    {
        private readonly IControllerTestHelper _testHelper;
        private readonly IList<EmployeeDomainModel> _stubEmployees;

        public BonusPoolControllerTests()
        {
            _testHelper = new ControllerTestHelper();
            // makes sense to enumerate this once here as it's a stub and doesn't change.
            _stubEmployees = _testHelper.GetStubEmployees().ToList();
        }

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
            var allStubEmployees = _stubEmployees;

            serviceMock
            .Setup(s => s.GetAll())
            .Returns(_stubEmployees);

            var sut = new BonusPoolController(serviceMock.Object, new MappingHelper<ApiMapperProfile>());

            var result = sut.Index() as ViewResult;
            var actual = (result.Model as BonusPoolCalculatorViewModel).AllEmployees;

            Assert.AreEqual(Json.Encode(allStubEmployees), Json.Encode(actual));
        }


        [TestMethod]
        public void ReturnsBonusPoolCalculation()
        {
            // arrange
            var serviceMock = new Mock<IBonusPoolService>();
            var allStubEmployees = _stubEmployees;

            var bonusPoolCalculatorResultModel = new BonusPoolCalculatorResultDomainModel
            {
                BonusPoolAllocation = 100,
                HrEmployee = allStubEmployees.Last()
            };

            serviceMock
                .Setup(s => s.Calculate(It.IsAny<BonusPoolCalculatorDomainModel>()))
                .Returns(bonusPoolCalculatorResultModel);

            // act
            var result = new BonusPoolController(serviceMock.Object, new MappingHelper<ApiMapperProfile>())
                    .Calculate(new BonusPoolCalculatorViewModel
                    {
                        
                        BonusPoolAmount = 100,
                        SelectedEmployeeId = allStubEmployees.Last().Id
                    })
                as ViewResult;

            var actual = result?.Model as BonusPoolCalculatorResultViewModel;

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(Json.Encode(bonusPoolCalculatorResultModel.BonusPoolAllocation), Json.Encode(actual.BonusPoolAllocation));
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
         

            serviceMock
                .Setup(s => s.GetAll())
                .Returns(_stubEmployees);

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
