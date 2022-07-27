using ExamApi.Models;
using FluentValidation;

namespace ExamApi.BusinessLogic.Validators;

public class PersonalInfoValidator : AbstractValidator<PersonalInfo>
{
    public PersonalInfoValidator()
    {
        RuleFor(personalInfo => personalInfo.Id).NotNull();
        RuleFor(personalInfo => personalInfo.FirstName).NotNull();
        RuleFor(personalInfo => personalInfo.LastName).NotNull();
        RuleFor(personalInfo => personalInfo.PersonalNumber.ToString()).NotNull().Length(11);
        RuleFor(personalInfo => personalInfo.Email).NotNull();
        RuleFor(personalInfo => personalInfo.Photo).NotNull();
        RuleFor(personalInfo => personalInfo.Address).NotNull();
    }
}