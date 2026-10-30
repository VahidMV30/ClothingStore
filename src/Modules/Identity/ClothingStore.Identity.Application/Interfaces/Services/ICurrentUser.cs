namespace ClothingStore.Identity.Application.Interfaces.Services;

public interface ICurrentUser
{
    Guid? Id { get; }
}