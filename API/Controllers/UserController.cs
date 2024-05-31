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
		// [HttpGet]
		// public async Task<IActionResult> GetUsers()
		// {
		// 	try
		// 	{
		// 		var response = await _userManager.GetUsers();
		// 		return Ok(response);
		// 	}
		// 	catch (System.Exception ex)
		// 	{
		// 		return BadRequest(ex.Message);
		// 	}
		// }
		// [HttpGet("{id}")]
		// public async Task<IActionResult> GetUserDetails(int id)
		// {
		// 	try
		// 	{
		// 		var response = await _userManager.GetUserDetails(id);
		// 		return Ok(response);
		// 	}
		// 	catch (System.Exception ex)
		// 	{
		// 		return BadRequest(ex.Message);
		// 	}
		// }
		// [HttpPost]
		// public async Task<IActionResult> AddUser(User user)
		// {
		// 	try
		// 	{
		// 		var response = await _userManager.AddUser(user);
		// 		return Ok(response);
		// 	}
		// 	catch (System.Exception ex)
		// 	{
		// 		return BadRequest(ex);
		// 		throw;
		// 	}
		// }
	}
}