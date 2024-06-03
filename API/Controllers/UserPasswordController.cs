
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAPPERCRUD
{
	[Authorize]

	[ApiController]
	[Route("api/v1/[controller]")]
	public class UserPasswordController : ControllerBase
	{
		private readonly UserPasswordManager _userPasswordManager;
		public UserPasswordController(UserPasswordManager userPasswordManager)
		{
			_userPasswordManager = userPasswordManager;
		}

		[HttpGet("{id}")]
		public IActionResult GetUserPassword(int id)
		{
			try
			{
				var response = _userPasswordManager.GetUserPassword(id);
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost]
		public IActionResult CreatePassword(UserPassword userPassword)
		{
			try
			{
				var response = _userPasswordManager.CreatePassword(userPassword);
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}