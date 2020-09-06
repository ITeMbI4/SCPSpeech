using System;
using HarmonyLib;
using UnityEngine;

namespace SCP_Speech
{
	[HarmonyPatch(typeof(Intercom), "ServerAllowToSpeak")]
	[HarmonyPatch(new Type[] { })]
	public class ServerAllowSpeakOverride
	{
		public static void Postfix(Intercom __instance, ref bool __result)
		{
			if (!SCPSpeech.s939I)
				return;

			CharacterClassManager ccm = __instance.GetComponent<CharacterClassManager>();

			if (!ccm.CurClass.Is939())
				return;

			__result = Vector3.Distance(__instance.transform.position, ccm.transform.position) < __instance.triggerDistance;
		}
	}
}
