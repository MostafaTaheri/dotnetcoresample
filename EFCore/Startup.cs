using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Northwind.EntityModels;
using Northwind.Reposotories;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
//using Prometheus;
using System.Diagnostics;
using System.Net.NetworkInformation;



//using OpenTelemetry.Logs;

using OpenTelemetry.Metrics;
//using OpenTelemetry.Resources;
//using OpenTelemetry.Trace;
//using OpenTelemetry;
//using OpenTelemetry.Exporter;
using OpenTelemetry.Instrumentation.AspNetCore;

using OpenTelemetry.Trace;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry;


public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var serviceName = "EFCoreMostafaNew2";
        var serviceVersion = "1.0";

        var resourceBuilder =
            ResourceBuilder
                .CreateDefault()
                .AddService(serviceName: serviceName, serviceVersion: serviceVersion)
                .AddAttributes(new Dictionary<string, object>
                {
                    ["environment.name"] = "development",
                    ["team.name"] = "backend"
                })
                .AddTelemetrySdk();

        Sdk.CreateTracerProviderBuilder()
            .AddOtlpExporter()
            .SetResourceBuilder(resourceBuilder)
            .AddConsoleExporter()
            .AddAspNetCoreInstrumentation().Build();


        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo {
                Title = "Mostafa EF Core API",
                Version = "v1"
            });
        });

        services.AddDbContext<NorthwindDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString(
                "DefaultConnection")));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        

        IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Configuration.GetConnectionString(
                "RedisConnection");
            options.InstanceName = "EFCoreSample-";
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        //app.UseMetricServer("/metrics");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}