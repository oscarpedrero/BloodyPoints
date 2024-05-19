using ProjectM;
using Stunlock.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

        public static class Buff
        {
            public static PrefabGUID InCombat = new PrefabGUID(581443919);
            public static PrefabGUID InCombat_PvP = new PrefabGUID(697095869);
        }
    }
}
