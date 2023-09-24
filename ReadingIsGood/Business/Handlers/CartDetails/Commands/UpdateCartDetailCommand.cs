
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
using Business.Handlers.CartDetails.ValidationRules;


namespace Business.Handlers.CartDetails.Commands
{


	public class UpdateCartDetailCommand : IRequest<IResult>
	{
		public int Id { get; set; }
		public int CartId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime UpdatedDate { get; set; }

		public class UpdateCartDetailCommandHandler : IRequestHandler<UpdateCartDetailCommand, IResult>
		{
			private readonly ICartDetailRepository _cartDetailRepository;
			private readonly IMediator _mediator;

			public UpdateCartDetailCommandHandler(ICartDetailRepository cartDetailRepository, IMediator mediator)
			{
				_cartDetailRepository = cartDetailRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(UpdateCartDetailValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(UpdateCartDetailCommand request, CancellationToken cancellationToken)
			{
				var isThereCartDetailRecord = await _cartDetailRepository.GetAsync(u => u.Id == request.Id);


				isThereCartDetailRecord.CartId = request.CartId;
				isThereCartDetailRecord.ProductId = request.ProductId;
				isThereCartDetailRecord.Quantity = request.Quantity;
				isThereCartDetailRecord.CreatedDate = request.CreatedDate;
				isThereCartDetailRecord.UpdatedDate = request.UpdatedDate;


				_cartDetailRepository.Update(isThereCartDetailRecord);
				await _cartDetailRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}

