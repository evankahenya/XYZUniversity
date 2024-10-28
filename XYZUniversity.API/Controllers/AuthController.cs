using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XYZUniversity.API.Models.DTO;
using XYZUniversity.API.Repositories;

namespace XYZUniversity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User registered successfully. Please login.");
                    }
                }
            }

            // Return an error response if registration or role assignment fails
            return BadRequest(identityResult.Errors.Select(e => e.Description).ToList());
        }

        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user == null)
            {
                return BadRequest("Username or password is incorrect.");
            }

            var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!checkPasswordResult)
            {
                return BadRequest("Username or password is incorrect.");
            }

            // Get Roles of the User
            var roles = await userManager.GetRolesAsync(user);
            if (roles == null)
            {
                return BadRequest("User roles not found.");
            }

            // Create Token
            var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
            var response = new LoginResponseDto
            {
                JwtToken = jwtToken,
            };
            return Ok(response);
        }
    }
}
