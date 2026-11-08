namespace ClothingStore.Identity.Contracts.Features.Queries.GetUserSummaryById;

public record GetUserSummaryByIdsQuery(List<Guid> UserIds) : IQuery<List<UserSummaryResponse>>;