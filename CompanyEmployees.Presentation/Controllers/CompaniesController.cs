using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

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

        [HttpGet("{id:guid}", Name = "GetCompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, trackChanges:false);
            return Ok(company);
        }

        [HttpGet("collection/({Ids})", Name = "CompanyCollection")]
        public IActionResult GetCommpaniesByIds(IEnumerable<Guid> Ids)
        {
            var companies = _service.CompanyService.GetCompaniesById(Ids,  trackChanges:false);
            return Ok(companies);
        }

        [HttpPost("collection")]
        public IActionResult CreateCompanyByBulk([FromBody] IEnumerable<CompanyForCreationDto> companies)
        {
            var newcompaniesResponse = _service.CompanyService.CreateCompanyByBulk(companies);
            // return newcompaniesResponse;
            return CreatedAtRoute("CompanyCollection", new {newcompaniesResponse.Ids}, newcompaniesResponse.companies);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null){
                return BadRequest("CompanyForCreationDto object is null");
            }
            var createdcompany = _service.CompanyService.CreateCompany(company);
            return CreatedAtRoute("GetCompanyById", new {id = createdcompany.Id}, createdcompany);
        }

    }
}