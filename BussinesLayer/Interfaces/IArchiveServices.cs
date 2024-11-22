namespace BussinesLayer.Interfaces;

public interface IArchiveServices
{
    bool Upload(byte[] Bytes, string Path, bool Override = true);
    bool Download(string Path, ref byte[] Bytes, ref string Format);
  //  string GenerateSecurePath(string Path);
    //   bool DownloadSecure(string SecurePath, ref byte[] Bytes, ref string Format);
    void CreateFTPDirectoryIfNotExists(string directory);

    public string getPath(string featureDir);
}
