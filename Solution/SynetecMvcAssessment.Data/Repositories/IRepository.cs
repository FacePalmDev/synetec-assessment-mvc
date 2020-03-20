using System.Collections.Generic;

namespace SynetecMvcAssessment.Data.Repositories
{
    public interface IRepository<out TDest>
        where TDest : class
    {
        TDest Get(int id);
        IEnumerable<TDest> GetAll();

    }
}