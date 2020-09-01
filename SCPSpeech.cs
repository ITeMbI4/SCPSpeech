using Smod2;
using Smod2.Attributes;

namespace SCP_Speech
{
    [PluginDetails(
        author = "TeMbI4",
        name = "SCP Speech",
        description = "Allow certain SCP to speak with other classes.",
        id = "TeMbI4.SCP.Speech",
        version = "1.0",
        SmodMajor = 3,
        SmodMinor = 8,
        SmodRevision = 4
        )]
    public class SCPSpeech : Plugin
    {
        public static SCPSpeech plugin;

        public override void OnEnable() { this.Info("SCP Speech has loaded :D"); }
        public override void OnDisable() { this.Info("SCP Speech has been unloaded"); }

        public override void Register()
        {
            this.AddEventHandlers(new EventHandler(this));

            this.AddConfig(new Smod2.Config.ConfigSetting("sp_disabled", false, true, "Is plugin enabled?"));
        }
    }
}
