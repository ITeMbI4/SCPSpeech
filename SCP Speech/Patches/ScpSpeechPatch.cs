using Assets._Scripts.Dissonance;
using HarmonyLib;

namespace SCP_Speech
{
    [HarmonyPatch(typeof(DissonanceUserSetup), nameof(DissonanceUserSetup.CallCmdAltIsActive))]
    public class Scp049Speak
    {
        public static bool Prefix(DissonanceUserSetup __instance, bool value)
        {
            if (SCPSpeech.enabled)
            {
                CharacterClassManager ccm = __instance.gameObject.GetComponent<CharacterClassManager>();
                if (ccm.CurClass.Is939() || ccm.CurClass == RoleType.Scp049 && SCPSpeech.s049 || ccm.CurClass == RoleType.Scp0492 && SCPSpeech.s0492 || ccm.CurClass == RoleType.Scp079 && SCPSpeech.enabled || ccm.CurClass == RoleType.Scp096 && SCPSpeech.s096 || ccm.CurClass == RoleType.Scp106 && SCPSpeech.s106 || ccm.CurClass == RoleType.Scp173 && SCPSpeech.s173)
                {
                    __instance.MimicAs939 = value;
                }
                return true;
            }
            return false;
        }
    }
}
