namespace ClothingStore.Shared.Infrastructure.Persistence.Outbox;

public class OutboxMessage
{
    private OutboxMessage()
    {
    }

    public Guid Id { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    public int RetryCount { get; private set; }
    public string? Error { get; private set; }

    public static OutboxMessage Create(IIntegrationEvent integrationEvent, DateTime now)
    {
        return new OutboxMessage
        {
            Id = integrationEvent.MessageId,
            Type = integrationEvent.GetType().AssemblyQualifiedName!,
            Content = JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType()),
            CreatedAt = now
        };
    }

    public void MarkProcessed(DateTime now)
    {
        ProcessedAt = now;
        Error = null;
    }

    public void MarkFailed(string error)
    {
        RetryCount++;
        Error = error;
    }
}