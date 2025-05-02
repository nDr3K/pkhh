namespace PokeSaveRomManager.Parser.Registry
{
    public static class FileIdentifier
    {
        public static class GameVersion
        {
            public const string Gen1 = "Gen1";
            public const string Unknown = "Unknown";
        }
        public static string DetectRomGameVersion(byte[] data)
        {

            if (data.Length > 0x134 && data[0x134] == (byte)'P') return GameVersion.Gen1;

            return "Unknown";
        }

        public static string DetectSaveGameVersion(byte[] data)
        {
            return data.Length switch
            {
                0x8000 => GameVersion.Gen1,
                _ => GameVersion.Unknown
            };
        }

        public static bool IsSaveFile(byte[] data)
        {
            return data.Length is 0x8000 or 0x20000;
        }
    }
}