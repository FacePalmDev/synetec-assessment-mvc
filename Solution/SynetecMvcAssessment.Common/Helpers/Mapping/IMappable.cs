namespace SynetecMvcAssessment.Common.Helpers.Mapping
{
    public interface IMappable<TProfile>
    {
        TDestination Map<TDestination>(object source);
    }
}