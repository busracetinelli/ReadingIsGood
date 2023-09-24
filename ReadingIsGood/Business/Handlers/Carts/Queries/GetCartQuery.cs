
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Carts.Queries
{
	public class GetCartQuery : IRequest<IDataResult<Cart>>
	{
		public int Id { get; set; }

		public class GetCartQueryHandler : IRequestHandler<GetCartQuery, IDataResult<Cart>>
		{
			private readonly ICartRepository _cartRepository;
			private readonly IMediator _mediator;

			public GetCartQueryHandler(ICartRepository cartRepository, IMediator mediator)
			{
				_cartRepository = cartRepository;
				_mediator = mediator;
			}
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IDataResult<Cart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
			{
				var cart = await _cartRepository.GetAsync(p => p.Id == request.Id);
				return new SuccessDataResult<Cart>(cart);
			}
		}
	}
}
