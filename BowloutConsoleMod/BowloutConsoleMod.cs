using BowloutModManager.BowloutMod;
using BowloutModManager.BowloutMod.Interfaces;
using System;

namespace BowloutConsoleMod
{
    public class BowloutConsoleMod : IBowloutMod
    {
        public string Name => "Bowlout Console Mod";

        public Version Version => new Version(1, 0, 0, 0);

        public string Description => "An Error Console for Bowlout.";

        public IBowloutConfiguration Configuration { get; private set; }
        public BowloutConsoleModConfiguration SampleModConfiguration => (BowloutConsoleModConfiguration)Configuration;

        public void OnSetup()
        {
            Configuration = this.GetConfiguration<BowloutConsoleModConfiguration>() ?? new BowloutConsoleModConfiguration();
        }

        public void Dispose()
        {
            
        }

        public void OnEnable()
        {

        }

        public void OnDisable()
        {
            
        }

        public void OnUpdate()
        {

        }

        public void OnFixedUpdate()
        {
            
        }

        public void OnLateUpdate()
        {
            
        }
    }
}
