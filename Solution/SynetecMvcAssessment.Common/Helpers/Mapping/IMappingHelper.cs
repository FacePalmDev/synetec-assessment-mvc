namespace SynetecMvcAssessment.Common.Helpers.Mapping
{
    public interface IMappingHelper<TProfile>
    {
        TDestination Map<TDestination>(object source);
    }
}