using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAPPERCRUD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAPPERCRUD
{
	[Authorize]
	[ApiController]
	[Route("api/v1/[controller]")]
	public class CustomerController : ControllerBase
	{
		private  CustomerManager _customerManager;
		public CustomerController()
		{


		}
		[HttpGet]
		public IActionResult GetCustomers()
		{
			try
			{
				this._customerManager = new CustomerManager();
				var response =  _customerManager.GetCustomers();
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{id}", Name = "CustomerById")]
		public IActionResult GetCustomerDetails(int id)
		{
			try
			{
				var response =  _customerManager.GetCustomerDetails(id);
				if (response == null) return NotFound();
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public IActionResult AddCustomer(Customer customer)
		{
			try
			{
				var response =  _customerManager.AddCustomer(customer);
				return Ok(response);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}
		[HttpPut("{id}")]
		public IActionResult UpdateCustomer(int id, Customer customer)
		{
			try
			{
				// var isCustomerExists =  _customerManager.GetCustomerDetails(id);
				// if (isCustomerExists == null) return NotFound();
				 _customerManager.UpdateCustomer(id, customer);
				return NoContent();
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCustomer(int id)
		{
			try
			{
				var isCustomerExists =  _customerManager.GetCustomerDetails(id);
				if (isCustomerExists == null) return NotFound();
				 _customerManager.DeleteCustomer(id);
				return NoContent();
			}
			catch (System.Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}