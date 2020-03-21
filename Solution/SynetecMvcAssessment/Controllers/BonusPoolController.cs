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
        private readonly IMappable<ApiMapperProfile> _mappable;

        public BonusPoolController(IBonusPoolService service, IMappable<ApiMapperProfile> mappable)
        {
            _service = service;
            _mappable = mappable;
        }

        // GET: BonusPool
        public ActionResult Index()
        {
            var model = new BonusPoolCalculatorViewModel
            {
                // todo make API model
                AllEmployees = _mappable.Map<IEnumerable<HrEmployeeViewModel>>(_service.GetAll())
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
                var domainModel = _mappable.Map<BonusPoolCalculatorDomainModel>(viewModel);
                var domainResult = _service.Calculate(domainModel);
                result = _mappable.Map<BonusPoolCalculatorResultViewModel>(domainResult);
            }
            catch (EmployeeNotFoundException)
            {
                return new HttpNotFoundResult($"Employee was not found.");
            }

            return View(result);
        }
    }

  
}