namespace ClothingStore.Identity.Application.Features.Commands.Logout;

public record LogoutCommand(string? RefreshToken) : ICommand<Unit>;