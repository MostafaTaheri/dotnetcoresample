using System.Threading.Tasks;
using MassTransit;
using MassTransitDocker.Contracts;

namespace MassTransitDocker.Producers;

public class PurchaseProducer
{
    private readonly IPublishEndpoint _publishEndpoint;

    public PurchaseProducer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishPurchase(PurchaseContract purchase)
    {
        await _publishEndpoint.Publish<PurchaseContract>(new()
        {
            ProductId = purchase.ProductId,
            PurchaseId = purchase.PurchaseId,
            Count = purchase.Count,
            UserId = purchase.UserId,
        });
    }
}
