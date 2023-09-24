
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.CartDetails.Queries
{
	public class GetCartDetailQuery : IRequest<IDataResult<CartDetail>>
	{
		public int Id { get; set; }

		public class GetCartDetailQueryHandler : IRequestHandler<GetCartDetailQuery, IDataResult<CartDetail>>
		{
			private readonly ICartDetailRepository _cartDetailRepository;
			private readonly IMediator _mediator;

			public GetCartDetailQueryHandler(ICartDetailRepository cartDetailRepository, IMediator mediator)
			{
				_cartDetailRepository = cartDetailRepository;
				_mediator = mediator;
			}
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IDataResult<CartDetail>> Handle(GetCartDetailQuery request, CancellationToken cancellationToken)
			{
				var cartDetail = await _cartDetailRepository.GetAsync(p => p.Id == request.Id);
				return new SuccessDataResult<CartDetail>(cartDetail);
			}
		}
	}
}
