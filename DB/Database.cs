using Bloody.Core.Helper.v1;
using Stunlock.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BloodyPoints.DB
{
    public class Database
    {

        public static JsonSerializerOptions JSON_options = new()
        {
            WriteIndented = false,
            IncludeFields = false
        };

        public static JsonSerializerOptions Pretty_JSON_options = new()
        {
            WriteIndented = true,
            IncludeFields = true
        };

        public static List<WaypointData> globalWaypoint { get; set; }
        public static List<WaypointData> waypoints { get; set; }
        public static Dictionary<ulong, int> waypoints_owned { get; set; }
        public static Dictionary<ulong, DateTime> UsersCooldown { get; set; } = new();

        public static class Buff
        {
            public static PrefabGUID InCombat = Prefabs.Buff_InCombat;
            public static PrefabGUID InCombat_PvP = Prefabs.Buff_InCombat_PvPVampire;
        }

        internal static bool TryCoolDownTP(ulong steamid, out double diffInSeconds)
        {

            if (UsersCooldown.TryGetValue(steamid, out DateTime playerCoolDown))
            {
                diffInSeconds = (DateTime.Now - playerCoolDown ).TotalSeconds;
                if (diffInSeconds >= Plugin.CoolDown.Value)
                {
                    diffInSeconds = 0;
                    UsersCooldown[steamid] = DateTime.Now;
                    return true;
                }
                return false;
            } else
            {
                diffInSeconds = 0;
                UsersCooldown[steamid] = DateTime.Now;
                return true;
            }
            
        }

        
    }
}
