using Smod2;
using Smod2.Events;
using Smod2.EventHandlers;
using System.Collections.Generic;
using UnityEngine;
using Assets._Scripts.Dissonance;
using MEC;

namespace SCP_Speech
{
    public class EventHandler : IEventHandlerWaitingForPlayers, IEventHandlerSetRole
    {
        private readonly Plugin plugin;
        private static readonly Dictionary<GameObject, ReferenceHub> _hubs = new Dictionary<GameObject, ReferenceHub>();

        public EventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            if (plugin.GetConfigBool("sp_disabled"))
            {
                plugin.PluginManager.DisablePlugin(plugin);
            }
        }

        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            foreach (ReferenceHub hub in GetHubs())
            {
                DissonanceUserSetup speak = hub.gameObject.GetComponent<DissonanceUserSetup>();
                if (hub.characterClassManager.CurClass == RoleType.Scp049)
                {
                    speak.NetworkaltIsActive = false;
                    Timing.RunCoroutine(Speech(speak));
                }
            }
        }

        public IEnumerator<float> Speech(DissonanceUserSetup speak)
        {
            while (true)
            {
                if (speak.NetworkaltIsActive)
                {
                    speak.NetworkspeakingFlags = SpeakingFlags.MimicAs939;
                }
                else
                {
                    speak.NetworkspeakingFlags = SpeakingFlags.SCPChat;
                }

                yield return Timing.WaitForSeconds(0.1f);
            }
        }

        public static List<ReferenceHub> GetHubs()
        {
            List<ReferenceHub> hubs = new List<ReferenceHub>();
            foreach (GameObject obj in PlayerManager.players)
                if (_hubs.ContainsKey(obj))
                    hubs.Add(_hubs[obj]);
                else
                {
                    ReferenceHub rh = ReferenceHub.GetHub(obj);
                    _hubs.Add(obj, rh);
                    hubs.Add(rh);
                }

            return hubs;
        }
    }
}
