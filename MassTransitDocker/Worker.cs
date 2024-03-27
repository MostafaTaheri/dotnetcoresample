using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using MassTransitDocker.Contracts;

namespace MassTransitDocker;

public class Worker : BackgroundService
{
    readonly IBus _bus;

    public Worker(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish(new PurchaseContract
            {
               ProductId = 1,
               PurchaseId = 120,
               UserId = 11,
               Count = 16,
            }, stoppingToken);

            await Task.Delay(1000, stoppingToken);
        }
    }
}