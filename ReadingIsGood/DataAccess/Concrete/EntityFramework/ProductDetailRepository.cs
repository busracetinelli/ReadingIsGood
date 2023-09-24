
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
	public class ProductDetailRepository : EfEntityRepositoryBase<ProductDetail, ProjectDbContext>, IProductDetailRepository
	{
		public ProductDetailRepository(ProjectDbContext context) : base(context)
		{
		}
	}
}
