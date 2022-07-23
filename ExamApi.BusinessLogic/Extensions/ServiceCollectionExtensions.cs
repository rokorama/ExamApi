using Microsoft.Extensions.DependencyInjection;

namespace ExamApi.BusinessLogic.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonalInfoService, PersonalInfoService>();
        services.AddScoped<IAddressService, AddressService>();

        return services;
    }
}