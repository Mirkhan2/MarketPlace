using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using MarketPlace.Applicationn.Services.Implementations;
using MarketPlace.Applicationn.Services.Interfaces;
using MarketPlace.DataLayerr.Context;
using MarketPlace.DataLayerr.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarketPlace.Web
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
			#region config services

			services.AddControllersWithViews();
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IUserService, UserService>();
           services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<IPasswordHelper, IPasswordHelper>();
            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
			services.AddScoped<ISmsService, SmsService>();
		

            #endregion

            #region config database

            services.AddDbContext<MarketPlaceDbContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("MarketPlaceConnection"));
			});

			#endregion

			#region authentication

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			}).AddCookie(options =>
			{
				options.LoginPath = "/login";
				options.LogoutPath = "/log-out";
				options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
			});

			#endregion

			#region html encoder

			services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(new[]
				{
					UnicodeRanges.BasicLatin , UnicodeRanges.Arabic
				}));

			#endregion
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapControllerRoute(
			//		 name: "areas",
			//		 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
			//	   );

			app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
		});
		}

	}
}
