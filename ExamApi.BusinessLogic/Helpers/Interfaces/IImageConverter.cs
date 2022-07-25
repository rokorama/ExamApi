using ExamApi.Models.UploadRequests;

namespace ExamApi.BusinessLogic.Helpers;

public interface IImageConverter
{
    public byte[] ConvertImage(ImageUploadRequest imageUploadRequest);
}