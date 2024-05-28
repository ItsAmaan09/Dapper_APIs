using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DAPPERCRUD
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly UserManager _userManager;
		public UserController(UserManager userManager)
		{
			_userManager = userManager;
		}
		[HttpGet]
		public IActionResult GetUsers()
		{
			try
			{
				var response = _userManager.GetUsers();
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}