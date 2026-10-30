namespace ClothingStore.Identity.Domain.Entities;

public class RefreshToken : Aggregate<Guid>
{
    private RefreshToken()
    {
    }

    public Guid UserId { get; private set; }
    public string TokenHash { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    public bool IsRevoked => RevokedAt != null;

    public bool IsExpired(DateTime now)
    {
        return now >= ExpiresAt;
    }

    public bool IsActive(DateTime now)
    {
        return !IsRevoked && !IsExpired(now);
    }

    public static RefreshToken Create(Guid id, Guid userId, string tokenHash, DateTime now, TimeSpan lifetime)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(id, Guid.Empty);
        ArgumentOutOfRangeException.ThrowIfEqual(userId, Guid.Empty);
        ArgumentException.ThrowIfNullOrWhiteSpace(tokenHash);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(lifetime, TimeSpan.Zero);

        return new RefreshToken
        {
            Id = id,
            UserId = userId,
            TokenHash = tokenHash,
            ExpiresAt = now.Add(lifetime)
        };
    }

    public void Revoke(DateTime now)
    {
        if (IsRevoked) return;
        RevokedAt = now;
    }
}