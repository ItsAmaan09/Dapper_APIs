
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAPPERCRUD
{
	[Authorize]

	[ApiController]
	[Route("api/v1/[controller]")]
	public class UserPasswordController : ControllerBase
	{
		private UserPasswordManager _userPasswordManager;
		public UserPasswordController()
		{
		}

		[HttpGet("{id}")]
		public IActionResult GetUserPassword(int id)
		{
			try
			{
				this._userPasswordManager = new UserPasswordManager();
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
				this._userPasswordManager = new UserPasswordManager();

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