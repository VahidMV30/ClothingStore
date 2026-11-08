namespace ClothingStore.Shared.Infrastructure.Persistence.Interceptors;

public class DispatchDomainEventInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEventAsync(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        await DispatchDomainEventAsync(eventData.Context, cancellationToken);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public async Task DispatchDomainEventAsync(DbContext? dbContext, CancellationToken cancellationToken = default)
    {
        if (dbContext is null) return;

        while (true)
        {
            var aggregates = dbContext.ChangeTracker
                .Entries<IAggregate>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .ToList();

            if (aggregates.Count == 0) break;

            var domainEvents = aggregates.SelectMany(x => x.DomainEvents).ToList();

            aggregates.ForEach(x => x.ClearDomainEvents());

            foreach (var domainEvent in domainEvents) await mediator.Publish(domainEvent, cancellationToken);
        }
    }
}