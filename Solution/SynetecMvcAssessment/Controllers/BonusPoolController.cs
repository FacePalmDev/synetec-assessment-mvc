using System;
using System.Collections.Generic;
using System.Web.Mvc;
using InterviewTestTemplatev2.Helpers.Mapper;
using InterviewTestTemplatev2.Models;
using SynetecMvcAssessment.Common.Helpers.Mapping;
using SynetecMvcAssessment.Core.Contracts;
using SynetecMvcAssessment.Core.Exceptions;
using SynetecMvcAssessment.Core.Models;

namespace InterviewTestTemplatev2.Controllers
{
    public class BonusPoolController : Controller
    {
        private readonly IBonusPoolService _service;
        private readonly IMappingHelper<ApiMapperProfile> _mappingHelper;

        public BonusPoolController(IBonusPoolService service, IMappingHelper<ApiMapperProfile> mappingHelper)
        {
            _service = service;
            _mappingHelper = mappingHelper;
        }

        // GET: BonusPool
        public ActionResult Index()
        {
            var model = new BonusPoolCalculatorViewModel
            {
                // todo make API model
                AllEmployees = _mappingHelper.Map<IEnumerable<HrEmployeeViewModel>>(_service.GetAllEmployees())
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calculate(BonusPoolCalculatorViewModel viewModel)
        {
            if(viewModel == null) throw new ArgumentNullException("BonusPoolCalculatorViewModel shouldn't be null");

            BonusPoolCalculatorResultViewModel result;

            try
            {
                var domainModel = _mappingHelper.Map<BonusPoolCalculatorDomainModel>(viewModel);
                var domainResult = _service.Calculate(domainModel);
                result = _mappingHelper.Map<BonusPoolCalculatorResultViewModel>(domainResult);
            }
            catch (EmployeeNotFoundException)
            {
                return new HttpNotFoundResult($"Employee was not found.");
            }

            return View(result);
        }
    }

  
}