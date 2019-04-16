using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using ToDoToDos.Web.Data;
using ToDoToDos.Web.Interfaces;
using ToDoToDos.Web.Models;

namespace ToDoToDos.Web
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IHostingEnvironment hostingEnvironment)
		{
			Configuration = new ConfigurationBuilder()
				.SetBasePath(hostingEnvironment.ContentRootPath)
				.AddJsonFile("appsettings.json")
				.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			// inject app settings
			services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

			// add Db context with default connection
			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<ApplicationUser>()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 8;
			});

			services.AddTransient<IMissionRepository, MissionRepository>();

			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
				.AddJsonOptions(options => 
					{
						var resolver = options.SerializerSettings.ContractResolver;

						if (resolver != null)
							((DefaultContractResolver) resolver).NamingStrategy = null; // use real values to serialize
					});

			services.AddCors();

			// json Web Token Auth

			var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JwtSecret"].ToString());

			services.AddAuthentication(auth =>
			{
				auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(auth =>
			{
				auth.RequireHttpsMetadata = false;
				auth.SaveToken = false;
				auth.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				};
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.Use(async (ctx, next) =>
			{
				await next();
				if (ctx.Response.StatusCode == 204)
				{
					ctx.Response.ContentLength = 0;
				}
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(options => options.WithOrigins(Configuration["ApplicationSettings:ClientUrl"].ToString())
											.AllowAnyMethod()
											.AllowAnyHeader());

			app.UseAuthentication();
			app.UseStaticFiles();
			app.UseMvc();

			//SeedData.EnsurePopulated(app);
		}
	}
}
