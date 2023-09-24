
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ProductDetails.Queries
{
	public class GetProductDetailQuery : IRequest<IDataResult<ProductDetail>>
	{
		public int Id { get; set; }

		public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, IDataResult<ProductDetail>>
		{
			private readonly IProductDetailRepository _productDetailRepository;
			private readonly IMediator _mediator;

			public GetProductDetailQueryHandler(IProductDetailRepository productDetailRepository, IMediator mediator)
			{
				_productDetailRepository = productDetailRepository;
				_mediator = mediator;
			}
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IDataResult<ProductDetail>> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
			{
				var productDetail = await _productDetailRepository.GetAsync(p => p.Id == request.Id);
				return new SuccessDataResult<ProductDetail>(productDetail);
			}
		}
	}
}
