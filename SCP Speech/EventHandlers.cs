using Smod2;
using Smod2.Events;
using Smod2.EventHandlers;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using System.Reflection;
using System;

namespace SCP_Speech
{
    public class EventHandlers : IEventHandlerWaitingForPlayers, IEventHandlerRoundStart
    {
        private readonly Plugin plugin;
        private static Transform intercomeArea = null;
        private static readonly Dictionary<GameObject, ReferenceHub> _hubs = new Dictionary<GameObject, ReferenceHub>();
		public List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();

        public EventHandlers(Plugin plugin)
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

		public void OnRoundStart(RoundStartEvent ev)
		{
			Coroutines.Add(Timing.RunCoroutine(CheckFor939Intercom()));
			intercomeArea = null;
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

		public static Transform IntercomArea
		{
			get
			{
				if (intercomeArea == null)
					intercomeArea = typeof(Intercom).GetField("area", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(Intercom.host) as Transform;

				if (intercomeArea == null)
					throw new MissingFieldException("Field for intercom not found.");
				return intercomeArea;
			}
		}

		public IEnumerator<float> CheckFor939Intercom()
		{
			while (true)
			{
				yield return Timing.WaitForSeconds(0.1f);

				if (Intercom.host.speaker != null || Intercom.host.speaking)
					continue;

				foreach (ReferenceHub rh in GetHubs())
				{
					try
					{
						if (!rh.characterClassManager.CurClass.Is939())
							continue;

						GameObject player = rh.gameObject;
						Intercom intercom = player.GetComponent<Intercom>();
						Scp939PlayerScript script = player.GetComponent<Scp939PlayerScript>();

						if (Vector3.Distance(player.transform.position, IntercomArea.position) > intercom.triggerDistance)
							continue;

						if (!script.NetworkusingHumanChat)
							continue;

						Intercom.host.RequestTransmission(player);
					}
					catch (Exception e)
					{
						while (e != null)
						{
							plugin.Error(e.ToString());
							e = e.InnerException;
						}
					}
				}
			}
		}
	}
}
