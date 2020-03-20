using InterviewTestTemplatev2.Helpers.Mapper;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using SynetecMvcAssessment.Common.Helpers.Mapping;

namespace InterviewTestTemplatev2.DependencyResolution
{
    public class ApiRegistry : Registry
    {
        public ApiRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });

            For<IMappingHelper<ApiMapperProfile>>().Use<MappingHelper<ApiMapperProfile>>();

        }
    }
}