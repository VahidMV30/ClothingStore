namespace ClothingStore.Catalog.Application.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync(CancellationToken cancellationToken);
}