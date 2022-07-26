using ExamApi.Models.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ExamApi.Models.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddModelServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<PersonalInfoUploadRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<AddressDtoValidator>();

        return services;
    }
}