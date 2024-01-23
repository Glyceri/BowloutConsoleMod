using BowloutModManager.BowloutMod.Interfaces;
using UnityEngine;

namespace BowloutConsoleMod
{
    public class BowloutConsoleModConfiguration : IBowloutConfiguration
    {
        public int Version { get => 1; set => _ = value; }

        [Range(10, 10000)]
        public int Backlog = 1000;
    }
}
