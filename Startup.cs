using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextCourses
{
    public class Startup
    {
        private string _UWApiKey = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _UWApiKey = Configuration["UWApiKey"];

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerDocument(settings =>
            {
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "NextCourses API";
                    document.Info.Description = "Find out which University of Waterloo courses you can take next.";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Mohit Patel",
                        Email = "m265pate@edu.uwaterloo.ca",
                        Url = "https://github.com/patelmohit/NextCoursesAPI"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "MIT",
                        Url = "https://opensource.org/licenses/MIT"
                    };
                };
            });
            services.AddHealthChecks();
        }

        public IConfiguration Configuration;
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseHealthChecks("/ready");
            app.Run( context =>
            {
                context.Response.Redirect("swagger/index.html");
                return Task.CompletedTask;
            });
        }
    }
}
