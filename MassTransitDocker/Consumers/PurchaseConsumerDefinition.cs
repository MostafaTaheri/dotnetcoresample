using MassTransit;

namespace MassTransitDocker.Consumers;

public class PurchaseConsumerDefinition : ConsumerDefinition<PurchaseConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator
        endpointConfigurator, IConsumerConfigurator<PurchaseConsumer>
        consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    }
}
