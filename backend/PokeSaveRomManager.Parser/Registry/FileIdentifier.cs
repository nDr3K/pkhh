using PokeSaveRomManager.Parser.Core.Utils;

namespace PokeSaveRomManager.Parser.Registry
{
    public static class FileIdentifier
    {
        public static class GameVersion
        {
            public const string Gen1 = "Gen1";
            public const string Gen2 = "Gen2";
            public const string Unknown = "Unknown";
        }
        public static string DetectRomGameVersion(byte[] data)
        {

            return data.Length switch
            {
                0x100000 => GameVersion.Gen1,
                0x200000 => GameVersion.Gen2,
                _ => GameVersion.Unknown,
            };
        }

        public static string DetectSaveGameVersion(byte[] data)
        {
            return data.Length switch
            {
                0x8000 => GameVersion.Gen1,
                0x8400 => GameVersion.Gen2,
                _ => GameVersion.Unknown
            };
        }

        public static bool IsSaveFile(byte[] data)
        {
            return data.Length is 0x8000 or 0x8400;
        }
    }
}