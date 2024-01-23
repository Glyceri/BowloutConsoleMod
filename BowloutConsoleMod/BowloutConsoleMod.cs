using BowloutModManager;
using BowloutModManager.BowloutMod;
using BowloutModManager.BowloutMod.Interfaces;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.IO;

namespace BowloutConsoleMod
{
    public class BowloutConsoleMod : IBowloutMod
    {
        public static BowloutConsoleMod Instance;

        public string Name => "Bowlout Console Mod";

        public Version Version => new Version(1, 0, 0, 0);

        public string Description => "An Error Console for Bowlout.";

        public IBowloutConfiguration Configuration { get; private set; }
        public BowloutConsoleModConfiguration SampleModConfiguration => (BowloutConsoleModConfiguration)Configuration;

        public bool Enabled { get; set; }

        bool consoleOpen = false;

        public List<string> log = new List<string>();

        public Harmony harmony;

        public void OnSetup()
        {
            Instance = this;
            Configuration = this.GetConfiguration<BowloutConsoleModConfiguration>() ?? new BowloutConsoleModConfiguration();
            this.SaveConfiguration(Configuration);
            PrereadLines();

            try
            {
                harmony = new Harmony("com.Glyceri.BowloutConsoleMod");
                var original = typeof(BLogger).GetMethod(nameof(BLogger.WriteTextToLog), BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);
                var prefix = typeof(BowloutConsoleMod).GetMethod(nameof(Postfix), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance);

                if (original == null) BLogger.WriteLineToLog("OG IS NULL!");
                if (prefix == null) BLogger.WriteLineToLog("FIX IS NULL!");

                harmony.Patch(original, new HarmonyMethod(prefix));
            }catch(Exception e)
            {
                BLogger.WriteLineToLog(e.ToString());
            }

           
        }

        // This is so hacky!
        void PrereadLines() 
        {
            string logPath = BLogger.ModlogPath;
            if (!File.Exists(logPath)) return;

            try
            {
                string[] lines = File.ReadAllLines(logPath);
                foreach (string line in lines)
                {
                    log.Add(line);
                }
            }
            catch(Exception e)
            {
                BLogger.WriteLineToLog(e.Message);
            }
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
            if (Instance.SampleModConfiguration.Backlog <= 0)
                Instance.SampleModConfiguration.Backlog = 1;
        }

        public void OnFixedUpdate()
        {
            
        }

        public void OnLateUpdate()
        {

        }

        Vector2 scrollPos = Vector2.zero;

        public void OnGUI()
        {
            if (GUILayout.Button(consoleOpen ? "Close Console" : "Open Console")) consoleOpen = !consoleOpen;

            if (consoleOpen)
            {
                string t = string.Empty;
                for (int i = 0; i < log.Count; i++) 
                {
                    t += log[i]; 
                    if (!t.EndsWith("\n")) t += "\n";
                }
                GUIStyle style = new GUIStyle();
                float height = style.CalcHeight(new GUIContent(t), 600);
                if (height > 400) height = 400;
                GUILayout.BeginHorizontal();
                scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(600), GUILayout.Height(height));
                GUILayout.Label(t);
                GUILayout.EndScrollView();
                GUILayout.EndHorizontal();
                if (GUILayout.Button("Clear"))
                    log.Clear();
            }
        }

        static void Postfix(ref string logText)
        {
            Instance.log.Add(logText);
            while (Instance.log.Count >= Instance.SampleModConfiguration.Backlog)
            {
                Instance.log.RemoveAt(0);
            }
        }
    }
}
