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
using Unity.Entities;
using System;
using Bloody.Core;
using Bloody.Core.API.v1;

namespace BloodyPoints
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("gg.deca.Bloodstone")]
    [BepInDependency("gg.deca.VampireCommandFramework")]
    [BepInDependency("trodi.Bloody.Core")]
    [Bloodstone.API.Reloadable]
    public class Plugin : BasePlugin, IRunOnInitialized
    {

        public static Harmony harmony;

        internal static Plugin Instance { get; private set; }

        public static readonly string ConfigPath = Path.Combine(Paths.ConfigPath, "BloodyPoints");
        public static readonly string WaypointsJson = Path.Combine(ConfigPath, "waypoints.json");
        public static readonly string GlobalWaypointsJson = Path.Combine(ConfigPath, "global_waypoints.json");
        public static readonly string TotalWaypointsJson = Path.Combine(ConfigPath, "total_waypoints.json");

        private static ConfigEntry<int> WaypointLimit;

        public static ConfigEntry<bool> DraculaRoom { get; private set; }

        public static ManualLogSource Logger;

        public override void Load()
        {

            Instance = this;
            Logger = Log;
            CommandRegistry.RegisterAll();
            harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            EventsHandlerSystem.OnInitialize += GameDataOnInitialize;
            EventsHandlerSystem.OnDestroy += GameDataOnDestroy;
            EventsHandlerSystem.OnSaveWorld += SaveWorldInvoke;

            if (!Core.IsServer)
            {
                Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is only for server!");
                return;
            }

            InitConfig();
            
            // Plugin startup logic
            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void SaveWorldInvoke()
        {
            Bloodypoint.SaveWaypoints();
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
            DraculaRoom = Config.Bind("Config", "Dracula Room", false, "Allows you to create waypoints in Dracula's room.");

            if (!Directory.Exists(ConfigPath)) Directory.CreateDirectory(ConfigPath);
        }

        public static void Initialize()
        {
            Bloodypoint.WaypointLimit = WaypointLimit.Value;
            Bloodypoint.DraculaRoom = DraculaRoom.Value;
        }

        public override bool Unload()
        {
            CommandRegistry.UnregisterAssembly();
            Bloodypoint.SaveWaypoints();
            Config.Clear();
            harmony.UnpatchSelf();
            EventsHandlerSystem.OnInitialize -= GameDataOnInitialize;
            EventsHandlerSystem.OnDestroy -= GameDataOnDestroy;
            EventsHandlerSystem.OnSaveWorld -= SaveWorldInvoke;
            return true;
        }

        public void OnGameInitialized()
        {
            
        }
    }
}
