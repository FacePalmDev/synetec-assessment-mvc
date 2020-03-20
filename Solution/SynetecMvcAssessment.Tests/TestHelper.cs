using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using InterviewTestTemplatev2.Data;
using Moq;
using SynetecMvcAssessment.Core.Models;

namespace SynetecMvcAssessment.Tests
{
    internal static class TestHelper
    {
        internal static List<EmployeeDomainModel> StubEmployees() =>
            new List<EmployeeDomainModel>
            {
                new EmployeeDomainModel
                {
                    Id = 1,
                    FirstName = "Scott",
                    SecondName = "Hanselman",
                    Salary = 50
                },
                new EmployeeDomainModel
                {
                    Id = 3,
                    FirstName = "Julie",
                    SecondName = "Lerman",
                    Salary = 125
                }

            };

        // a helper to make dbset queryable
        // I found this here: https://stackoverflow.com/questions/20509315/moqing-entity-framework-6-include-using-dbset
        internal static Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
            return mockSet;
        }

        internal static Mock<MvcInterviewV3Entities1> GetMockContext(Mock<DbSet<HrEmployee>> mockEmployees)
        {
            var context = new Mock<MvcInterviewV3Entities1>();
            context.SetupGet(x => x.HrEmployees)
                .Returns(mockEmployees.Object);

            return context;
        }
    }
}