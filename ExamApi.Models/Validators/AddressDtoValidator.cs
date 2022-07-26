using ExamApi.Models.DTOs;
using FluentValidation;

namespace ExamApi.Models.Validators;


public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(personalInfo => personalInfo.City).NotNull();
        RuleFor(personalInfo => personalInfo.Street).NotNull();
        RuleFor(personalInfo => personalInfo.House).NotNull();
    }
}