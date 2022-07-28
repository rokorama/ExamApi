using ExamApi.Models.Attributes;
using Microsoft.AspNetCore.Http;

namespace ExamApi.Models.UploadRequests;

public class ImageUploadRequest
{
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(new string[] {".png", ".jpg", ".jpeg"})]
    public IFormFile? Image { get; set; }
}