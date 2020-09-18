using System;
using System.Collections.Generic;
using System.IO;
using Drop.Application;
using Drop.Application.Commands;
using Drop.Application.Services;
using Drop.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Drop.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddApplication();
            services.AddInfrastructure();
            services.AddScoped<DummyMiddleware>();
            services.AddScoped<ErrorHandlerMiddleware>();
            services.Configure<ApiOptions>(_configuration.GetSection("api"));
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("Drop", new OpenApiInfo
                {
                    Title = "Drop API",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/Drop/swagger.json", "Drop API v1");
            });

            app.UseMiddleware<ErrorHandlerMiddleware>();
            
            app.Use(async (ctx, next) =>
            {
                Console.WriteLine("I'm the first middleware");
                await next();
            });
            
            app.Use(async (ctx, next) =>
            {
                Console.WriteLine("I'm the second middleware");
                await next();
            });
            
            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Query.TryGetValue("token", out var token) && token == "secret")
                {
                    await ctx.Response.WriteAsync("Secret");
                    return;    
                }
                
                await next();
            });

            app.UseMiddleware<DummyMiddleware>();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
                endpoints.MapGet("/", async context =>
                {
                    var messengers = context.RequestServices.GetRequiredService<IEnumerable<IMessenger>>(); // Resolve all instances
                    // var messenger1 = context.RequestServices.GetRequiredService<IMessenger>(); //Service locator
                    // var messenger2 = context.RequestServices.GetRequiredService<IMessenger>(); //Service locator
                    // await context.Response.WriteAsync($"{messenger1.GetMessage()} {messenger2.GetMessage()}");
                    var options = context.RequestServices.GetRequiredService<IOptions<ApiOptions>>();
                    await context.Response.WriteAsync(options.Value.Name);
                });

                endpoints.MapGet("parcels/{parcelId:guid}", async context =>
                {
                    var parcelId = Guid.Parse(context.Request.RouteValues["parcelId"].ToString());
                    var parcelsService = context.RequestServices.GetRequiredService<IParcelsService>();
                    var parcel = await parcelsService.GetAsync(parcelId);
                    if (parcel is null)
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        return;
                    }

                    // var json = JsonSerializer.Serialize(parcel);
                    var json = JsonConvert.SerializeObject(parcel);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json);
                });

                endpoints.MapPost("parcels", async context =>
                {
                    var parcelsService = context.RequestServices.GetRequiredService<IParcelsService>();
                    var json = await new StreamReader(context.Request.Body).ReadToEndAsync();
                    var command = JsonConvert.DeserializeObject<AddParcel>(json);
                    // var command = JsonSerializer.Deserialize<AddParcel>(json, new JsonSerializerOptions
                    // {
                    //     PropertyNameCaseInsensitive = true
                    // });
                    await parcelsService.AddAsync(command);
                    context.Response.Headers.Add("Location", $"parcels/{command.Id}");
                    context.Response.StatusCode = StatusCodes.Status201Created;
                });
            });
        }
    }
}
