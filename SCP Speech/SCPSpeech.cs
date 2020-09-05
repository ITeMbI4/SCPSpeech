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

        public override void Register()
        {
            this.AddEventHandlers(new EventHandlers(this));

            this.AddConfig(new Smod2.Config.ConfigSetting("sp_disabled", false, true, "Is plugin enabled?"));
        }
    }
}
