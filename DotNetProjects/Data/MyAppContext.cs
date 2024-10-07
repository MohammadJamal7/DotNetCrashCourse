using DotNetProjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetProjects.Data
{
	public class MyAppContext : DbContext
	{

	  public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
		{

		}
		public DbSet<Dept> depts { get; set; }

	}
}