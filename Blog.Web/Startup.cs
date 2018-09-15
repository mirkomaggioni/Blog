using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blog.Core.Models;
using Blog.Core.Services;
using Blog.Core.Services.Common;

namespace Blog.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<BlogContextFactory>();
			services.AddSingleton<CategoryService>();
			services.AddSingleton<PostCategoryService>();
			services.AddSingleton<PostService>();
			services.AddSingleton<PostTagService>();
			services.AddSingleton<ReplyService>();
			services.AddSingleton<TagService>();

			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = _ => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BlogContext")));
			services.AddDbContext<BlogContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BlogContext")));

			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddDefaultUI()
				.AddRoles<IdentityRole>()
				.AddRoleManager<RoleManager<IdentityRole>>()
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireNonAlphanumeric = false;
				options.User.RequireUniqueEmail = true;
			});

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var applicationDbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
				applicationDbContext.Database.Migrate();
				var blogContext = serviceScope.ServiceProvider.GetService<BlogContext>();
				blogContext.Database.Migrate();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}


	}
}
