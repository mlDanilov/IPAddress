using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IPAddressRepository;
using IPAddressRepository.Classes;
using IPAddressRepository.Entities;
using IPAddressRepository.Entities.Dapper;

namespace IPAddressWebApp
{
    using IGeneralRepository = IPAddressRepository.IGeneralRepository;

    public class Startup
    {
        public Startup(IConfiguration configuration_)
        {
            Configuration = configuration_;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services_)
        {
            var conn = "Server=.\\MSSQLSERVER_DEV;Initial Catalog=IPAddress;Integrated Security=True";
            
            
            services_.AddControllersWithViews();
            services_.AddTransient<IGeneralRepository, GeneralRepository>
                (provider => new GeneralRepository(conn));
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app_, IWebHostEnvironment env_)
        {
            if (env_.IsDevelopment())
            {
                app_.UseDeveloperExceptionPage();
            }
            else
            {
                app_.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app_.UseHsts();
            }
            app_.UseHttpsRedirection();
            app_.UseStaticFiles();

            app_.UseRouting();

            app_.UseAuthorization();

            app_.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
