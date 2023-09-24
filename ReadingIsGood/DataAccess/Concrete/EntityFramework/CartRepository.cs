﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
	public class CartRepository : EfEntityRepositoryBase<Cart, ProjectDbContext>, ICartRepository
	{
		public CartRepository(ProjectDbContext context) : base(context)
		{
		}
	}
}
