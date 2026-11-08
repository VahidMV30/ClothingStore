namespace ClothingStore.Catalog.Application.Interfaces.Repositories;

public interface IOutboxWriter
{
    Task AddAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken);
}