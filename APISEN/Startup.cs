﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using TodoApi.Models;  

namespace TodoApi
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
            services.AddMvc();

            services.Add(new ServiceDescriptor(typeof(SenDBContext), new SenDBContext(Configuration.GetConnectionString("DefaultConnection"))));

                    services.AddSwaggerGen(c =>  
              {  
                c.SwaggerDoc("v1", new Info { Title = "API Senura - 10569142", Version = "v1" });  
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "ApiSenDS.xml");
                  c.IncludeXmlComments(xmlPath);
            }); //ref - http://foreverframe.net/documenting-asp-net-core-api-with-swagger/
     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

                // Enable middleware to serve generated Swagger as a JSON endpoint.  
          app.UseSwagger();  
      
          // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.  
          app.UseSwaggerUI(c =>  
          {  
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");  
          }); 

            app.UseMvc();
        }
    }
}
