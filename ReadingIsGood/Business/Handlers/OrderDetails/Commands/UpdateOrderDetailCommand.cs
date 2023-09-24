
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.OrderDetails.ValidationRules;


namespace Business.Handlers.OrderDetails.Commands
{


	public class UpdateOrderDetailCommand : IRequest<IResult>
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime UpdatedDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand, IResult>
		{
			private readonly IOrderDetailRepository _orderDetailRepository;
			private readonly IMediator _mediator;

			public UpdateOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository, IMediator mediator)
			{
				_orderDetailRepository = orderDetailRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(UpdateOrderDetailValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
			{
				var isThereOrderDetailRecord = await _orderDetailRepository.GetAsync(u => u.Id == request.Id);


				isThereOrderDetailRecord.OrderId = request.OrderId;
				isThereOrderDetailRecord.ProductName = request.ProductName;
				isThereOrderDetailRecord.Quantity = request.Quantity;
				isThereOrderDetailRecord.Price = request.Price;
				isThereOrderDetailRecord.CreatedDate = request.CreatedDate;
				isThereOrderDetailRecord.UpdatedDate = request.UpdatedDate;
				isThereOrderDetailRecord.IsActive = request.IsActive;
				isThereOrderDetailRecord.IsDeleted = request.IsDeleted;


				_orderDetailRepository.Update(isThereOrderDetailRecord);
				await _orderDetailRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}

