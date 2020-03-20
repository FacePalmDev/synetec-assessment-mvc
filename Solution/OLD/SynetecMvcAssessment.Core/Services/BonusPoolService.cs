using SynetecMvcAssessment.Core.Contracts;
using SynetecMvcAssessment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SynetecMvcAssessment.Core.Services
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly MvcInterviewV3Entities1 _db;

        public BonusPoolService(MvcInterviewV3Entities1 db)
        {
            _db = db;
        }

        public List<HrEmployee> GetAllEmployees()
        {
            return _db.HrEmployees.ToList();
        }

        public BonusPoolCalculatorResultDomainModel Calculate(BonusPoolCalculatorDomainModel domainModel)
        {

            if (domainModel == null)
                throw new ArgumentNullException("viewModel model to calculate was null");

            var selectedEmployeeId = domainModel.SelectedEmployeeId;
            var totalBonusPool = domainModel.BonusPoolAmount;

            //load the details of the selected employee using the ID
            var hrEmployee = _db.HrEmployees.FirstOrDefault(item => item.ID == selectedEmployeeId);

            if (hrEmployee == null)
            {
                throw new EmployeeNotFoundException();
            }

            var employeeSalary = hrEmployee.Salary;

            //get the total salary budget for the company
            var totalSalary = _db.HrEmployees.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            var bonusPercentage = (decimal)employeeSalary / (decimal)totalSalary;
            var bonusAllocation = (int)(bonusPercentage * totalBonusPool);

            var result = new BonusPoolCalculatorResultModel();
            result.hrEmployee = hrEmployee;
            result.bonusPoolAllocation = bonusAllocation;

            return result;
        }
    }
}