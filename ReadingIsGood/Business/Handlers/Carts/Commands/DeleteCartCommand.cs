
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


namespace Business.Handlers.Carts.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class DeleteCartCommand : IRequest<IResult>
	{
		public int Id { get; set; }

		public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, IResult>
		{
			private readonly ICartRepository _cartRepository;
			private readonly IMediator _mediator;

			public DeleteCartCommandHandler(ICartRepository cartRepository, IMediator mediator)
			{
				_cartRepository = cartRepository;
				_mediator = mediator;
			}

			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
			{
				var cartToDelete = _cartRepository.Get(p => p.Id == request.Id);

				_cartRepository.Delete(cartToDelete);
				await _cartRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Deleted);
			}
		}
	}
}

