using first.Models;
using first.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace first
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
            services.AddSession(c =>
            {
                c.IdleTimeout = TimeSpan.FromMinutes(100);

            });
            services.AddControllersWithViews();
            services.AddTransient<IUserRepository, UserServices>();

            services.AddTransient<IOrderDetailRepository, OrderDetailServices>();

            services.AddTransient<IAdminRepository, AdminServices>();
            services.AddTransient<IProductRepository, ProductServices>();

            services.AddTransient<ICategoryRepository, CategoryServices>();

            services.AddTransient<IOrderRepository, OrderServices>();

            services.AddDbContext<Context>(options =>
            options.UseSqlServer("Data Source=.;Initial Catalog=Nada;Integrated Security=True"));//dbms , server name , db, autha-windows

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
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
