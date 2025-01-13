using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/employees")]
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

        [HttpGet("{employeeId:guid}")]
        public IActionResult GetEmployee(Guid employeeId, Guid companyId)
        {
            var employee = _service.EmployeeService.GetEmployee(employeeId, companyId, trackChanges:false);
            return Ok(employee);
        }
    }
}