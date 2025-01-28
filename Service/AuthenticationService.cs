using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(
            ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IConfiguration configuration
        )
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            
        }
        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

            return result;

        }
    }
}


// using System;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Identity;
// using Service.Contracts;
// using Shared.DataTransferObjects;
// using Entities.Models;

// namespace Service
// {
//     public class AuthenticationService : IAuthenticationService
//     {
//         private readonly UserManager<User> _userManager;
//         private readonly ILogger<AuthenticationService> _logger;

//         public AuthenticationService(
//             UserManager<User> userManager,
//             ILogger<AuthenticationService> logger)
//         {
//             _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
//             _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//         }

//         public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
//         {
//             var user = new User
//             {
//                 FirstName = userForRegistration.FirstName,
//                 LastName = userForRegistration.LastName,
//                 UserName = userForRegistration.UserName,
//                 Email = userForRegistration.Email,
//                 PhoneNumber = userForRegistration.PhoneNumber
//             };

//             _logger.LogInformation($"Registering user: {user.UserName}");

//             try
//             {
//                 var result = await _userManager.CreateAsync(user, userForRegistration.Password);

//                 if (result.Succeeded)
//                     _logger.LogInformation($"Successfully registered user: {user.UserName}");
//                 else
//                     _logger.LogWarning($"Failed to register user: {user.UserName}. Errors: {string.Join(", ", result.Errors)}");

//                 return result;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, $"Error occurred while registering user: {user.UserName}");
//                 throw;
//             }
//         }
//     }
// }