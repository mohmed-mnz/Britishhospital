namespace SharedConfig
{
    public class AppConfiguration
    {
        public DbConfig? DbConfig { get; set; }
        public JWT? Jwt { get; set; }
        public ArchivingConfig? ArchivingConfig { get; set; }

    }
}
