namespace PokeSaveRomManager.Data.Domain
{
    [Flags]
    public enum Badges : byte
    {
        None = 0,
        Badge1 = 1 << 0,
        Badge2 = 1 << 1,
        Badge3 = 1 << 2,
        Badge4 = 1 << 3,
        Badge5 = 1 << 4,
        Badge6 = 1 << 5,
        Badge7 = 1 << 6,
        Badge8 = 1 << 7
    }
}