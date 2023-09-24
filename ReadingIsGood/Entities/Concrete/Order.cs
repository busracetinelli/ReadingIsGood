using Core.Entities.Concrete;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace Entities.Concrete
{
	public class Order : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public DateTime DeletedDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }
	}
}
