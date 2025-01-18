using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

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
        public IActionResult GetAllEmployees()
        {
            var employees = _service.EmployeeService.GetAllEmployees(trackChanges:false);
            return Ok(employees);
        }

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _service.EmployeeService.GetEmployees(companyId, trackChanges:false);
            return Ok(employees);
        }

        [HttpGet("{employeeId:guid}", Name = "GetEmployeeForCompany")]
        public IActionResult GetEmployee(Guid employeeId, Guid companyId)
        {
            var employee = _service.EmployeeService.GetEmployee(employeeId, companyId, trackChanges:false);
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDto object is null");

            var employeeToReturn = _service.EmployeeService.CreateEmployeeForCompany(companyId, employee, trackChanges: false);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id },
                employeeToReturn);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid id,
         [FromBody] EmployeeForUpdateDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForUpdateDto object is null");
            

            if (!ModelState.IsValid) 
                return UnprocessableEntity(ModelState);

            _service.EmployeeService.UpdateEmployeeForCompany(companyId, id, employee, compTrackChanges: false, 
            empTrackChanges: true);

            return NoContent();
        }
        
        [HttpPatch("{id:guid}")] 
        public IActionResult PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id, 
        [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");


            var (employeeToPatch, employeeEntity) = _service.EmployeeService.GetEmployeeForPatch(companyId, id, compTrackChanges: false,
            empTrackChanges: true);

            patchDoc.ApplyTo(employeeToPatch);

            _service.EmployeeService.SaveChangesForPatch(employeeToPatch, employeeEntity);
            
            return NoContent();

        }


        [HttpDelete("{employeeId:guid}")]
        public IActionResult DeleteEmployee(Guid employeeId, Guid companyId)
        {
            _service.EmployeeService.DeleteEmployee(employeeId, companyId, trackChanges:false);
            return NoContent();
        }
    }

    
}