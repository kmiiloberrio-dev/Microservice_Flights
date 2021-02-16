using FluentValidation.AspNetCore;
using MediatR;
using MicroserviceFlight_Api.Middleware;
using MicroserviceFlight_Application.Flight;
using MicroserviceFlight_InfraEstructure.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MicroserviceFlight_Application.Flight.CreateFlight;

namespace MicroserviceFlight_Api
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
            services.AddCors(o => o.AddPolicy("corsAppFlight", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddMediatR(typeof(CreateFlight.Fire).Assembly);

            services.AddDbContext<FlightDBContext>(options =>
           {
               options.UseSqlServer(Configuration.GetConnectionString("ConnectionDatabase"));
           });

            //services.Configure<ApiSettings>(Configuration.GetSection(nameof(ApiSettings)));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo() { Title = "FLIGHT API", Version = "v1" });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor ingresa JWT con Bearer dentro del campo.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                        {
                          Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                        }
                       },
                       new string[] { }
                    }
                 });
            });

            services.AddControllers().AddMvcOptions(options =>
            {
            }).AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<ExecuteCreateFligh>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddFile("LogFlightMsc\\Log-{Date}.txt");

            app.UseCors("corsAppFlight");

            app.UseRouting();

            app.UseMiddleware<MagamentExceptionMiddleware>();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "FLIGHT API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
