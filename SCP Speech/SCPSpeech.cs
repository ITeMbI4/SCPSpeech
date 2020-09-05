using HarmonyLib;
using Smod2;
using Smod2.Attributes;

namespace SCP_Speech
{
    [PluginDetails(
        author = "TeMbI4",
        name = "SCP Speech",
        description = "Allow certain SCP to speak with other classes.",
        id = "TeMbI4.SCP.Speech",
        version = "1.1",
        SmodMajor = 3,
        SmodMinor = 8,
        SmodRevision = 4
        )]
    public class SCPSpeech : Plugin
    {
        public static SCPSpeech plugin;
        public Harmony Instance;

        public override void OnEnable() 
        {
            Instance = new Harmony("TeMbI4");
            Instance.PatchAll(); 
            this.Info("SCP Speech has loaded :D"); 
        }

        public override void OnDisable() 
        { 
            Instance.UnpatchAll(); 
            this.Info("SCP Speech has been unloaded"); 
        }
        public static bool s049;
        public static bool enabled;
        public static bool s0492;
        public static bool s096;
        public static bool s106;
        public static bool s173;
        public override void Register()
        {
            this.AddEventHandlers(new EventHandlers(this));
            this.AddConfig(new Smod2.Config.ConfigSetting("sp_enabled", true, true, "no description"));
            this.AddConfig(new Smod2.Config.ConfigSetting("sp_049", true, true, "no description"));
            this.AddConfig(new Smod2.Config.ConfigSetting("sp_0492", true, true, "no description"));
            this.AddConfig(new Smod2.Config.ConfigSetting("sp_096", true, true, "no description"));
            this.AddConfig(new Smod2.Config.ConfigSetting("sp_106", true, true, "no description"));
            this.AddConfig(new Smod2.Config.ConfigSetting("sp_173", true, true, "no description"));
            s049 = this.GetConfigBool("sp_049");
            enabled = this.GetConfigBool("sp_enabled");
            s0492 = this.GetConfigBool("sp_0492");
            s096 = this.GetConfigBool("sp_096");
            s173 = this.GetConfigBool("sp_173");
            s106 = this.GetConfigBool("sp_106");
        }
    }
}
