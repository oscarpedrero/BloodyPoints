using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Bloodstone.API;
using System.IO;
using VampireCommandFramework;
using System.Reflection;
using BloodyPoints.Command;
using VRising.GameData;
using Unity.Entities;
using System;

namespace BloodyPoints
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("gg.deca.Bloodstone")]
    [BepInDependency("gg.deca.VampireCommandFramework")]
    public class Plugin : BasePlugin, IRunOnInitialized
    {

        public static Harmony harmony;

        internal static Plugin Instance { get; private set; }

        public static readonly string ConfigPath = Path.Combine(Paths.ConfigPath, "BloodyPoints");
        public static readonly string WaypointsJson = Path.Combine(ConfigPath, "waypoints.json");
        public static readonly string GlobalWaypointsJson = Path.Combine(ConfigPath, "global_waypoints.json");
        public static readonly string TotalWaypointsJson = Path.Combine(ConfigPath, "total_waypoints.json");

        private static ConfigEntry<int> WaypointLimit;

        public static ManualLogSource Logger;

        public override void Load()
        {
            
            Instance = this;
            Logger = Log;
            CommandRegistry.RegisterAll();
            harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            GameData.OnInitialize += GameDataOnInitialize;
            GameData.OnDestroy += GameDataOnDestroy;

            if (!VWorld.IsServer)
            {
                Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is only for server!");
                return;
            }

            InitConfig();
            
            // Plugin startup logic
            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void GameDataOnDestroy()
        {
            
        }

        private void GameDataOnInitialize(World world)
        {
            Initialize();
            Bloodypoint.LoadWaypoints();
        }

        public void InitConfig()
        {
            WaypointLimit = Config.Bind("Config", "Waypoint Limit", 1, "Set a waypoint limit per user.");

            if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);
        }

        public static void Initialize()
        {
            Bloodypoint.WaypointLimit = WaypointLimit.Value;
        }

        public override bool Unload()
        {
            Bloodypoint.SaveWaypoints();
            Config.Clear();
            harmony.UnpatchSelf();
            return true;
        }

        public void OnGameInitialized()
        {
            
        }
    }
}
