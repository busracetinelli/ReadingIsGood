
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
using Business.Handlers.ProductDetails.ValidationRules;

namespace Business.Handlers.ProductDetails.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class CreateProductDetailCommand : IRequest<IResult>
	{

		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public int ProductStock { get; set; }
		public decimal ProductPrice { get; set; }


		public class CreateProductDetailCommandHandler : IRequestHandler<CreateProductDetailCommand, IResult>
		{
			private readonly IProductDetailRepository _productDetailRepository;
			private readonly IMediator _mediator;
			public CreateProductDetailCommandHandler(IProductDetailRepository productDetailRepository, IMediator mediator)
			{
				_productDetailRepository = productDetailRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(CreateProductDetailValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(CreateProductDetailCommand request, CancellationToken cancellationToken)
			{
				var isThereProductDetailRecord = _productDetailRepository.Query().Any(u => u.ProductId == request.ProductId);

				if (isThereProductDetailRecord == true)
					return new ErrorResult(Messages.NameAlreadyExist);

				var addedProductDetail = new ProductDetail
				{
					ProductId = request.ProductId,
					ProductName = request.ProductName,
					ProductDescription = request.ProductDescription,
					ProductStock = request.ProductStock,
					ProductPrice = request.ProductPrice,

				};

				_productDetailRepository.Add(addedProductDetail);
				await _productDetailRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Added);
			}
		}
	}
}