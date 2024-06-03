using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAPPERCRUD
{
	[Authorize]

	[ApiController]
	[Route("api/v1/[controller]")]
	public class UserController : ControllerBase
	{
		private UserManager _userManager;
		public UserController()
		{
		}

		[HttpGet(Name = "GetUsers")]
		public IActionResult GetUsers()
		{
			try
			{
				this._userManager = new UserManager();
				var response = _userManager.GetUsers();
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("{id}", Name = "GetUserById")]
		public IActionResult GetUserDetails(int id)
		{
			try
			{
				this._userManager = new UserManager();
				var response = _userManager.GetUserDetails(id);
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[AllowAnonymous]
		[HttpPost(Name = "AddUser")]
		public IActionResult AddUser(User user)
		{
			try
			{
				this._userManager = new UserManager();
				var response = _userManager.AddUser(user);
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}