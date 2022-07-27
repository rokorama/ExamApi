using ExamApi.BusinessLogic.Helpers;
using ExamApi.BusinessLogic.Validators;
using ExamApi.Models;
using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;
using FluentValidation;
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
        services.AddScoped<IValidator<PersonalInfoUploadRequest>, PersonalInfoUploadRequestValidator>();
        services.AddScoped<IValidator<PersonalInfo>, PersonalInfoValidator>();
        services.AddScoped<IValidator<AddressDto>, AddressDtoValidator>();
        services.AddScoped<IValidator<Address>, AddressValidator>();

        return services;
    }
}