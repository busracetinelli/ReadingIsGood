
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework
{
	public class OrderRepository : EfEntityRepositoryBase<Order, ProjectDbContext>//, IOrderRepository
	{
		public OrderRepository(ProjectDbContext context) : base(context)
		{

		}
		//public List<Order> GetOrderList(int orderId)
		//{
			//var result = (from user in Context.Users
			//			  join userGroup in Context.UserGroups on user.UserId equals userGroup.UserId
			//			  join groupClaim in Context.GroupClaims on userGroup.GroupId equals groupClaim.GroupId
			//			  join operationClaim in Context.OperationClaims on groupClaim.ClaimId equals operationClaim.Id
			//			  where user.UserId == userId
			//			  select new
			//			  {
			//				  operationClaim.Name
			//			  }).Union(from user in Context.Users
			//					   join userClaim in Context.UserClaims on user.UserId equals userClaim.UserId
			//					   join operationClaim in Context.OperationClaims on userClaim.ClaimId equals operationClaim.Id
			//					   where user.UserId == userId
			//					   select new
			//					   {
			//						   operationClaim.Name
			//					   });

			//return result.Select(x => new Order { Name = x.Name }).Distinct()
			//	.ToList();
	//	}
	}
}
