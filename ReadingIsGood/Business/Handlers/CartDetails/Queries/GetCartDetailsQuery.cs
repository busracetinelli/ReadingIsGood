
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.CartDetails.Queries
{

	public class GetCartDetailsQuery : IRequest<IDataResult<IEnumerable<CartDetail>>>
	{
		public class GetCartDetailsQueryHandler : IRequestHandler<GetCartDetailsQuery, IDataResult<IEnumerable<CartDetail>>>
		{
			private readonly ICartDetailRepository _cartDetailRepository;
			private readonly IMediator _mediator;

			public GetCartDetailsQueryHandler(ICartDetailRepository cartDetailRepository, IMediator mediator)
			{
				_cartDetailRepository = cartDetailRepository;
				_mediator = mediator;
			}

			[PerformanceAspect(5)]
			[CacheAspect(10)]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IDataResult<IEnumerable<CartDetail>>> Handle(GetCartDetailsQuery request, CancellationToken cancellationToken)
			{
				return new SuccessDataResult<IEnumerable<CartDetail>>(await _cartDetailRepository.GetListAsync());
			}
		}
	}
}