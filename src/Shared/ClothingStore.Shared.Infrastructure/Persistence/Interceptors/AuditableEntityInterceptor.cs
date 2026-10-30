namespace ClothingStore.Shared.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public static void UpdateEntities(DbContext? dbContext)
    {
        if (dbContext is null) return;

        var now = DateTime.UtcNow;

        foreach (var entry in dbContext.ChangeTracker.Entries<IEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                continue;
            }

            if (entry.State == EntityState.Modified || entry.HasChangedOwnEntities()) entry.Entity.UpdatedAt = now;
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnEntities(this EntityEntry entry)
    {
        return entry.References.Any(x =>
            x.TargetEntry != null &&
            x.TargetEntry.Metadata.IsOwned() &&
            x.TargetEntry.State is EntityState.Added or EntityState.Modified);
    }
}