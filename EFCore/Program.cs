//using Northwind.EntityModels;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        //var configuration = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
        //    .AddEnvironmentVariables()
        //    .Build();

        //Log.Logger = new LoggerConfiguration()
        //    .ReadFrom.Configuration(configuration)
        //    .Enrich.FromLogContext()
        //    .WriteTo.Console()
        //    .CreateLogger();

        try
        {
            //Log.Information("Starting up");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            //Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            //Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();

            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                //webBuilder.UseSerilog(); // Add Serilog to log HTTP requests
            });
}


