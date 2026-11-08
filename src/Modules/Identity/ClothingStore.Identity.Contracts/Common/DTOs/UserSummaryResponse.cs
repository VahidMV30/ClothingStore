namespace ClothingStore.Identity.Contracts.Common.DTOs;

public record UserSummaryResponse(Guid UserId, string FullName, string Mobile);