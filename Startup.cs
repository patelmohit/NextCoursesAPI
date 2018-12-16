using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

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
        }

        public IConfiguration Configuration;
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                if (string.IsNullOrEmpty(_UWApiKey))
                {
                    await context.Response.WriteAsync("No API Key detected.");
                }
                else
                {
                    await context.Response.WriteAsync("API key detected.");
                }
            });
        }
    }
}
