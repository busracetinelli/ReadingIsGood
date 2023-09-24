
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


namespace Business.Handlers.ProductDetails.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class DeleteProductDetailCommand : IRequest<IResult>
	{
		public int Id { get; set; }

		public class DeleteProductDetailCommandHandler : IRequestHandler<DeleteProductDetailCommand, IResult>
		{
			private readonly IProductDetailRepository _productDetailRepository;
			private readonly IMediator _mediator;

			public DeleteProductDetailCommandHandler(IProductDetailRepository productDetailRepository, IMediator mediator)
			{
				_productDetailRepository = productDetailRepository;
				_mediator = mediator;
			}

			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(DeleteProductDetailCommand request, CancellationToken cancellationToken)
			{
				var productDetailToDelete = _productDetailRepository.Get(p => p.Id == request.Id);

				_productDetailRepository.Delete(productDetailToDelete);
				await _productDetailRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Deleted);
			}
		}
	}
}

