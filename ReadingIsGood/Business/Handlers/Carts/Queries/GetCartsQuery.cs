
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

namespace Business.Handlers.Carts.Queries
{

	public class GetCartsQuery : IRequest<IDataResult<IEnumerable<Cart>>>
	{
		public class GetCartsQueryHandler : IRequestHandler<GetCartsQuery, IDataResult<IEnumerable<Cart>>>
		{
			private readonly ICartRepository _cartRepository;
			private readonly IMediator _mediator;

			public GetCartsQueryHandler(ICartRepository cartRepository, IMediator mediator)
			{
				_cartRepository = cartRepository;
				_mediator = mediator;
			}

			[PerformanceAspect(5)]
			[CacheAspect(10)]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IDataResult<IEnumerable<Cart>>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
			{
				return new SuccessDataResult<IEnumerable<Cart>>(await _cartRepository.GetListAsync());
			}
		}
	}
}