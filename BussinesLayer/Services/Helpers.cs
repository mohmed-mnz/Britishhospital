using HeyRed.Mime;
using System.Drawing;
using System.Drawing.Imaging;
namespace BussinesLayer.Services;

public static class Helpers
{
    public static Image Base64ToImage(string base64String)
    {
        // Convert base 64 string to byte[]


        byte[] imageBytes = Convert.FromBase64String(base64String);
        // Convert byte[] to Image
        using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
        {
            Image image = Image.FromStream(ms, true);
            return image;
        }

    }
    public static FileInfo Base64ToFile(string base64String, string fileType)
    {
        byte[] fileBytes = Convert.FromBase64String(base64String);

        using (MemoryStream ms = new MemoryStream(fileBytes))
        {
            string mimeType = "";

            switch (fileType.ToLower())
            {
                case "image":
                    mimeType = MimeTypesMap.GetMimeType(".png");
                    break;
                case "pdf":
                    mimeType = MimeTypesMap.GetMimeType(".pdf");
                    break;
                case "video":
                    mimeType = MimeTypesMap.GetMimeType(".mp4");
                    break;
                case "html":
                    mimeType = MimeTypesMap.GetMimeType(".html");
                    break;
                default:
                    mimeType = "application/octet-stream";
                    break;
            }

            string extension = MimeTypesMap.GetExtension(mimeType);

            string tempFileName = Path.GetTempFileName();
            string filePath = Path.ChangeExtension(tempFileName, extension);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                ms.CopyTo(fs);
            }

            return new FileInfo(filePath);
        }
    }





    public static string GetImageExtension(byte[] fileBytes)
    {
        using (var ms = new MemoryStream(fileBytes))
        {
            using (var image = Image.FromStream(ms))
            {
                var format = image.RawFormat;
                return ImageCodecInfo.GetImageEncoders()!
                    .FirstOrDefault(encoder => encoder.FormatID == format.Guid)!
                    ?.FilenameExtension.Split(';')[0]!;
            }
        }
    }




    public static string GetFileExtensionFromBytes(byte[] fileBytes, string fileType)
    {
        string mimeType = "";

        switch (fileType)
        {
            case "html":
            case "mht":
                mimeType = MimeTypesMap.GetMimeType(".html");
                break;
            case "mp4":
                using (MemoryStream ms = new MemoryStream(fileBytes))
                {
                    mimeType = MimeTypesMap.GetMimeType(Path.GetExtension(".mp4"));
                }
                break;
            case "pdf":
                using (MemoryStream ms = new MemoryStream(fileBytes))
                {
                    mimeType = MimeTypesMap.GetMimeType(Path.GetExtension(".pdf"));
                }
                break;
            default:
                break;
        }

        return MimeTypesMap.GetExtension(mimeType);
    }


    public static (bool, string) SaveImage(string ImgStr, string ImgName)
    {
        Image image = Base64ToImage(ImgStr);
        String path = ""; //Path

        //Check if directory exist
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path); //Create directory if it doesn't exist
        }

        string imageName = ImgName + ".jpg";

        //set the image path
        string imgPath = Path.Combine(path, imageName);

        try
        {
            image.Save(imgPath, ImageFormat.Jpeg);
            return (true, imageName);

        }
        catch (Exception)
        {
            return (false, "failed");
        }



    }
}
