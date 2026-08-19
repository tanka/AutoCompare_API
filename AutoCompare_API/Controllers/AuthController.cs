using AutoCompare_API.Data;
using AutoCompare_API.Models;
using AutoCompare_API.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

/**
* @author Tanka N Sharma
* api/Auth/login, api/Auth/register
*/


namespace AutoCompare_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ApiResponse _response;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string secretKey;


        public AuthController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret") ?? string.Empty;
            _response = new ApiResponse();

        }

        // Register end point
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            if (ModelState.IsValid)
            {
                // Registration logic here
                ApplicationUser newUser = new()
                {
                    Email = registerRequestDTO.email,
                    UserName = registerRequestDTO.email,
                    Name = registerRequestDTO.name,
                    NormalizedEmail = registerRequestDTO.email.ToUpper()
                };
                var result = await _userManager.CreateAsync(newUser, registerRequestDTO.password);
                if (result.Succeeded)
                {
                    // This will be invoked only once, when there is no roles.
                    if (!_roleManager.RoleExistsAsync(
                        SD.Role_Admin).GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
                    }

                    if (registerRequestDTO.role.Equals(SD.Role_Admin, StringComparison.CurrentCultureIgnoreCase))
                    {
                        await _userManager.AddToRoleAsync(newUser, SD.Role_Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser, SD.Role_Customer);

                    }

                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    foreach (var error in result.Errors)
                    {
                        _response.ErrorMessages.Add(error.Description);
                    }
                    return BadRequest(_response);
                }
            }
            else
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                foreach (var error in ModelState.Values)
                {
                    foreach (var subError in error.Errors)
                    {
                        _response.ErrorMessages.Add(subError.ErrorMessage);
                    }
                }
                return BadRequest(_response);
            }
        }

        // Login End point 
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            if (ModelState.IsValid)
            {

                var userFromDB = await _userManager.FindByEmailAsync(loginRequestDTO.email);
                if (userFromDB == null)
                {  // no user --> return errors
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Invalid email or password");
                    return BadRequest(_response);
                }
                else
                {
                    var isPasswordValid = await _userManager.CheckPasswordAsync(userFromDB, loginRequestDTO.password);
                    if (isPasswordValid)
                    { // if correct user credentials

                        // Generate JWT Token
                        JwtSecurityTokenHandler tokenHandler = new();
                        byte[] key = System.Text.Encoding.ASCII.GetBytes(secretKey);

                        SecurityTokenDescriptor tokenDescriptor = new()
                        {
                            Subject = new ClaimsIdentity(
                                [
                                new ("fullname", userFromDB.Name),
                                new ("id", userFromDB.Id),
                                new (ClaimTypes.Email, userFromDB.Email!.ToString()),// ClaimTypes is integrated in .Net Identity.
                                new (ClaimTypes.Role, _userManager.GetRolesAsync(userFromDB).GetAwaiter().GetResult().FirstOrDefault()!)
                                ]),
                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new(
                                new SymmetricSecurityKey(key),
                                SecurityAlgorithms.HmacSha256Signature
                                )
                        };

                        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

                        LoginResponseDTO loginResponse = new LoginResponseDTO();
                        loginRequestDTO.email = userFromDB.Email!;
                        loginResponse.token = tokenHandler.WriteToken(token);
                        loginResponse.role = _userManager.GetRolesAsync(userFromDB).GetAwaiter().GetResult().FirstOrDefault()!;

                        _response.StatusCode = HttpStatusCode.OK;
                        _response.IsSuccess = true;
                        _response.Result = loginResponse;
                        return Ok(_response);
                    }
                    else
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("Invalid email or password");
                        return BadRequest(_response);
                    }
                }
            }
            else
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                foreach (var error in ModelState.Values)
                {
                    foreach (var subError in error.Errors)
                    {
                        _response.ErrorMessages.Add(subError.ErrorMessage);
                    }
                }
                return BadRequest(_response);
            }
        }
    }
}
