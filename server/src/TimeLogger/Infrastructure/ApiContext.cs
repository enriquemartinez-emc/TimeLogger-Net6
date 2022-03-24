using Microsoft.EntityFrameworkCore;
using Timelogger.Domain;

namespace Timelogger
{
	public class ApiContext : DbContext
	{
		public ApiContext(DbContextOptions<ApiContext> options)
			: base(options)
		{
		}

		public DbSet<Project> Projects { get; set; }
		public DbSet<TimeLog> TimeLogs { get; set; }
	}
}
