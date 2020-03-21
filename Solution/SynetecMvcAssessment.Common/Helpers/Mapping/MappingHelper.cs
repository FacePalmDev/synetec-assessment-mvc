using AutoMapper;

namespace SynetecMvcAssessment.Common.Helpers.Mapping
{
    public class MappingHelper<TProfile>: IMappable<TProfile> where TProfile : Profile, new()
    {
        private readonly Mapper _mapper;

        public MappingHelper()
        {
            var config = new MapperConfiguration(x =>
            {
                x.AddProfile(new TProfile());
            });

            _mapper = new Mapper(config);

        }

        public TDestination Map<TDestination>(object source) => _mapper.Map<TDestination>(source);

    }
}
