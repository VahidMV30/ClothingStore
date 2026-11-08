namespace ClothingStore.Shared.Contracts.Interfaces;

public interface IIntegrationEvent
{
    Guid MessageId { get; }
}