using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using ToDoToDos.Web.Data;

namespace ToDoToDos.Web.Models
{
	public static class SeedData
	{
		public static void EnsurePopulated(IApplicationBuilder app)
		{
			// creates scope connection 
			using (var serviceScope = app.ApplicationServices.CreateScope())
			{
				// create new db context
				var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

				// if there are no games in db
				if (!context.Missions.Any())
				{
					// add new games
					context.Missions.AddRange(
					new Mission
					{
						Name = "Do homework",
						Description = "1 sdfwewwwwerg erge rg e ge er g",
						DateToDo = new DateTime(2019, 2, 20)
					},
					new Mission
					{
						Name = "Buy some food",
						Description = "2 dfgdfger ertyery eyeryery eryery",
						DateToDo = new DateTime(2019, 3, 1)
					},
					new Mission
					{
						Name = "Go for a walk",
						Description = "3 erteryfhgh fghfgh tryrtuy vbnvbn",
						DateToDo = new DateTime(2019, 2, 15)
					});

					// save all changes (all added objects)
					context.SaveChanges();
				}
			}
		}
	}
}
