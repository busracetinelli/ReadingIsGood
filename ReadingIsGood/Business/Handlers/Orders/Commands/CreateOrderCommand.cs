
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
using Business.Handlers.Orders.ValidationRules;

namespace Business.Handlers.Orders.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class CreateOrderCommand : IRequest<IResult>
	{

		public int UserId { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime UpdatedDate { get; set; }
		public System.DateTime DeletedDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }


		public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, IResult>
		{
			private readonly IOrderRepository _orderRepository;
			private readonly IMediator _mediator;
			public CreateOrderCommandHandler(IOrderRepository orderRepository, IMediator mediator)
			{
				_orderRepository = orderRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(CreateOrderValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
			{
				var isThereOrderRecord = _orderRepository.Query().Any(u => u.UserId == request.UserId);

				if (isThereOrderRecord == true)
					return new ErrorResult(Messages.NameAlreadyExist);

				var addedOrder = new Order
				{
					UserId = request.UserId,
					CreatedDate = request.CreatedDate,
					UpdatedDate = request.UpdatedDate,
					DeletedDate = request.DeletedDate,
					IsActive = request.IsActive,
					IsDeleted = request.IsDeleted,

				};

				_orderRepository.Add(addedOrder);
				await _orderRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Added);
			}
		}
	}
}