namespace SynetecMvcAssessment.Core.Contracts
{
    public interface ICalculatable<TInput,TResult>
    {
        TResult Calculate(TInput viewModel);
    }

}
