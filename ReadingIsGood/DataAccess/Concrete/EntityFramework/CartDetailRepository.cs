
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
	public class CartDetailRepository : EfEntityRepositoryBase<CartDetail, ProjectDbContext>, ICartDetailRepository
	{
		public CartDetailRepository(ProjectDbContext context) : base(context)
		{
		}
	}
}
