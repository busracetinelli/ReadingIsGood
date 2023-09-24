
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.CartDetails.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class DeleteCartDetailCommand : IRequest<IResult>
	{
		public int Id { get; set; }

		public class DeleteCartDetailCommandHandler : IRequestHandler<DeleteCartDetailCommand, IResult>
		{
			private readonly ICartDetailRepository _cartDetailRepository;
			private readonly IMediator _mediator;

			public DeleteCartDetailCommandHandler(ICartDetailRepository cartDetailRepository, IMediator mediator)
			{
				_cartDetailRepository = cartDetailRepository;
				_mediator = mediator;
			}

			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(DeleteCartDetailCommand request, CancellationToken cancellationToken)
			{
				var cartDetailToDelete = _cartDetailRepository.Get(p => p.Id == request.Id);

				_cartDetailRepository.Delete(cartDetailToDelete);
				await _cartDetailRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Deleted);
			}
		}
	}
}

