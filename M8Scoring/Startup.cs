using System;
using System.Text;
using M8Scoring.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace M8Scoring {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			//Add EntityFramework Support for SqlServer
			services.AddEntityFrameworkSqlServer();

			//Add ApplicationDbContext
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			//Add ASP.NET Identity support
			services.AddIdentity<ApplicationUser, IdentityRole>(
				opts =>
				{
					opts.Password.RequireDigit = true;
					opts.Password.RequireLowercase = true;
					opts.Password.RequireUppercase = true;
					opts.Password.RequireNonAlphanumeric = false;
					opts.Password.RequiredLength = 7;
				})
				.AddEntityFrameworkStores<ApplicationDbContext>();

			//Add Authentication with JWT Tokens
			services.AddAuthentication(opts =>
			{
				opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(cfg =>
			{
				cfg.RequireHttpsMetadata = false;
				cfg.SaveToken = true;
				cfg.TokenValidationParameters = new TokenValidationParameters()
				{
					//standard configuration
					ValidIssuer = Configuration["Auth:Jwt:Issuer"],
					ValidAudience = Configuration["Auth:Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"])),
					ClockSkew = TimeSpan.Zero,

					// security switches
					RequireExpirationTime = true,
					ValidateIssuer = true,
					ValidateIssuerSigningKey = true,
					ValidateAudience = true
				};
			});

			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if(env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			//Add the AuthenticationMiddleware to the pipeline
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
									name: "default",
									template: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
							// To learn more about options for serving an Angular SPA from ASP.NET Core,
							// see https://go.microsoft.com/fwlink/?linkid=864501

							spa.Options.SourcePath = "ClientApp";

				if(env.IsDevelopment()) {
					spa.UseAngularCliServer(npmScript: "start");
				}
			});

			//create a service scope to get an ApplicationDbContext isntance using DI
			using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
				var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

				var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
				var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
				
				//create the db if it doesnt exits and applies and pending migration.
				dbContext.Database.Migrate();

				//Seed the db
				DbSeeder.Seed(dbContext, roleManager, userManager);
			}
		}
	}
}
