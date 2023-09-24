
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.OrderDetails.ValidationRules;

namespace Business.Handlers.OrderDetails.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class CreateOrderDetailCommand : IRequest<IResult>
	{

		public int OrderId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime UpdatedDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }


		public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, IResult>
		{
			private readonly IOrderDetailRepository _orderDetailRepository;
			private readonly IMediator _mediator;
			public CreateOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository, IMediator mediator)
			{
				_orderDetailRepository = orderDetailRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(CreateOrderDetailValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
			{
				var isThereOrderDetailRecord = _orderDetailRepository.Query().Any(u => u.OrderId == request.OrderId);

				if (isThereOrderDetailRecord == true)
					return new ErrorResult(Messages.NameAlreadyExist);

				var addedOrderDetail = new OrderDetail
				{
					OrderId = request.OrderId,
					ProductName = request.ProductName,
					Quantity = request.Quantity,
					Price = request.Price,
					CreatedDate = request.CreatedDate,
					UpdatedDate = request.UpdatedDate,
					IsActive = request.IsActive,
					IsDeleted = request.IsDeleted,

				};

				_orderDetailRepository.Add(addedOrderDetail);
				await _orderDetailRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Added);
			}
		}
	}
}