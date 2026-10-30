namespace ClothingStore.Identity.Application.Common.Messages;

public static class AuthMessages
{
    public const string AlreadyRegistered = "شما قبلا ثبت نام کرده اید. لطفا وارد شوید.";
    public const string InvalidCredentials = "شماره موبایل یا گذرواژه صحیح نیست.";
    public const string InvalidRefreshToken = "نشست شما معتبر نیست یا منقضی شده است. لطفا دوباره وارد شوید.";
    public const string UserNotFound = "حساب کاربری یافت نشد.";
    public const string Unauthorized = "برای دسترسی به این بخش لطفا وارد حساب کاربری خود شوید.";
}