
using Business.Handlers.Carts.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Carts.Queries.GetCartQuery;
using Entities.Concrete;
using static Business.Handlers.Carts.Queries.GetCartsQuery;
using static Business.Handlers.Carts.Commands.CreateCartCommand;
using Business.Handlers.Carts.Commands;
using Business.Constants;
using static Business.Handlers.Carts.Commands.UpdateCartCommand;
using static Business.Handlers.Carts.Commands.DeleteCartCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
	[TestFixture]
	public class CartHandlerTests
	{
		Mock<ICartRepository> _cartRepository;
		Mock<IMediator> _mediator;
		[SetUp]
		public void Setup()
		{
			_cartRepository = new Mock<ICartRepository>();
			_mediator = new Mock<IMediator>();
		}

		[Test]
		public async Task Cart_GetQuery_Success()
		{
			//Arrange
			var query = new GetCartQuery();

			_cartRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cart, bool>>>())).ReturnsAsync(new Cart()
//propertyler buraya yazılacak
//{																		
//CartId = 1,
//CartName = "Test"
//}
);

			var handler = new GetCartQueryHandler(_cartRepository.Object, _mediator.Object);

			//Act
			var x = await handler.Handle(query, new System.Threading.CancellationToken());

			//Asset
			x.Success.Should().BeTrue();
			//x.Data.CartId.Should().Be(1);

		}

		[Test]
		public async Task Cart_GetQueries_Success()
		{
			//Arrange
			var query = new GetCartsQuery();

			_cartRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Cart, bool>>>()))
						.ReturnsAsync(new List<Cart> { new Cart() { /*TODO:propertyler buraya yazılacak CartId = 1, CartName = "test"*/ } });

			var handler = new GetCartsQueryHandler(_cartRepository.Object, _mediator.Object);

			//Act
			var x = await handler.Handle(query, new System.Threading.CancellationToken());

			//Asset
			x.Success.Should().BeTrue();
			((List<Cart>)x.Data).Count.Should().BeGreaterThan(1);

		}

		[Test]
		public async Task Cart_CreateCommand_Success()
		{
			Cart rt = null;
			//Arrange
			var command = new CreateCartCommand();
			//propertyler buraya yazılacak
			//command.CartName = "deneme";

			_cartRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cart, bool>>>()))
						.ReturnsAsync(rt);

			_cartRepository.Setup(x => x.Add(It.IsAny<Cart>())).Returns(new Cart());

			var handler = new CreateCartCommandHandler(_cartRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			_cartRepository.Verify(x => x.SaveChangesAsync());
			x.Success.Should().BeTrue();
			x.Message.Should().Be(Messages.Added);
		}

		[Test]
		public async Task Cart_CreateCommand_NameAlreadyExist()
		{
			//Arrange
			var command = new CreateCartCommand();
			//propertyler buraya yazılacak 
			//command.CartName = "test";

			_cartRepository.Setup(x => x.Query())
										   .Returns(new List<Cart> { new Cart() { /*TODO:propertyler buraya yazılacak CartId = 1, CartName = "test"*/ } }.AsQueryable());

			_cartRepository.Setup(x => x.Add(It.IsAny<Cart>())).Returns(new Cart());

			var handler = new CreateCartCommandHandler(_cartRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			x.Success.Should().BeFalse();
			x.Message.Should().Be(Messages.NameAlreadyExist);
		}

		[Test]
		public async Task Cart_UpdateCommand_Success()
		{
			//Arrange
			var command = new UpdateCartCommand();
			//command.CartName = "test";

			_cartRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cart, bool>>>()))
						.ReturnsAsync(new Cart() { /*TODO:propertyler buraya yazılacak CartId = 1, CartName = "deneme"*/ });

			_cartRepository.Setup(x => x.Update(It.IsAny<Cart>())).Returns(new Cart());

			var handler = new UpdateCartCommandHandler(_cartRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			_cartRepository.Verify(x => x.SaveChangesAsync());
			x.Success.Should().BeTrue();
			x.Message.Should().Be(Messages.Updated);
		}

		[Test]
		public async Task Cart_DeleteCommand_Success()
		{
			//Arrange
			var command = new DeleteCartCommand();

			_cartRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Cart, bool>>>()))
						.ReturnsAsync(new Cart() { /*TODO:propertyler buraya yazılacak CartId = 1, CartName = "deneme"*/});

			_cartRepository.Setup(x => x.Delete(It.IsAny<Cart>()));

			var handler = new DeleteCartCommandHandler(_cartRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			_cartRepository.Verify(x => x.SaveChangesAsync());
			x.Success.Should().BeTrue();
			x.Message.Should().Be(Messages.Deleted);
		}
	}
}

