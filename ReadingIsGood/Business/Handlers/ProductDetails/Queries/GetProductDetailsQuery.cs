
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

namespace Business.Handlers.ProductDetails.Queries
{

	public class GetProductDetailsQuery : IRequest<IDataResult<IEnumerable<ProductDetail>>>
	{
		public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, IDataResult<IEnumerable<ProductDetail>>>
		{
			private readonly IProductDetailRepository _productDetailRepository;
			private readonly IMediator _mediator;

			public GetProductDetailsQueryHandler(IProductDetailRepository productDetailRepository, IMediator mediator)
			{
				_productDetailRepository = productDetailRepository;
				_mediator = mediator;
			}

			[PerformanceAspect(5)]
			[CacheAspect(10)]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IDataResult<IEnumerable<ProductDetail>>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
			{
				return new SuccessDataResult<IEnumerable<ProductDetail>>(await _productDetailRepository.GetListAsync());
			}
		}
	}
}