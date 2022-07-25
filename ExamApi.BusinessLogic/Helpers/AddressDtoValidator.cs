using ExamApi.Models.DTOs;
using ExamApi.Models.UploadRequests;
using FluentValidation;

namespace ExamApi.BusinessLogic.Helpers;

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(personalInfo => personalInfo.City).NotNull();
        RuleFor(personalInfo => personalInfo.Street).NotNull();
        RuleFor(personalInfo => personalInfo.House).NotNull();
    }
}