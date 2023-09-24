using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
	public sealed class PostgreDbContext : ProjectDbContext
	{
		public PostgreDbContext(DbContextOptions<PostgreDbContext> options, IConfiguration configuration)
			: base(options, configuration)
		{
			
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			
			if (!optionsBuilder.IsConfigured)
			{
				var options = optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PostgreDbConnection"));
			//	optionsBuilder.Options.MaxPoolSize = 50;
				base.OnConfiguring(options);
			}
		}
	}
}