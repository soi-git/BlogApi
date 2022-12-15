using BlogApi.Helpers;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApi.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AccountController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;

        }

        [Route("api/login")]
        [HttpPost]
        public ActionResult<string> Post([FromBody] LoginModel user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            //var aaa = PasswordHashing.HashPassword(user.Password);
            UserModel? person = _userService.GetAll()?.FirstOrDefault(u => u.UserName == user.UserName && PasswordHashing.VerifyHashedPassword(u.PasswordHash, user.Password));
            if (person is null) return Unauthorized();
            person.Role = _roleService.Get(person.RoleId);
            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.Name, person.UserName),
                new Claim(ClaimTypes.Role, person.Role?.RoleName),
            };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token.ToString();
        }
    }
}
