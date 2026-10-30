namespace ClothingStore.Identity.Infrastructure.Persistence;

public class IdentityDataSeeder(IdentityDbContext dbContext, IConfiguration configuration, ISecurityService securityService) : IDataSeeder
{
    public async Task SeedAllAsync()
    {
        var adminRoleId = configuration.GetValue<Guid>("AuthSettings:Roles:Admin:Id")!;
        var userRoleId = configuration.GetValue<Guid>("AuthSettings:Roles:User:Id")!;

        if (!await dbContext.Roles.AnyAsync())
        {
            var adminRoleName = configuration.GetValue<string>("AuthSettings:Roles:Admin:Name")!;
            var userRoleName = configuration.GetValue<string>("AuthSettings:Roles:User:Name")!;

            var roles = new List<Role>
            {
                Role.Create(adminRoleId, adminRoleName),
                Role.Create(userRoleId, userRoleName)
            };

            await dbContext.Roles.AddRangeAsync(roles);

            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.Users.AnyAsync())
        {
            var userId = configuration.GetValue<Guid>("AuthSettings:AdminUser:Id");
            var fullName = configuration.GetValue<string>("AuthSettings:AdminUser:FullName")!;
            var mobile = MobileNumber.Create(configuration.GetValue<string>("AuthSettings:AdminUser:Mobile")!);
            var passwordHash = securityService.HashPassword(configuration.GetValue<string>("AuthSettings:AdminUser:Password")!);

            var adminUser = User.Create(userId, fullName, mobile, passwordHash);
            adminUser.AssignRole(Guid.NewGuid(), adminRoleId);
            adminUser.AssignRole(Guid.NewGuid(), userRoleId);

            await dbContext.Users.AddAsync(adminUser);

            await dbContext.SaveChangesAsync();
        }
    }
}