namespace ClothingStore.Identity.Application.Features.Queries.GetUserSummaryById;

public class GetUserSummaryByIdQueryHandler(IUsersRepository usersRepository) : IQueryHandler<GetUserSummaryByIdsQuery, List<UserSummaryResponse>>
{
    public async Task<List<UserSummaryResponse>> Handle(GetUserSummaryByIdsQuery query, CancellationToken cancellationToken)
    {
        return await usersRepository.GetUserSummaryByIdsAsync(query.UserIds, cancellationToken);
    }
}