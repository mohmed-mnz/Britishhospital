using BussinesLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using SharedConfig;

namespace BussinesLayer.Services;

public class ArchiveService : IArchiveServices
{
    private readonly IConfiguration _configuration;
    ArchivingConfig ArchivingConfig { get; set; }

    public ArchiveService(IConfiguration configuration)
    {
        _configuration = configuration;
        ArchivingConfig = _configuration.Get<AppConfiguration>()!.ArchivingConfig!;
    }

    public void CreateFTPDirectoryIfNotExists(string directory)
    {
        if (!Directory.Exists($@"{ArchivingConfig.ArchivePath}\{directory}"))
            Directory.CreateDirectory($@"{ArchivingConfig.ArchivePath}\{directory}");
    }

    public string getPath(string featureDir)
    {
        string directoryPath = Path.Combine(ArchivingConfig.ArchivePath!, featureDir);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        return directoryPath;
    }

    public bool Upload(byte[] Bytes, string Path, bool Override = true)
    {
        File.WriteAllBytes($@"{ArchivingConfig.ArchivePath}\{Path}", Bytes);
        return true;
    }

    public bool Download(string PlainPath, ref byte[] Bytes, ref string Format)
    {
        if (File.Exists($@"{ArchivingConfig.ArchivePath}\{PlainPath}"))
        {
            Format = Path.GetExtension(PlainPath).Replace(".", string.Empty);
            Bytes = File.ReadAllBytes($@"{ArchivingConfig.ArchivePath}\{PlainPath}");
            return true;
        }
        return false;
    }

    //public string GenerateSecurePath(string Path)
    //{
    //    return Cryptography.Sign(Path);
    //}

    public class FileData
    {
        public string FilePath { get; set; } = string.Empty; 
        public string FileName { get; set; } = string.Empty;
        public byte[] FileBytes { get; set; } = Array.Empty<byte>(); 
        public string exe { get; set; } = string.Empty; 
    }

    public class FileUploaded
    {
        public int ChildId { get; set; }
        public byte[] Bytes { get; set; } = Array.Empty<byte>(); 
        public string Extension { get; set; } = string.Empty; 
    }
}
