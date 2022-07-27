using ExamApi.Models;
using ExamApi.Models.DTOs;
using FluentValidation;

namespace ExamApi.BusinessLogic.Validators;


public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(address => address.Id).NotNull();
        RuleFor(address => address.City).NotNull();
        RuleFor(address => address.Street).NotNull();
        RuleFor(address => address.House).NotNull();
    }
}