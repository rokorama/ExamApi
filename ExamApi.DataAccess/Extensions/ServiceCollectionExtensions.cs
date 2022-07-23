using Microsoft.Extensions.DependencyInjection;

namespace ExamApi.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();

        return services;
    }
}