using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class CartDetail : IEntity
	{
		public int Id { get; set; }
		public int CartId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		
		[ForeignKey("CartId")]
		public virtual Cart Cart { get; set; }

		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }
	}
}
