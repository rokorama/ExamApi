using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace ExamApi.Models;

public static class ImageConverter
{    
    public static byte[] ConvertImage(ImageUploadRequest imageUploadRequest)
    {
        using var memoryStream = new MemoryStream();
        imageUploadRequest.Image.CopyTo(memoryStream);
        return ResizeImage(memoryStream.ToArray());
    }

    private static byte[] ResizeImage(byte[] imageBytes)
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