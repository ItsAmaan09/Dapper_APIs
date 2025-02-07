using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DAPPERCRUD
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class AuthController : ControllerBase
	{
		IConfiguration _configuration;
		private  UserManager _userManager;
		public AuthController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[AllowAnonymous]
		[HttpPost("Login")]
		public IActionResult Login(UserLogin userLogin)
		{
			try
			{
				this._userManager = new UserManager();
				 _userManager.IsUserVerified(userLogin);

				var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
						new Claim(ClaimTypes.Name, userLogin.Username)
					}),
					Expires = DateTime.UtcNow.AddHours(1),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
					Issuer = _configuration["Jwt:Issuer"],
					Audience = _configuration["Jwt:Audience"]
				};

				var tokenHandler = new JwtSecurityTokenHandler();

				var token = tokenHandler.CreateToken(tokenDescriptor);

				var tokenString = tokenHandler.WriteToken(token);

				return Ok(tokenString);
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
	}
}
