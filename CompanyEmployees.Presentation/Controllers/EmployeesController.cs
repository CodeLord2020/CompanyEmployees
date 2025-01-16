using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _service.EmployeeService.GetEmployees(companyId, trackChanges:false);
            return Ok(employees);
        }

        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
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
        public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid employeeId, [FromBody] 
        EmployeeForUpdateDto employee)
        {
            if (employee is null){
                return BadRequest("EmployeeForUpdateDto object is null");
            }

            _service.EmployeeService.UpdateEmployeeForCompany(companyId, employeeId, employee, compTrackChanges: false, 
            empTrackChanges: true);

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