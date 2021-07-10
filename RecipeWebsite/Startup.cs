using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeWebsite.Models;

namespace RecipeWebsite
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration config) => Configuration = config;
 
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(Configuration["Data:RecipeWebsite:ConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:RecipeWebsiteIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<AppIdentityDbContext>()
               .AddDefaultTokenProviders();
            services.AddMvc();
            //services.AddSingleton<IRecipeRepository, FakeRecipeRepository>();
            services.AddScoped<IRecipeRepository, SQLRecipeRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{RecipeId?}");
            });
            SeedData.EnsurePopulated(app);
            SeedDataIdentity.EnsurePopulated(app);
        }
    }
}
