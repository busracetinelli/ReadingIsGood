
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
using Business.Handlers.Products.ValidationRules;


namespace Business.Handlers.Products.Commands
{


	public class UpdateProductCommand : IRequest<IResult>
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime UpdatedDate { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }

		public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IResult>
		{
			private readonly IProductRepository _productRepository;
			private readonly IMediator _mediator;

			public UpdateProductCommandHandler(IProductRepository productRepository, IMediator mediator)
			{
				_productRepository = productRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(UpdateProductValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
			{
				var isThereProductRecord = await _productRepository.GetAsync(u => u.Id == request.Id);


				isThereProductRecord.Code = request.Code;
				isThereProductRecord.CreatedDate = request.CreatedDate;
				isThereProductRecord.UpdatedDate = request.UpdatedDate;
				isThereProductRecord.IsDeleted = request.IsDeleted;
				isThereProductRecord.IsActive = request.IsActive;


				_productRepository.Update(isThereProductRecord);
				await _productRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}

