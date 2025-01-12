using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CompaniesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            // try {
                //  throw new Exception("Exception");
                  var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
                  return Ok(companies);
            // }
            // catch (Exception ex) {
            //         return StatusCode (500, "Inter-anal Server Error");
            // }
        }

    }
}