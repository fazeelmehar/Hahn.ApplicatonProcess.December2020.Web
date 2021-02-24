using Hahn.ApplicatonProcess.December2020.Data.Context;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Hahn.ApplicatonProcess.December2020.Domain.ServiceRegister;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AutoMapper;
using Swashbuckle.AspNetCore.SwaggerUI;
using Newtonsoft.Json;
using System.IO;
using Serilog;
using Newtonsoft.Json.Linq;
using Serilog.Formatting.Compact;

namespace Hahn.ApplicatonProcess.December2020.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Serilog.ILogger Logger { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Dev", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContextFactory<DataContext>(b =>
            b.UseInMemoryDatabase(databaseName: "AppicantDb"));
            services.AddScoped<DataContext>();
            services.AddAutoMapper(typeof(Startup));
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Domain.Helpers.AutoMapper());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddControllersWithViews();
            services.AddSingleton(mapper);
            services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true; // false by default
            });
            services.AddServices();
            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Hahn.ApplicationProcess.December2020",
                    Description = "A simple example ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Fazeel Liaqat",
                        Email = string.Empty,

                    }
                });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSwagger();
            // Enable middleware,
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.DocExpansion(DocExpansion.None);
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DefaultModelsExpandDepth(-1);
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableFilter();
                c.ShowExtensions();
                c.EnableValidator();
            });
            //app.UseResponseCompression();
            if (env.IsDevelopment())
            {
                app.UseCors("Dev");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            loggerFactory.AddSerilog(Logger);
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
