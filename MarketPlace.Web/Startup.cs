using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using MarketPlace.App.Services.Implementations;
using MarketPlace.App.Services.Interfaces;
using MarketPlace.Data.Context;
using MarketPlace.Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
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
            //services.AddScoped<IPasswordHelper, PasswordHelper>();
            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
			services.AddScoped<ISmsService, SmsService>();
			services.AddScoped<IContactService, ContactService>();
		services.AddScoped<ISellerService , SellerService>();
			services.AddScoped<IProductService, ProductService>();

			#endregion
			#region data protection

			services.AddDataProtection()
				.PersistKeysToFileSystem(new System.IO.DirectoryInfo(Directory.GetCurrentDirectory() + "\\wwwroot\\Auth\\"))
				.SetApplicationName("MarketPlaceProject")
				.SetDefaultKeyLifetime(TimeSpan.FromSeconds(30));

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
