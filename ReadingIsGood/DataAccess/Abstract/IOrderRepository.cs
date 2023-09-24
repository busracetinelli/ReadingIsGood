
using System;
using System.Collections.Generic;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
	public interface IOrderRepository : IEntityRepository<Order>
	{
		List<Order> GetOrderList();
		Order OrderById(int OrderId);
	}
}