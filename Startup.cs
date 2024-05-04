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
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using msvc_provinsi.Schema;

namespace msvc_provinsi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "msvc_provinsi", Version = "v1" });
            });
            services.AddDbContext<appContext>(
                dbContextOption => dbContextOption.UseMySql(Configuration["ConnectionStrings:app"],
                                                             Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"),
                                                             mySqlOptions => {
                                                                    mySqlOptions.EnableRetryOnFailure(
                                                                        maxRetryCount: 10,
                                                                        maxRetryDelay: TimeSpan.FromSeconds(30),
                                                                        errorNumbersToAdd: null
                                                                    );
                                                            })
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "msvc_provinsi v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllers();
            // });
        }
    }
}