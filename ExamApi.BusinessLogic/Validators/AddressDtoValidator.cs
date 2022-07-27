using ExamApi.Models.DTOs;
using FluentValidation;

namespace ExamApi.BusinessLogic.Validators;


public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(addressDto => addressDto.City).NotNull();
        RuleFor(addressDto => addressDto.Street).NotNull();
        RuleFor(addressDto => addressDto.House).NotNull();
    }
}