namespace PokeSaveRomManager.Parser.Core.Models.Data
{
    public class PlayerData
    {
        public string Name { get; set; }
        public GameTime GameTime { get; set; }
        public List<Badge> Badges { get; set; }
    }

    public class GameTime
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(0, Hours, Minutes, Seconds);
        }

        public override string ToString()
        {
            return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
        }
    }
}