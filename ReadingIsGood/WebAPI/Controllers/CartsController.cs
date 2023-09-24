
using Business.Handlers.Carts.Commands;
using Business.Handlers.Carts.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
	/// <summary>
	/// Carts If controller methods will not be Authorize, [AllowAnonymous] is used.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CartsController : BaseApiController
	{
		///<summary>
		///List Carts
		///</summary>
		///<remarks>Carts</remarks>
		///<return>List Carts</return>
		///<response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Cart>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpGet("getall")]
		public async Task<IActionResult> GetList()
		{
			var result = await Mediator.Send(new GetCartsQuery());
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		///<summary>
		///It brings the details according to its id.
		///</summary>
		///<remarks>Carts</remarks>
		///<return>Carts List</return>
		///<response code="200"></response>  
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cart))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpGet("getbyid")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await Mediator.Send(new GetCartQuery { Id = id });
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Add Cart.
		/// </summary>
		/// <param name="createCart"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateCartCommand createCart)
		{
			var result = await Mediator.Send(createCart);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Update Cart.
		/// </summary>
		/// <param name="updateCart"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateCartCommand updateCart)
		{
			var result = await Mediator.Send(updateCart);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Delete Cart.
		/// </summary>
		/// <param name="deleteCart"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteCartCommand deleteCart)
		{
			var result = await Mediator.Send(deleteCart);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}
	}
}
