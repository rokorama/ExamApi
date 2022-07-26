using ExamApi.Models.UploadRequests;
using FluentValidation;

namespace ExamApi.Models.Validators;

public class PersonalInfoUploadRequestValidator : AbstractValidator<PersonalInfoUploadRequest>
{
    public PersonalInfoUploadRequestValidator()
    {
        RuleFor(personalInfo => personalInfo.FirstName).NotNull();
        RuleFor(personalInfo => personalInfo.LastName).NotNull();
        RuleFor(personalInfo => personalInfo.PersonalNumber.ToString()).NotNull().Length(11);
        RuleFor(personalInfo => personalInfo.Email).NotNull();
        RuleFor(personalInfo => personalInfo.ImageUpload).NotNull();
        RuleFor(personalInfo => personalInfo.Address!).SetValidator(new AddressDtoValidator());
    }
}