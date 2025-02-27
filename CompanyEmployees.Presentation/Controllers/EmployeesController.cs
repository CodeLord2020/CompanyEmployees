using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace CompanyEmployees.Presentation.Controllers
{
    
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("~/api/employees")]
        public async Task<IActionResult> GetAllEmployees(
            [FromQuery] EmployeeParameters employeeparameters
        )
        {
            var employees = await _service.EmployeeService.GetAllEmployeesAsync(employeeparameters,trackChanges:false);
            return Ok(employees);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId,
        [FromQuery] EmployeeParameters employeeParameters)
        {
            var pagedResult = await _service.EmployeeService.GetEmployeesAsync(companyId, employeeParameters, trackChanges:false);
            Response.Headers.Append("X-Pagination",
                JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.employees);
        }

        [HttpGet("{employeeId:guid}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployee(Guid employeeId, Guid companyId)
        {
            var employee = await  _service.EmployeeService.GetEmployeeAsync(employeeId, companyId, trackChanges:false);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDto object is null");

            var employeeToReturn = await _service.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employee, trackChanges: false);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id },
                employeeToReturn);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id,
         [FromBody] EmployeeForUpdateDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForUpdateDto object is null");
            
            if (!ModelState.IsValid) 
                return UnprocessableEntity(ModelState);

           await  _service.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employee, compTrackChanges: false, 
            empTrackChanges: true);

            return NoContent();
        }
        
        [HttpPatch("{id:guid}")] 
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id, 
        [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");


            var (employeeToPatch, employeeEntity) = await _service.EmployeeService.GetEmployeeForPatchAsync(companyId, id, compTrackChanges: false,
            empTrackChanges: true);

            patchDoc.ApplyTo(employeeToPatch, ModelState);

            if(!TryValidateModel(employeeToPatch))
                return UnprocessableEntity(ModelState);

            await _service.EmployeeService.SaveChangesForPatchAsync(employeeToPatch, employeeEntity);
            
            return NoContent();

        }


        [HttpDelete("{employeeId:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid employeeId, Guid companyId)
        {
            await _service.EmployeeService.DeleteEmployeeAsync(employeeId, companyId, trackChanges:false);
            return NoContent();
        }
    }

    
}