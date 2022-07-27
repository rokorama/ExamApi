using ExamApi.Models.UploadRequests;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace ExamApi.BusinessLogic.Helpers;

public class ImageConverter : IImageConverter
{    
    public byte[] ConvertImage(ImageUploadRequest imageUploadRequest)
    {
        using var memoryStream = new MemoryStream();
        try
        {
            imageUploadRequest.Image!.CopyTo(memoryStream);
            return ResizeImage(memoryStream.ToArray());
        }
        catch (SixLabors.ImageSharp.UnknownImageFormatException)
        {
            return null!;
        }
    }

    public virtual byte[] ResizeImage(byte[] imageBytes)
    {
        using (Image image = Image.Load(imageBytes))
        {
            image.Mutate(x => x.Resize(200, 200));
            var outStream = new MemoryStream();
            image.Save(outStream, new JpegEncoder());
            return outStream.ToArray();
        }
    }
}