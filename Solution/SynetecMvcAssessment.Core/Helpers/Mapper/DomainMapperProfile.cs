using AutoMapper;
using SynetecMvcAssessment.Core.Models;
using SynetecMvcAssessment.Data.Models;

namespace SynetecMvcAssessment.Core.Helpers.Mapper
{
    public class DomainMapperProfile : Profile
    {
        public DomainMapperProfile()
        {
            CreateMap<EmployeeDto, EmployeeDomainModel>();
        }
    }
}