using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoToDos.Web.Models;

namespace ToDoToDos.Web.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }

		public DbSet<Mission> Missions { get; set; }

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	}
}
