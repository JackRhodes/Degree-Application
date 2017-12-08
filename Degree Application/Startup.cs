using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Degree_Application.Models;
using Degree_Application.Data;
using Microsoft.AspNetCore.Identity;
using Degree_Application.Data.Repositories;

namespace Degree_Application
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

            services.AddDbContext<Degree_ApplicationContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Degree_ApplicationContext")));

            services.AddIdentity<AccountModel, IdentityRole>()
                .AddEntityFrameworkStores<Degree_ApplicationContext>()
                .AddDefaultTokenProviders();

            Func<IServiceProvider, IItemRepository> supplier = serviceProvider => new ItemRepository(serviceProvider.GetService<Degree_ApplicationContext>(), serviceProvider.GetService<UserManager<AccountModel>>());
            services.AddTransient(supplier);

            //  services.AddTransient<UserManager<AccountModel>>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //Enables login system
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                /*routes.MapRoute(
                    name: "search",
                    template: "{controller=Item}/{action=Search}/{search?}");*/

            });
        }
    }
}
