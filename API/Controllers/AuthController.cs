using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DAPPERCRUD
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly UserManager _userManager;
		public AuthController(UserManager userManager)
		{
			_userManager = userManager;
		}
		[HttpPost("Login")]
		public async Task<IActionResult> Login(UserLogin model)
		{
			try
			{
				await _userManager.IsUserVerified(model);
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes("this_is_a_very_secure_key_that_is_at_least_32_bytes_long!");
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
					new Claim(ClaimTypes.Name, model.Username)
					}),
					Expires = DateTime.UtcNow.AddHours(1),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
					Issuer = "WHY NOT",
					Audience = "WHY NOT"
				};
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
