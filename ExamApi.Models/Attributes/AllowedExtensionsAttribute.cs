using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ExamApi.Models.Attributes;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _allowedExtensions;
    public AllowedExtensionsAttribute(string[] allowedExtensions)
    {
        _allowedExtensions = allowedExtensions;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_allowedExtensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(GetErrorMessage());
            }
        }
        
        return ValidationResult.Success!;
    }

    public string GetErrorMessage()
    {
        return $"Files of this type are not allowed.";
    }
}