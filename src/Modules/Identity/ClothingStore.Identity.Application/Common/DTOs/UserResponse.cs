namespace ClothingStore.Identity.Application.Common.DTOs;

public record UserResponse(Guid Id, string FullName, string Mobile, List<string> Roles);