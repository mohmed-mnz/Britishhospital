using BussinesLayer.Interface;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using SharedConfig;

namespace BussinesLayer.Services;

public class AttachmetsService : IAttachmetsService
{
    private readonly IArchiveServices archiveServices;

    public AttachmetsService(IArchiveServices archiveServices)
    {
        this.archiveServices = archiveServices;
    }
    public bool DeleteFile(string plainPath, string featureDirectory)
    {
        try
        {
            string fullPath = $@"{archiveServices.getPath(Path.GetDirectoryName(featureDirectory)!)}\{Path.GetFileName(featureDirectory)}";

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting file: {ex.Message}");
            return false;
        }
    }



    public async Task<AddAttachmentsResponse> uploadFile(FileAssetInfo assetInfo, IFormFile file)
    {
        string filename = "";
        try
        {
            var extension = Path.GetExtension(file.FileName);
            filename = $"{assetInfo.Name}{extension}";

            var filePath = Path.Combine(archiveServices.getPath(assetInfo.FeatureDirectory), filename);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return new AddAttachmentsResponse() { dbPath = filename, success = true };
        }
        catch (Exception ex)
        {
            return new AddAttachmentsResponse() { dbPath = ex.Message.ToString(), success = false };
        }
    }

    public AddAttachmentsResponse uploadImage(AssetInfo assetInfo)
    {

        byte[] bytes = Convert.FromBase64String(assetInfo.buffer!);
        string extention = assetInfo.type == "img" ? Helpers.GetImageExtension(bytes).Split(".")[1] :
            assetInfo.type == "file" ? Helpers.GetFileExtensionFromBytes(bytes, "html") : "mht";

        string FullPath = Path.Combine(assetInfo.FeatureDirectory!, $"{assetInfo.Name}.{extention}");
        archiveServices.CreateFTPDirectoryIfNotExists(Path.GetDirectoryName(FullPath)!);
        if (archiveServices.Upload(bytes, FullPath, true))
        {
            return new AddAttachmentsResponse()
            {
                dbPath = $"{assetInfo.Name}.{extention}",
                success = true
            };
        }
        else
        {
            return null!;
        }

    }
    public async Task<IFormFile> RetrieveFile(string filePath, string dbpath)
    {
        try
        {
            var fullPath = Path.Combine(archiveServices.getPath(filePath), dbpath);
            if (File.Exists(fullPath))
            {

                byte[] fileBytes = await File.ReadAllBytesAsync(fullPath);
                IFormFile file = new FormFile(
                    baseStream: new MemoryStream(fileBytes),
                    baseStreamOffset: 0,
                    length: fileBytes.Length,
                name: Path.GetFileName(filePath),
                    fileName: Path.GetFileName(fullPath)
                );

                return file;
            }
            else
            {
                throw new FileNotFoundException("The specified file does not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving file: {ex.Message}");
            return null;
        }
    }


    #region Retriving Data
    public string retriveImage(string dbPath, string featureDirctory)
    {
        if (dbPath != null)
        {
            string extention = Path.GetExtension(dbPath);
            string fullPath = $"{archiveServices.getPath(featureDirctory)}/{dbPath} ";
            try
            {
                byte[] fileBytes = File.ReadAllBytes(fullPath) ?? null!;
                string base64String = Convert.ToBase64String(fileBytes!);
                var extention2 = extention.Replace(".", "");
                string base64Image = $"data:image/{extention2};base64,{base64String}";
                return base64Image;
            }
            catch (Exception )
            {
                return dbPath;
            }
        }
        else
        {
            return null!;
        }
    }
    public async Task<string> RetrieveFileAsBase64(string filePath, string dbpath)
    {
        try
        {
            var filetype = "";
            string extention = Path.GetExtension(dbpath).ToLower();
            string featureDir = archiveServices.getPath(filePath);
            var fullPath = $"{featureDir}/{dbpath}";
            if (File.Exists(fullPath))
            {

                switch (extention)
                {
                    case ".pdf":
                        filetype = "application";
                        break;
                    case ".mp4":
                        filetype = "video";
                        break;
                    case ".mp3":
                        filetype = "audio";
                        break;
                    case ".jpg":
                    case ".png":
                    case ".jpeg":
                    case ".gif":
                    case ".svg":
                        filetype = "image";
                        break;
                    case ".html":
                    case ".mht":
                        filetype = "html";
                        break;
                    default:
                        filetype = "application";
                        break;

                }



                byte[] fileBytes = await File.ReadAllBytesAsync(fullPath);
                try
                {
                    string base64String = Convert.ToBase64String(fileBytes);
                    var extention2 = extention.Replace(".", "");
                    string base64File = $"data:{filetype}/{extention2};base64,{base64String}";
                    return base64File;

                }
                catch (Exception )
                {
                    return dbpath;
                }

            }
            else
            {
                throw new FileNotFoundException("The specified file does not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving file: {ex.Message}");
            return dbpath;
        }
    }

    public AddAttachmentsResponse UploadFileAsBase64(AssetInfo assetInfo)
    {
        var base64 = assetInfo.buffer!.Split(',')[1];

        byte[] bytes = Convert.FromBase64String(base64);

        string extention = assetInfo.type!;
        string FullPath = $"{assetInfo.FeatureDirectory}/{assetInfo.Name}.{extention}";
        archiveServices.CreateFTPDirectoryIfNotExists(Path.GetDirectoryName(FullPath)!);
        if (archiveServices.Upload(bytes, FullPath, true))
        {
            return new AddAttachmentsResponse()
            {
                dbPath = $"{assetInfo.Name}.{extention}",
                success = true
            };
        }
        else
        {
            return null!;
        }
    }
    #endregion
}
