using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyEmployees.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthenticationController(IServiceManager service){
            _service = service;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody]
        UserForRegistrationDto userForRegistrationDto)
        {
            // if (userForRegistrationDto is null)
            //     return BadRequest("userForRegistrationDto is null");

            var result = await _service.AuthenticationService.RegisterUser(userForRegistrationDto);
            
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }
    }
}