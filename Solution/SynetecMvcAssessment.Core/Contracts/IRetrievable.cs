using System.Collections.Generic;

namespace SynetecMvcAssessment.Core.Contracts
{
    public interface IRetrievable<T>
    {
        IEnumerable<T> GetAll();
    }
}