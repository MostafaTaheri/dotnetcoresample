namespace MassTransitDocker.Contracts;


public record PurchaseContract
{
    public int PurchaseId { get; init; }

    public int ProductId { get; init; }

    public int UserId { get; init; }

    public int Count { get; init; }
}
