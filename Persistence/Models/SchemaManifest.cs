namespace Persistence.Models
{
    public class SchemaManifest
    {
        public string Schema { get; set; }
        public string Version { get; set; }
        public DateTime GeneratedAt { get; set; }
        public Dictionary<string, SchemaFile> Files { get; set; } = new Dictionary<string, SchemaFile>();
    }
}
