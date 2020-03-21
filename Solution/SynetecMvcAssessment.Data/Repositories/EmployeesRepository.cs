using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SynetecMvcAssessment.Common.Helpers.Mapping;
using SynetecMvcAssessment.Data.Helpers.Mapping;
using SynetecMvcAssessment.Data.Models;

namespace SynetecMvcAssessment.Data.Repositories
{
    public class EmployeesRepository : IRepository<EmployeeDto> 
       
    {
        private readonly MappingHelper<DataMapperProfile> _mappingHelper;
        protected readonly DbSet<HrEmployee> DbSet;

        public EmployeesRepository(DbContext context, MappingHelper<DataMapperProfile> mappingHalper)
        {
            _mappingHelper = mappingHalper;
            DbSet = context.Set<HrEmployee>();
        }

        public IEnumerable<EmployeeDto> GetAll() => _mappingHelper.Map<IEnumerable<EmployeeDto>>(DbSet.AsEnumerable()) ;

        public EmployeeDto Get(uint id) => _mappingHelper.Map<EmployeeDto>(DbSet.Find((int)id));
    }
}
