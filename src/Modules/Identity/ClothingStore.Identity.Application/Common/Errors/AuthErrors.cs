namespace ClothingStore.Identity.Application.Common.Errors;

public static class AuthErrors
{
    public static Error AlreadyRegistered => Error.Conflict("Auth.AlreadyRegistered", AuthMessages.AlreadyRegistered);
    public static Error InvalidCredentials => Error.Unauthorized("Auth.InvalidCredentials", AuthMessages.InvalidCredentials);
    public static Error InvalidRefreshToken => Error.Unauthorized("Auth.InvalidRefreshToken", AuthMessages.InvalidRefreshToken);
    public static Error Unauthorized => Error.Unauthorized("Auth.Unauthorized", AuthMessages.Unauthorized);
    public static Error UserNotFound => Error.Unauthorized("Auth.UserNotFound", AuthMessages.UserNotFound);
}