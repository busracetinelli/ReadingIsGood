
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.ProductDetails.ValidationRules;


namespace Business.Handlers.ProductDetails.Commands
{


	public class UpdateProductDetailCommand : IRequest<IResult>
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public int ProductStock { get; set; }
		public decimal ProductPrice { get; set; }

		public class UpdateProductDetailCommandHandler : IRequestHandler<UpdateProductDetailCommand, IResult>
		{
			private readonly IProductDetailRepository _productDetailRepository;
			private readonly IMediator _mediator;

			public UpdateProductDetailCommandHandler(IProductDetailRepository productDetailRepository, IMediator mediator)
			{
				_productDetailRepository = productDetailRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(UpdateProductDetailValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(UpdateProductDetailCommand request, CancellationToken cancellationToken)
			{
				var isThereProductDetailRecord = await _productDetailRepository.GetAsync(u => u.Id == request.Id);


				isThereProductDetailRecord.ProductId = request.ProductId;
				isThereProductDetailRecord.ProductName = request.ProductName;
				isThereProductDetailRecord.ProductDescription = request.ProductDescription;
				isThereProductDetailRecord.ProductStock = request.ProductStock;
				isThereProductDetailRecord.ProductPrice = request.ProductPrice;


				_productDetailRepository.Update(isThereProductDetailRecord);
				await _productDetailRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}

