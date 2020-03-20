using AutoMapper;

using SynetecMvcAssessment.Data.Models;

namespace SynetecMvcAssessment.Data.Helpers.Mapping
{
    public class DataMapperProfile : Profile
    {
        public DataMapperProfile()
        {
            CreateMap<HrEmployee, EmployeeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FistName))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Full_Name));
        }
    }
}
