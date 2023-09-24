
using Business.Handlers.ProductDetails.Commands;
using Business.Handlers.ProductDetails.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
	/// <summary>
	/// ProductDetails If controller methods will not be Authorize, [AllowAnonymous] is used.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class ProductDetailsController : BaseApiController
	{
		///<summary>
		///List ProductDetails
		///</summary>
		///<remarks>ProductDetails</remarks>
		///<return>List ProductDetails</return>
		///<response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDetail>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpGet("getall")]
		public async Task<IActionResult> GetList()
		{
			var result = await Mediator.Send(new GetProductDetailsQuery());
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		///<summary>
		///It brings the details according to its id.
		///</summary>
		///<remarks>ProductDetails</remarks>
		///<return>ProductDetails List</return>
		///<response code="200"></response>  
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetail))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpGet("getbyid")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await Mediator.Send(new GetProductDetailQuery { Id = id });
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Add ProductDetail.
		/// </summary>
		/// <param name="createProductDetail"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateProductDetailCommand createProductDetail)
		{
			var result = await Mediator.Send(createProductDetail);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Update ProductDetail.
		/// </summary>
		/// <param name="updateProductDetail"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateProductDetailCommand updateProductDetail)
		{
			var result = await Mediator.Send(updateProductDetail);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Delete ProductDetail.
		/// </summary>
		/// <param name="deleteProductDetail"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteProductDetailCommand deleteProductDetail)
		{
			var result = await Mediator.Send(deleteProductDetail);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}
	}
}
