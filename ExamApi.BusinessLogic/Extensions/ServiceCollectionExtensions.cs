using ExamApi.BusinessLogic.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace ExamApi.BusinessLogic.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonalInfoService, PersonalInfoService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IObjectMapper, ObjectMapper>();
        services.AddScoped<IImageConverter, ImageConverter>();
        services.AddScoped<IPropertyChanger, PropertyChanger>();

        return services;
    }
}