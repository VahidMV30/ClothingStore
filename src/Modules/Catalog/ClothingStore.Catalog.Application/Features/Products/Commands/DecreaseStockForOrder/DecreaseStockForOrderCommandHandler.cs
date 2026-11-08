namespace ClothingStore.Catalog.Application.Features.Products.Commands.DecreaseStockForOrder;

public class DecreaseStockForOrderCommandHandler(
    IProcessedOrdersRepository processedOrdersRepository,
    IProductsRepository productsRepository,
    TimeProvider timeProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<DecreaseStockForOrderCommand, Unit>
{
    public async Task<Unit> Handle(DecreaseStockForOrderCommand command, CancellationToken cancellationToken)
    {
        if (await processedOrdersRepository.ExistsAsync(command.OrderId, cancellationToken)) return Unit.Value;

        var productIds = command.Items.Select(x => x.ProductId).ToList();

        var products = await productsRepository.GetByIdsAsync(productIds, cancellationToken);

        var productById = products.ToDictionary(x => x.Id);

        foreach (var item in command.Items)
        {
            if (!productById.TryGetValue(item.ProductId, out var product)) continue;
            product.DecreaseQuantityInStock(item.Quantity);
        }

        await processedOrdersRepository.AddAsync(ProcessedOrder.Create(command.OrderId, timeProvider.GetUtcNow().UtcDateTime), cancellationToken);

        await unitOfWork.CommitChangesAsync(cancellationToken);

        return Unit.Value;
    }
}