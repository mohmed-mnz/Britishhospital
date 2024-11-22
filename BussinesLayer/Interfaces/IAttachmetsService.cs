using Microsoft.AspNetCore.Http;
using SharedConfig;

namespace BussinesLayer.Interface;

public interface IAttachmetsService
{
    public AddAttachmentsResponse uploadImage(AssetInfo assetInfo);
    public string retriveImage(string dbPath, string featureDirctory);
    public bool DeleteFile(string plainPath, string featureDirectory);
    public Task<AddAttachmentsResponse> uploadFile(FileAssetInfo assetInfo, IFormFile file);
    Task<IFormFile> RetrieveFile(string filePath, string dbpath);
    Task<string> RetrieveFileAsBase64(string filePath, string dbpath);
    public AddAttachmentsResponse UploadFileAsBase64(AssetInfo assetInfo);
}
