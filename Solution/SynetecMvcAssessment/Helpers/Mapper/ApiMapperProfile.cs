using AutoMapper;
using InterviewTestTemplatev2.Models;
using SynetecMvcAssessment.Core.Models;

namespace InterviewTestTemplatev2.Helpers.Mapper
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<BonusPoolCalculatorDomainModel, BonusPoolCalculatorViewModel>();
            CreateMap<BonusPoolCalculatorResultDomainModel, BonusPoolCalculatorResultViewModel>();
            CreateMap<BonusPoolCalculatorViewModel, BonusPoolCalculatorDomainModel>();
            CreateMap<EmployeeDomainModel, HrEmployeeViewModel>();

        }
    }
}
