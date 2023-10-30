using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_API_JWT.Models;
using Web_API_JWT.Services;
using Web_API_JWT.Validators.Movies;

namespace Web_API_JWT.Controllers;

[Route("api/v1/")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly AuthService _authService;
    private readonly RoleService _roleService;
    private readonly TokenService _tokenService;
    public AuthController(AuthService authService, RoleService roleService, TokenService tokenService)
    {
        _authService = authService;
        _roleService = roleService;
        _tokenService = tokenService;
    }

    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRole([FromBody] string roleName)
    {
        try
        {
            var created = await _roleService.CreateRoleAsync(roleName);

            if (!created)
            {
                return BadRequest(new { Message = "Ocorreu um erro ao criar a função." });

            }
            return Ok(new { Message = "Função criada com sucesso." });
        } catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Ocorreu um erro durante o login.", Error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginModel user)
    {
        try
        {
            var identityUser = await _authService.FindByEmailAsync(user.Email);

            if (identityUser != null && await _authService.CheckPasswordAsync(identityUser, user.Password))
            {
                var roles = await _authService.GetRolesAsync(identityUser);
                var token = _tokenService.GenerateJwtToken(identityUser, roles);
                return Ok(new { token });
            }

            return Unauthorized(new { Message = "Credenciais inválidas." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Ocorreu um erro durante o login.", Error = ex.Message });
         }
    
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterModel user)
    {
        try
        {
            var validator = new UserRegisterValidator();
            var validationResult = validator.Validate(user);
            if (!validationResult.IsValid)
            {
                
                return BadRequest(new { Message = "Os campos não foram corretamente preenchidos", Errors = validationResult.Errors });
            }

            var newIdentityUser = new IdentityUser { UserName = user.UserName, Email = user.Email };
            var result = await _authService.CreateAsync(newIdentityUser, user.Password);

            if (!result.Succeeded)
            {
               
                var errorMessages = result.Errors.Select(error => error.Description);
                return BadRequest(new { Message = "Algo deu errado na criação do usuário", Errors = errorMessages });
               
               
            }
            await _authService.AddToRoleAsync(newIdentityUser, user.Role);
            var token = _tokenService.GenerateJwtToken(newIdentityUser, new List<string> { user.Role });

           
            return Ok(new { token });
        }
        catch (Exception ex)
        {
           
            return StatusCode(500, new { Message = "Ocorreu um erro durante o registro.", Error = ex.Message });
        }
    }

}