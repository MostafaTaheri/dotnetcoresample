using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;
using MassTransitDocker.Consumers;

namespace MassTransitDocker
{
    public class Program
    {
        static bool? _isRunningInContainer;

        static bool IsRunningInContainer =>
            _isRunningInContainer ??= bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) && inContainer;
   
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddDelayedMessageScheduler();

                        x.SetKebabCaseEndpointNameFormatter();

                        // By default, sagas are in-memory, but should be changed to a durable
                        // saga repository.
                        //x.SetInMemorySagaRepositoryProvider();

                        var entryAssembly = Assembly.GetEntryAssembly();

                        x.AddConsumers(entryAssembly);
                        x.AddSagaStateMachines(entryAssembly);
                        x.AddSagas(entryAssembly);
                        x.AddActivities(entryAssembly);

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            //if (IsRunningInContainer)
                            //    cfg.Host("localhost", "/", h =>
                            //    {
                            //        h.Username("admin");
                            //        h.Password("admin");                           
                            //    });              

                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("admin");
                                h.Password("admin");
                            });

                            cfg.UseDelayedMessageScheduler();
                            
                            cfg.ConfigureEndpoints(context);
                            cfg.UseMessageRetry(r => r.Immediate(5));
                        });

                        //Consumers by the mediator
                        x.AddMediator(cfg =>
                        {
                            cfg.AddConsumer<PurchaseConsumer, PurchaseConsumerDefinition>();
                        });

                    });

                    services.AddOptions<MassTransitHostOptions>().Configure(options =>
                    {
                        options.WaitUntilStarted = true;
                        options.StartTimeout = TimeSpan.FromSeconds(10);
                        options.StopTimeout = TimeSpan.FromSeconds(30);
                    });

                    //services.AddHostedService<Worker>();
                });
    }
}

