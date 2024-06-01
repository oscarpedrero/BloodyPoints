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
using Stunlock.Core;
using static Il2CppSystem.Net.Http.Headers.Parser;

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

        public static ConfigEntry<bool> InCombat { get; private set; }
        public static ConfigEntry<bool> DraculaRoom { get; private set; }
        public static ConfigEntry<int> CoolDown { get; private set; }
        public static ConfigEntry<bool> Cost { get; private set; }
        public static ConfigEntry<int> PrefabGUID { get; private set; }
        public static ConfigEntry<int> Amount { get; private set; }
        public static ConfigEntry<bool> TeleportPlayer { get; private set; }
        public static ConfigEntry<bool> RequestTeleportPlayer { get; private set; }

        public static Bloody.Core.Helper.v1.Logger Logger;

        public static SystemsCore SystemsCore;

        public override void Load()
        {

            Instance = this;
            Logger = new(Log);
            CommandRegistry.RegisterCommandType(typeof(Bloodypoint));
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
            SystemsCore = Core.SystemsCore;
            Initialize();
            Bloodypoint.LoadWaypoints();
            if (TeleportPlayer.Value)
            {
                CommandRegistry.RegisterCommandType(typeof(TTPCommands));
            }
        }

        public void InitConfig()
        {
            WaypointLimit = Config.Bind("Config", "Waypoint Limit", 1, "Set a waypoint limit per user.");
            InCombat = Config.Bind("Config", "In Combat", true, "Allows tp to be used when a player is in combat");
            DraculaRoom = Config.Bind("Config", "Dracula Room", false, "Allows you to create waypoints or tp from Dracula's room");
            CoolDown = Config.Bind("Config", "CoolDown", 20, "Time in seconds for teleportation to cooldown");
            Cost = Config.Bind("Config", "Cost", true, "Activate cost to make tp");
            PrefabGUID = Config.Bind("Config", "PrefabGUID", 862477668, "PrefabGUID that the player will have to have in the inventory to make tp");
            Amount = Config.Bind("Config", "Amount", 20, "Amount of PrefabGUID needed to make tp");
            TeleportPlayer = Config.Bind("Config", "TeleporPlayer", true, "Enable players to tp another player");
            RequestTeleportPlayer = Config.Bind("Config", "RequestTeleportPlayer", true, "Activate that players must accept a tp from another player who wants to tp their position.");

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
