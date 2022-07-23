using Microsoft.Extensions.DependencyInjection;

namespace ExamApi.UserAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserAccessServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}