using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
    }
}



// using System;
// using System.Threading.Tasks;
// using System.Security.Claims;

// namespace Service
// {
//     public interface IAuthenticationService
//     {
//         Task<bool> ValidateCredentialsAsync(string username, string password);
//         Task<string> GenerateTokenAsync(string username);
//         Task<ClaimsPrincipal> ValidateTokenAsync(string token);
//         Task<bool> RevokeTokenAsync(string token);
//         Task<bool> ChangePasswordAsync(string username, string currentPassword, string newPassword);
//     }

//     public class AuthenticationService : IAuthenticationService
//     {
//         private readonly IUserRepository _userRepository;
//         private readonly ITokenService _tokenService;
//         private readonly IPasswordHasher _passwordHasher;

//         public AuthenticationService(
//             IUserRepository userRepository,
//             ITokenService tokenService,
//             IPasswordHasher passwordHasher)
//         {
//             _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
//             _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
//             _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
//         }

//         public async Task<bool> ValidateCredentialsAsync(string username, string password)
//         {
//             var user = await _userRepository.GetByUsernameAsync(username);
//             if (user == null) return false;

//             return await _passwordHasher.VerifyPasswordAsync(password, user.PasswordHash);
//         }

//         public async Task<string> GenerateTokenAsync(string username)
//         {
//             var user = await _userRepository.GetByUsernameAsync(username);
//             if (user == null) throw new ArgumentException("User not found", nameof(username));

//             return await _tokenService.GenerateTokenAsync(user);
//         }

//         public async Task<ClaimsPrincipal> ValidateTokenAsync(string token)
//         {
//             if (string.IsNullOrEmpty(token))
//                 throw new ArgumentNullException(nameof(token));

//             return await _tokenService.ValidateTokenAsync(token);
//         }

//         public async Task<bool> RevokeTokenAsync(string token)
//         {
//             if (string.IsNullOrEmpty(token))
//                 throw new ArgumentNullException(nameof(token));

//             return await _tokenService.RevokeTokenAsync(token);
//         }

//         public async Task<bool> ChangePasswordAsync(string username, string currentPassword, string newPassword)
//         {
//             if (!await ValidateCredentialsAsync(username, currentPassword))
//                 return false;

//             var user = await _userRepository.GetByUsernameAsync(username);
//             var newPasswordHash = await _passwordHasher.HashPasswordAsync(newPassword);
            
//             user.PasswordHash = newPasswordHash;
//             await _userRepository.UpdateAsync(user);
            
//             return true;
//         }
//     }
// }