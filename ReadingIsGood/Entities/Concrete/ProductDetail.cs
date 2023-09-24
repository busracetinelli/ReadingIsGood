using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class ProductDetail : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public int ProductStock { get; set; }
		public decimal ProductPrice { get; set; }
		public int CurrencyId { get; set; }

		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }
		[ForeignKey("CurrencyId")]
		public virtual Currency Currency { get; set; }
	}
}
