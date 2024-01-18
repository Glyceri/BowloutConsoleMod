using BowloutModManager.BowloutMod.Interfaces;

namespace BowloutConsoleMod
{
    public class BowloutConsoleModConfiguration : IBowloutConfiguration
    {
        public int Version { get => 1; set => _ = value; }
    }
}
