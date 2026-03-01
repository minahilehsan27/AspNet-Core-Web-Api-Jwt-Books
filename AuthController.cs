using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HomeWork2_JWTToken_ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration =  configuration;
        }
        [HttpPost("login")]
        public IActionResult Login(string username , string password)
        {
            if(username.Equals("abc") && password.Equals("1234"))
            {
                var jwtSettings = _configuration.GetSection("Jwt");
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub , username),
                    new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()) ,
                    new Claim(ClaimTypes.Role,"Admin")
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
                var credentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                     issuer: jwtSettings["Issuer"],
                     audience: jwtSettings["Audience"],
                     claims: claims,
                     expires: DateTime.UtcNow.AddMinutes(
                         Convert.ToDouble(jwtSettings["ExpiryMinutes"])
                     ),
                     signingCredentials: credentials
                 );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiresIn = jwtSettings["ExpiryMinutes"],
                    tokenType = "Bearer"
                });
            }
            else
            {
                return Unauthorized(new { error = "Invalid username or password" });
            }
        }
    }
}
