using CompanyEmployees.Presentation.ModelBinders;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<IActionResult> GetCompanies()
        {
            // try {
                //  throw new Exception("Exception");
                  var companies = await _service.CompanyService.GetAllCompaniesAsync(trackChanges: false);
                  return Ok(companies);
            // }
            // catch (Exception ex) {
            //         return StatusCode (500, "Inter-anal Server Error");
            // }
        }

        [HttpGet("{id:guid}", Name = "GetCompanyById")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _service.CompanyService.GetCompanyAsync(id, trackChanges:false);
            return Ok(company);
        }

        [HttpGet("collection/({Ids})", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> Ids)
        {
            var companies = await  _service.CompanyService.GetCompaniesByIdsAsync(Ids,  trackChanges:false);
            return Ok(companies);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyByBulk([FromBody] IEnumerable<CompanyForCreationDto> companies)
        {
            var newcompaniesResponse = await _service.CompanyService.CreateCompanyByBulkAsync(companies);
            // return newcompaniesResponse;
            return CreatedAtRoute("CompanyCollection", new {newcompaniesResponse.Ids}, newcompaniesResponse.companies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null){
                return BadRequest("CompanyForCreationDto object is null");
            }
            var createdcompany = await _service.CompanyService.CreateCompanyAsync(company);
            return CreatedAtRoute("GetCompanyById", new {id = createdcompany.Id}, createdcompany);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
        {
            if (company is null)
                return BadRequest("CompanyForUpdateDto object is null");

            await _service.CompanyService.UpdateCompanyAsync(id, company, trackChanges: true);
            return NoContent();
            
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PatchCompany(Guid id, 
        [FromBody] JsonPatchDocument<CompanyForUpdateDto> patchDoc)
        {
            if (patchDoc is null) 
                // Console.WriteLine("Hello, no p-doc");
                return BadRequest("patchDoc object sent from client is null/Empty.");
           
            var (companyDto,companyInstance) = await _service.CompanyService.GetCompanyForPatchAsync(id, compTrackChanges:true);
            
            patchDoc.ApplyTo(companyDto, ModelState);


            if (!TryValidateModel(companyDto))
                return UnprocessableEntity(ModelState);
            
            await _service.CompanyService.SaveChangesForPatch(companyDto,companyInstance);

            return NoContent();

        }


        [HttpDelete("{companyId:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            await _service.CompanyService.DeleteCompanyAsync(companyId, trackChanges:false);
            return NoContent();
        }

    }
}