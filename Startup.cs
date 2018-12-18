using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Arqsi_1160752_1161361_3DF.Data;
using Arqsi_1160752_1161361_3DF.Data.Repositories;

namespace Arqsi_1160752_1161361_3DF
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
    
             services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll",
                        builder =>
                        {
                            builder
                            .AllowAnyOrigin() 
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                        });
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IFinishRepository, FinishRepository>();
            services.AddScoped<IRestrictionRepository, RestrictionRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAndMaterialRepository, ProductAndMaterialRepository>();
            services.AddScoped<IProductRelationshipRepository, ProductRelationshipRepository>();

            services.AddDbContext<ClosetContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ClosetContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();  
            }

            //app.UseHttpsRedirection();
            app.UseCookiePolicy();

            app.UseCors("AllowAll");
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
