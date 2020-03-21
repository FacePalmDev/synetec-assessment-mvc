using SynetecMvcAssessment.Core.Contracts;
using SynetecMvcAssessment.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SynetecMvcAssessment.Common.Helpers.Mapping;
using SynetecMvcAssessment.Core.Exceptions;
using SynetecMvcAssessment.Core.Helpers.Mapper;
using SynetecMvcAssessment.Data.Models;
using SynetecMvcAssessment.Data.Repositories;

namespace SynetecMvcAssessment.Core.Services
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly IRepository<EmployeeDto> _repository;
        private readonly MappingHelper<DomainMapperProfile> _mappingHelper;


        //todo: make all constructor params interfaces!
        public BonusPoolService(IRepository<EmployeeDto> repository, MappingHelper<DomainMapperProfile> mappingHelper)
        {
            _repository = repository;
            _mappingHelper = mappingHelper;
        }

        public IEnumerable<EmployeeDomainModel> GetAllEmployees()
        {
            return _mappingHelper.Map<IEnumerable<EmployeeDomainModel>>(_repository.GetAll());
        }

        public BonusPoolCalculatorResultDomainModel Calculate(BonusPoolCalculatorDomainModel domainModel)
        {
            if (domainModel == null)
                throw new ArgumentNullException("viewModel model to calculate was null");

            var selectedEmployeeId = domainModel.SelectedEmployeeId;
            var totalBonusPool = domainModel.BonusPoolAmount;

            //load the details of the selected employee using the ID
            var hrEmployee = _repository.Get(selectedEmployeeId);

            if (hrEmployee == null)
            {
                throw new EmployeeNotFoundException();
            }

            var employeeSalary = hrEmployee.Salary;

            //get the total salary budget for the company
            var totalSalary = _repository.GetAll().Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            var bonusPercentage = employeeSalary / (decimal)totalSalary;
            var bonusAllocation = (int)(bonusPercentage * totalBonusPool);

            var result = new BonusPoolCalculatorResultDomainModel
            {
                HrEmployee = _mappingHelper.Map<EmployeeDomainModel>(hrEmployee),
                BonusPoolAllocation = bonusAllocation
            };

            return result;
        }



    }
}