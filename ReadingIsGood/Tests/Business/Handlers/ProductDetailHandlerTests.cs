
using Business.Handlers.ProductDetails.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.ProductDetails.Queries.GetProductDetailQuery;
using Entities.Concrete;
using static Business.Handlers.ProductDetails.Queries.GetProductDetailsQuery;
using static Business.Handlers.ProductDetails.Commands.CreateProductDetailCommand;
using Business.Handlers.ProductDetails.Commands;
using Business.Constants;
using static Business.Handlers.ProductDetails.Commands.UpdateProductDetailCommand;
using static Business.Handlers.ProductDetails.Commands.DeleteProductDetailCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
	[TestFixture]
	public class ProductDetailHandlerTests
	{
		Mock<IProductDetailRepository> _productDetailRepository;
		Mock<IMediator> _mediator;
		[SetUp]
		public void Setup()
		{
			_productDetailRepository = new Mock<IProductDetailRepository>();
			_mediator = new Mock<IMediator>();
		}

		[Test]
		public async Task ProductDetail_GetQuery_Success()
		{
			//Arrange
			var query = new GetProductDetailQuery();

			_productDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductDetail, bool>>>())).ReturnsAsync(new ProductDetail()
//propertyler buraya yazılacak
//{																		
//ProductDetailId = 1,
//ProductDetailName = "Test"
//}
);

			var handler = new GetProductDetailQueryHandler(_productDetailRepository.Object, _mediator.Object);

			//Act
			var x = await handler.Handle(query, new System.Threading.CancellationToken());

			//Asset
			x.Success.Should().BeTrue();
			//x.Data.ProductDetailId.Should().Be(1);

		}

		[Test]
		public async Task ProductDetail_GetQueries_Success()
		{
			//Arrange
			var query = new GetProductDetailsQuery();

			_productDetailRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<ProductDetail, bool>>>()))
						.ReturnsAsync(new List<ProductDetail> { new ProductDetail() { /*TODO:propertyler buraya yazılacak ProductDetailId = 1, ProductDetailName = "test"*/ } });

			var handler = new GetProductDetailsQueryHandler(_productDetailRepository.Object, _mediator.Object);

			//Act
			var x = await handler.Handle(query, new System.Threading.CancellationToken());

			//Asset
			x.Success.Should().BeTrue();
			((List<ProductDetail>)x.Data).Count.Should().BeGreaterThan(1);

		}

		[Test]
		public async Task ProductDetail_CreateCommand_Success()
		{
			ProductDetail rt = null;
			//Arrange
			var command = new CreateProductDetailCommand();
			//propertyler buraya yazılacak
			//command.ProductDetailName = "deneme";

			_productDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductDetail, bool>>>()))
						.ReturnsAsync(rt);

			_productDetailRepository.Setup(x => x.Add(It.IsAny<ProductDetail>())).Returns(new ProductDetail());

			var handler = new CreateProductDetailCommandHandler(_productDetailRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			_productDetailRepository.Verify(x => x.SaveChangesAsync());
			x.Success.Should().BeTrue();
			x.Message.Should().Be(Messages.Added);
		}

		[Test]
		public async Task ProductDetail_CreateCommand_NameAlreadyExist()
		{
			//Arrange
			var command = new CreateProductDetailCommand();
			//propertyler buraya yazılacak 
			//command.ProductDetailName = "test";

			_productDetailRepository.Setup(x => x.Query())
										   .Returns(new List<ProductDetail> { new ProductDetail() { /*TODO:propertyler buraya yazılacak ProductDetailId = 1, ProductDetailName = "test"*/ } }.AsQueryable());

			_productDetailRepository.Setup(x => x.Add(It.IsAny<ProductDetail>())).Returns(new ProductDetail());

			var handler = new CreateProductDetailCommandHandler(_productDetailRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			x.Success.Should().BeFalse();
			x.Message.Should().Be(Messages.NameAlreadyExist);
		}

		[Test]
		public async Task ProductDetail_UpdateCommand_Success()
		{
			//Arrange
			var command = new UpdateProductDetailCommand();
			//command.ProductDetailName = "test";

			_productDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductDetail, bool>>>()))
						.ReturnsAsync(new ProductDetail() { /*TODO:propertyler buraya yazılacak ProductDetailId = 1, ProductDetailName = "deneme"*/ });

			_productDetailRepository.Setup(x => x.Update(It.IsAny<ProductDetail>())).Returns(new ProductDetail());

			var handler = new UpdateProductDetailCommandHandler(_productDetailRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			_productDetailRepository.Verify(x => x.SaveChangesAsync());
			x.Success.Should().BeTrue();
			x.Message.Should().Be(Messages.Updated);
		}

		[Test]
		public async Task ProductDetail_DeleteCommand_Success()
		{
			//Arrange
			var command = new DeleteProductDetailCommand();

			_productDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ProductDetail, bool>>>()))
						.ReturnsAsync(new ProductDetail() { /*TODO:propertyler buraya yazılacak ProductDetailId = 1, ProductDetailName = "deneme"*/});

			_productDetailRepository.Setup(x => x.Delete(It.IsAny<ProductDetail>()));

			var handler = new DeleteProductDetailCommandHandler(_productDetailRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			_productDetailRepository.Verify(x => x.SaveChangesAsync());
			x.Success.Should().BeTrue();
			x.Message.Should().Be(Messages.Deleted);
		}
	}
}

