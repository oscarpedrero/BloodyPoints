# BloodyPoints Mod so that personal or global waypoints can be created

If you had the previous version installed, you must delete the configuration files for the server and restart the server.

# Support this project

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/K3K8ENRQY)

## Requirements:

For the correct functioning of this mod you must have the following dependencies installed on your server:

1. [BepInEx](https://thunderstore.io/c/v-rising/p/BepInEx/BepInExPack_V_Rising/)
2. [Bloodstone](https://thunderstore.io/c/v-rising/p/deca/Bloodstone/)
3. [VampireCommandFramework](https://thunderstore.io/c/v-rising/p/deca/VampireCommandFramework/)
4. [Bloody.Core](https://thunderstore.io/c/v-rising/p/Trodi/BloodyCore/)


<details>
<summary>Changelog</summary>

`2.0.3`
- Added option to the configuration file to prevent tp when a player is in combat
- Added option in the configuration file so that players have a cooldown time between tp
- Changed the short command from .bp to .blp to avoid incompatibility with other mods.

`2.0.2`
- Bloody.Core dependency removed as dll and added as framework
- Added parameter in the mod configuration to avoid creating or teleporting to Dracula's room

`2.0.0`
- Updated to VRISING 1.0

`1.0.2`
- Fixed README

`1.0.2`
- Fix problem with config files when init first time mod

`1.0.0`
- First Release


</details>

## Configuration

Once the mod is installed, a configuration file will be created in the server folder \BepInEx\config where you can configure the maximum number of waypoints per user allowed

**BloodyPoints.cfg**

|SECTION|PARAM| DESCRIPTION                                                     | DEFAULT
|----------------|-------------------------------|-----------------------------------------------------------------|-----------------------------|
|Config|`Waypoint Limit`            | Set a waypoint limit per user              | 1
|Config|`In Combat`            | Allows tp to be used when a player is in combat             | true
|Config|`Dracula Room`            | Allows you to create waypoints or tp from Dracula's room              | false
|Config|`CoolDown`            | Time in seconds for teleportation to cooldown              | 20
 
 ```
## Settings file was created by plugin BloodyPoints v2.0.2
## Plugin GUID: BloodyPoints

[Config]

## Set a waypoint limit per user.
# Setting type: Int32
# Default value: 1
Waypoint Limit = 1

## Allows you to uses tp when in combat 
# Setting type: Boolean
# Default value: true
In Combat = true

## Allows you to create waypoints or tp from Dracula's room
# Setting type: Boolean
# Default value: false
Dracula Room = false

## Time in seconds for teleportation to cooldown
# Setting type: Int32
# Default value: 20
CoolDown = 20

 ```

## Chat Commands

| COMMAND                                          |DESCRIPTION
|--------------------------------------------------|-------------------------------|
| `.help bloodypoint`                                   | Command that returns all available commands    
| `.blp wpg <Name>` (Only Admins)                   | Creates the specified global waypoint.
| `.blp rmg <Name>` (Only Admins)   | Removes the specified global waypoint.
| `.blp tpp <Name> <PlayerName/all>` (Only Admins)  | Teleports player to the specific waypoint. If we type "all" instead of the player's name it will teleport all online players to the specified point.
| `.blp wp <Name>`  | Creates the specified personal waypoint.
| `.blp rm <Name>`  | Removes the specified personal waypoint.
| `.blp tp <Name>`  | Teleports you to the specific waypoint.
| `.blp l `  | Lists waypoints available to you

## Credits

- [decaprime (Bloodstone & VCF)](https://github.com/decaprime) 
- [Kaltharos (RPGMods)](https://github.com/Kaltharos/RPGMods) 
- [adainrivers (VRising.GameData)](https://github.com/adainrivers/VRising.GameData)
