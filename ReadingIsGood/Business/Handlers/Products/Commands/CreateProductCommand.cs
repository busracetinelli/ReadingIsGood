
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
using Business.Handlers.Products.ValidationRules;

namespace Business.Handlers.Products.Commands
{
	/// <summary>
	/// 
	/// </summary>
	public class CreateProductCommand : IRequest<IResult>
	{

		public string Code { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime UpdatedDate { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsActive { get; set; }


		public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IResult>
		{
			private readonly IProductRepository _productRepository;
			private readonly IMediator _mediator;
			public CreateProductCommandHandler(IProductRepository productRepository, IMediator mediator)
			{
				_productRepository = productRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(CreateProductValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
			{
				var isThereProductRecord = _productRepository.Query().Any(u => u.Code == request.Code);

				if (isThereProductRecord == true)
					return new ErrorResult(Messages.NameAlreadyExist);

				var addedProduct = new Product
				{
					Code = request.Code,
					CreatedDate = request.CreatedDate,
					UpdatedDate = request.UpdatedDate,
					IsDeleted = request.IsDeleted,
					IsActive = request.IsActive,

				};

				_productRepository.Add(addedProduct);
				await _productRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Added);
			}
		}
	}
}