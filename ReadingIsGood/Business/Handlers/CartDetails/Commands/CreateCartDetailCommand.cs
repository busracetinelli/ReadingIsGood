
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
using Business.Handlers.CartDetails.ValidationRules;

namespace Business.Handlers.CartDetails.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class CreateCartDetailCommand : IRequest<IResult>
	{

		public int CartId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime UpdatedDate { get; set; }


		public class CreateCartDetailCommandHandler : IRequestHandler<CreateCartDetailCommand, IResult>
		{
			private readonly ICartDetailRepository _cartDetailRepository;
			private readonly IMediator _mediator;
			public CreateCartDetailCommandHandler(ICartDetailRepository cartDetailRepository, IMediator mediator)
			{
				_cartDetailRepository = cartDetailRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(CreateCartDetailValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(CreateCartDetailCommand request, CancellationToken cancellationToken)
			{
				var isThereCartDetailRecord = _cartDetailRepository.Query().Any(u => u.CartId == request.CartId);

				if (isThereCartDetailRecord == true)
					return new ErrorResult(Messages.NameAlreadyExist);

				var addedCartDetail = new CartDetail
				{
					CartId = request.CartId,
					ProductId = request.ProductId,
					Quantity = request.Quantity,
					CreatedDate = request.CreatedDate,
					UpdatedDate = request.UpdatedDate,

				};

				_cartDetailRepository.Add(addedCartDetail);
				await _cartDetailRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Added);
			}
		}
	}
}