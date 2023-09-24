
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
using Business.Handlers.Carts.ValidationRules;


namespace Business.Handlers.Carts.Commands
{


	public class UpdateCartCommand : IRequest<IResult>
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime UpdatedDate { get; set; }
		public bool IsDeleted { get; set; }

		public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, IResult>
		{
			private readonly ICartRepository _cartRepository;
			private readonly IMediator _mediator;

			public UpdateCartCommandHandler(ICartRepository cartRepository, IMediator mediator)
			{
				_cartRepository = cartRepository;
				_mediator = mediator;
			}

			[ValidationAspect(typeof(UpdateCartValidator), Priority = 1)]
			[CacheRemoveAspect("Get")]
			[LogAspect(typeof(FileLogger))]
			[SecuredOperation(Priority = 1)]
			public async Task<IResult> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
			{
				var isThereCartRecord = await _cartRepository.GetAsync(u => u.Id == request.Id);


				isThereCartRecord.UserId = request.UserId;
				isThereCartRecord.CreatedDate = request.CreatedDate;
				isThereCartRecord.UpdatedDate = request.UpdatedDate;


				_cartRepository.Update(isThereCartRecord);
				await _cartRepository.SaveChangesAsync();
				return new SuccessResult(Messages.Updated);
			}
		}
	}
}

