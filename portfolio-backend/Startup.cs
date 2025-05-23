using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PortfolioBackend
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
            // services.AddCors(options =>
            // {
            //     options.AddPolicy("AllowReactApp", builder =>
            //     {
            //         builder.WithOrigins("http://localhost:3000")
            //             .AllowAnyHeader()
            //             .AllowAnyMethod();
            //     });
            // });
            // services.AddControllers();
            services.AddCors(options =>
      options.AddDefaultPolicy(b =>
        b.WithOrigins("https://larEvans.github.io")
         .AllowAnyMethod()
         .AllowAnyHeader()
      )
    );
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHttpsRedirection();

            app.UseRouting();

            // Serve index.html by default
            app.UseDefaultFiles();    // will look for index.html
            app.UseStaticFiles();     // serve everything under wwwroot/

            // Apply your named CORS policy
            app.UseCors("AllowReactApp");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // ANY non-API route falls back to index.html
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }

}