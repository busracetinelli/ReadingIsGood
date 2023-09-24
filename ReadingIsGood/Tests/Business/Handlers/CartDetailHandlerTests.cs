
using Business.Handlers.CartDetails.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.CartDetails.Queries.GetCartDetailQuery;
using Entities.Concrete;
using static Business.Handlers.CartDetails.Queries.GetCartDetailsQuery;
using static Business.Handlers.CartDetails.Commands.CreateCartDetailCommand;
using Business.Handlers.CartDetails.Commands;
using Business.Constants;
using static Business.Handlers.CartDetails.Commands.UpdateCartDetailCommand;
using static Business.Handlers.CartDetails.Commands.DeleteCartDetailCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
	[TestFixture]
	public class CartDetailHandlerTests
	{
		Mock<ICartDetailRepository> _cartDetailRepository;
		Mock<IMediator> _mediator;
		[SetUp]
		public void Setup()
		{
			_cartDetailRepository = new Mock<ICartDetailRepository>();
			_mediator = new Mock<IMediator>();
		}

		[Test]
		public async Task CartDetail_GetQuery_Success()
		{
			//Arrange
			var query = new GetCartDetailQuery();

			_cartDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CartDetail, bool>>>())).ReturnsAsync(new CartDetail()
//propertyler buraya yazılacak
//{																		
//CartDetailId = 1,
//CartDetailName = "Test"
//}
);

			var handler = new GetCartDetailQueryHandler(_cartDetailRepository.Object, _mediator.Object);

			//Act
			var x = await handler.Handle(query, new System.Threading.CancellationToken());

			//Asset
			x.Success.Should().BeTrue();
			//x.Data.CartDetailId.Should().Be(1);

		}

		[Test]
		public async Task CartDetail_GetQueries_Success()
		{
			//Arrange
			var query = new GetCartDetailsQuery();

			_cartDetailRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<CartDetail, bool>>>()))
						.ReturnsAsync(new List<CartDetail> { new CartDetail() { /*TODO:propertyler buraya yazılacak CartDetailId = 1, CartDetailName = "test"*/ } });

			var handler = new GetCartDetailsQueryHandler(_cartDetailRepository.Object, _mediator.Object);

			//Act
			var x = await handler.Handle(query, new System.Threading.CancellationToken());

			//Asset
			x.Success.Should().BeTrue();
			((List<CartDetail>)x.Data).Count.Should().BeGreaterThan(1);

		}

		[Test]
		public async Task CartDetail_CreateCommand_Success()
		{
			CartDetail rt = null;
			//Arrange
			var command = new CreateCartDetailCommand();
			//propertyler buraya yazılacak
			//command.CartDetailName = "deneme";

			_cartDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CartDetail, bool>>>()))
						.ReturnsAsync(rt);

			_cartDetailRepository.Setup(x => x.Add(It.IsAny<CartDetail>())).Returns(new CartDetail());

			var handler = new CreateCartDetailCommandHandler(_cartDetailRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			_cartDetailRepository.Verify(x => x.SaveChangesAsync());
			x.Success.Should().BeTrue();
			x.Message.Should().Be(Messages.Added);
		}

		[Test]
		public async Task CartDetail_CreateCommand_NameAlreadyExist()
		{
			//Arrange
			var command = new CreateCartDetailCommand();
			//propertyler buraya yazılacak 
			//command.CartDetailName = "test";

			_cartDetailRepository.Setup(x => x.Query())
										   .Returns(new List<CartDetail> { new CartDetail() { /*TODO:propertyler buraya yazılacak CartDetailId = 1, CartDetailName = "test"*/ } }.AsQueryable());

			_cartDetailRepository.Setup(x => x.Add(It.IsAny<CartDetail>())).Returns(new CartDetail());

			var handler = new CreateCartDetailCommandHandler(_cartDetailRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			x.Success.Should().BeFalse();
			x.Message.Should().Be(Messages.NameAlreadyExist);
		}

		[Test]
		public async Task CartDetail_UpdateCommand_Success()
		{
			//Arrange
			var command = new UpdateCartDetailCommand();
			//command.CartDetailName = "test";

			_cartDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CartDetail, bool>>>()))
						.ReturnsAsync(new CartDetail() { /*TODO:propertyler buraya yazılacak CartDetailId = 1, CartDetailName = "deneme"*/ });

			_cartDetailRepository.Setup(x => x.Update(It.IsAny<CartDetail>())).Returns(new CartDetail());

			var handler = new UpdateCartDetailCommandHandler(_cartDetailRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			_cartDetailRepository.Verify(x => x.SaveChangesAsync());
			x.Success.Should().BeTrue();
			x.Message.Should().Be(Messages.Updated);
		}

		[Test]
		public async Task CartDetail_DeleteCommand_Success()
		{
			//Arrange
			var command = new DeleteCartDetailCommand();

			_cartDetailRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<CartDetail, bool>>>()))
						.ReturnsAsync(new CartDetail() { /*TODO:propertyler buraya yazılacak CartDetailId = 1, CartDetailName = "deneme"*/});

			_cartDetailRepository.Setup(x => x.Delete(It.IsAny<CartDetail>()));

			var handler = new DeleteCartDetailCommandHandler(_cartDetailRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			_cartDetailRepository.Verify(x => x.SaveChangesAsync());
			x.Success.Should().BeTrue();
			x.Message.Should().Be(Messages.Deleted);
		}
	}
}

