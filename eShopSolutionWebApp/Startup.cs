using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using eShopsolution.Viewmodels.System;
using eShopSolution.ApiIntegration;
using eShopSolutionWebApp.LocalizationResources;
using FluentValidation.AspNetCore;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eShopSolutionWebApp
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
            services.AddHttpClient();
            services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
            
            var cultures = new[]
        {
                  new CultureInfo("vi"),
                  new CultureInfo("en"),
            };

            services.AddRazorPages()
          .AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(
            ops =>
            {
                      ops.ResourcesPath = "LocalizationResources";
                      ops.RequestLocalizationOptions = o =>
            {
                      o.SupportedCultures = cultures;
                      o.SupportedUICultures = cultures;
                      o.DefaultRequestCulture = new RequestCulture("vi");
            };
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(Options =>
            {

                Options.LoginPath = "/Account/Login";
                Options.AccessDeniedPath = "/User/Forbidden";

            });
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddTransient<ISlideApiClient, SlideApiClient>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IProductApiClient, ProductApiClient>();
            services.AddTransient<ICategoryApiClient, CategoryApClient>();
            services.AddTransient<IUserApiClient, UserApiClient>();

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

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "Product Category en",
                  pattern: "{culture}/categories/{id}", new
                  {
                      controller = "Product",
                      action = "Category"
                  });

                endpoints.MapControllerRoute(
                 name: "Product Category Vn",
                 pattern: "{culture}/danh-muc/{id}", new
                 {
                     controller = "Product",
                     action = "Category"
                 });

                endpoints.MapControllerRoute(
              name: "Product detail en",
              pattern: "{culture}/products/{id}", new
              {
                  controller = "Product",
                  action = "Detail"
              });

                endpoints.MapControllerRoute(
                 name: "Product detail Vn",
                 pattern: "{culture}/san-pham/{id}", new
                 {
                     controller = "Product",
                     action = "Detail"
                 });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{culture=en}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
