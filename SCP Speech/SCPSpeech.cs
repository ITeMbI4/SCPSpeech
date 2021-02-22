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
        version = "1.2",
        SmodMajor = 3,
        SmodMinor = 9,
        SmodRevision = 9
        )]
    public class SCPSpeech : Plugin
    {
        public static SCPSpeech plugin;
        public Harmony Instance;

        public static bool Enabled;
        public static bool s939I;
        public static bool s049;
        public static bool s0492;
        public static bool s096;
        public static bool s106;
        public static bool s173;

        public override void OnEnable() 
        {
            Instance = new Harmony("SCPSP_TeMbI4");
            Instance.PatchAll(); 
            this.Info("SCP Speech has loaded :D"); 
        }

        public override void OnDisable() 
        { 
            Instance.UnpatchAll(); 
            this.Info("SCP Speech has been unloaded"); 
        }

        public override void Register()
        {
            this.AddEventHandlers(new EventHandlers(this));

            this.AddConfig(new Smod2.Config.ConfigSetting("sp_enabled", true, true, "Is plugin enabled?"));
            this.AddConfig(new Smod2.Config.ConfigSetting("sp_939intercom", true, true, "Can SCP-939 use Intercom?"));
            this.AddConfig(new Smod2.Config.ConfigSetting("sp_049", true, true, "Can SCP-049 speak?"));
            this.AddConfig(new Smod2.Config.ConfigSetting("sp_0492", false, true, "Can SCP-049-2 speak?"));
            this.AddConfig(new Smod2.Config.ConfigSetting("sp_096", false, true, "Can SCP-096 speak?"));
            this.AddConfig(new Smod2.Config.ConfigSetting("sp_106", false, true, "Can SCP-106 speak?"));
            this.AddConfig(new Smod2.Config.ConfigSetting("sp_173", false, true, "Can SCP-173 speak?"));

            Enabled = this.GetConfigBool("sp_enabled");
            s939I = this.GetConfigBool("sp_939intercom");
            s049 = this.GetConfigBool("sp_049");
            s0492 = this.GetConfigBool("sp_0492");
            s096 = this.GetConfigBool("sp_096");
            s173 = this.GetConfigBool("sp_173");
            s106 = this.GetConfigBool("sp_106");
        }
    }
}
