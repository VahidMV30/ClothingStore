namespace ClothingStore.Ordering.Application.Interfaces.Repositories;

public interface IOutboxWriter
{
    Task AddAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken);
}