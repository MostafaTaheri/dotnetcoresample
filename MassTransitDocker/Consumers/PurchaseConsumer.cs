using System.Threading.Tasks;
using MassTransit;
using MassTransitDocker.Contracts;
using Microsoft.Extensions.Logging;

namespace MassTransitDocker.Consumers;


public class PurchaseConsumer : IConsumer<PurchaseContract>
{
    readonly ILogger<PurchaseConsumer> _logger;

    public PurchaseConsumer(ILogger<PurchaseConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<PurchaseContract> context)
    {
        _logger.LogInformation($"Recieved text: {context.Message.PurchaseId}");

        return Task.CompletedTask;
    }
}
