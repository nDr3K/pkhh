namespace PokeSaveRomManager.Parser.Core.Models
{
    public class RomInfo
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string GameVersion => Version?.ToLowerInvariant();
        public int Generation { get; set; }
        public string Region { get; set; }
        public string Checksum { get; set; }
        public long FileSize { get; set; }
        public bool IsValid { get; set; }

        public RomInfo Clone()
        {
            return new RomInfo
            {
                Title = this.Title,
                Version = this.Version,
                Generation = this.Generation,
                Region = this.Region,
                Checksum = this.Checksum,
                FileSize = this.FileSize,
                IsValid = this.IsValid
            };
        }

        public override string ToString()
        {
            return $"{Title} ({Version}) - Gen {Generation}, {Region}, {FileSize} bytes";
        }
    }
}
