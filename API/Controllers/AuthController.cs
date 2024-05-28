using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class AuthController : ControllerBase
	{
		[HttpPost("Login")]
		public IActionResult Login(UserLogin model)
		{
			// Validate user credentials (this is just a demo, so we will skip actual validation)
			if (model.Username != "test" || model.Password != "password")
				return Unauthorized();

			// Generate JWT token
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("this_is_a_very_secure_key_that_is_at_least_32_bytes_long!");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, model.Username)
				}),
				Expires = DateTime.UtcNow.AddMinutes(360),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
				Issuer="WHY NOT",
				Audience="WHY NOT"
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			return Ok(tokenString);
		}
	}

	public class UserLogin
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
