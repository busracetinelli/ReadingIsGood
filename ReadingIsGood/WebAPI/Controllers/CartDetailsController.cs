
using Business.Handlers.CartDetails.Commands;
using Business.Handlers.CartDetails.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
	/// <summary>
	/// CartDetails If controller methods will not be Authorize, [AllowAnonymous] is used.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CartDetailsController : BaseApiController
	{
		///<summary>
		///List CartDetails
		///</summary>
		///<remarks>CartDetails</remarks>
		///<return>List CartDetails</return>
		///<response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CartDetail>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpGet("getall")]
		public async Task<IActionResult> GetList()
		{
			var result = await Mediator.Send(new GetCartDetailsQuery());
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		///<summary>
		///It brings the details according to its id.
		///</summary>
		///<remarks>CartDetails</remarks>
		///<return>CartDetails List</return>
		///<response code="200"></response>  
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartDetail))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpGet("getbyid")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await Mediator.Send(new GetCartDetailQuery { Id = id });
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Add CartDetail.
		/// </summary>
		/// <param name="createCartDetail"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateCartDetailCommand createCartDetail)
		{
			var result = await Mediator.Send(createCartDetail);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Update CartDetail.
		/// </summary>
		/// <param name="updateCartDetail"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateCartDetailCommand updateCartDetail)
		{
			var result = await Mediator.Send(updateCartDetail);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Delete CartDetail.
		/// </summary>
		/// <param name="deleteCartDetail"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteCartDetailCommand deleteCartDetail)
		{
			var result = await Mediator.Send(deleteCartDetail);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}
	}
}
