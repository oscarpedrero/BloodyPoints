# BloodyPoints Mod so that personal or global waypoints can be created

If you had the previous version installed, you must delete the configuration files for the server and restart the server.

## NEW IN 3.0.4

Now you can configure that the tp have cooldown, that they cost a specific amount of an item and you can request to tp the position of another player.

Please check the mod settings and new commands for these features.

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

`2.0.4`
- Added the cost option to make tp and the configuration of the PrefabGUID and its quantity.
- Added the option to request a player to tp his position.

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
|Config|`Cost`            | Activate cost to make tp              | true
|Config|`PrefabGUID`            | PrefabGUID that the player will have to have in the inventory to make tp              | 862477668
|Config|`Amount`            | Amount of PrefabGUID needed to make tp              | 20
|Config|`TeleporPlayer`            | Enable players to tp another player             | true
|Config|`RequestTeleportPlayer`            | Activate that players must accept a tp from another player who wants to tp their position.              | true
 
 ```
## Settings file was created by plugin BloodyPoints v2.0.2
## Plugin GUID: BloodyPoints

[Config]

## Set a waypoint limit per user.
# Setting type: Int32
# Default value: 1
Waypoint Limit = 1

## Allows tp to be used when a player is in combat
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

## Activate cost to make tp
# Setting type: Boolean
# Default value: true
Cost = true

## PrefabGUID that the player will have to have in the inventory to make tp
# Setting type: Int32
# Default value: 862477668
PrefabGUID = 862477668

## Amount of PrefabGUID needed to make tp
# Setting type: Int32
# Default value: 20
Amount = 20

## Enable players to tp another player
# Setting type: Boolean
# Default value: true
TeleporPlayer = true

## Activate that players must accept a tp from another player who wants to tp their position.
# Setting type: Boolean
# Default value: true
RequestTeleportPlayer = true
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
| `.blp tlp <Name>`  | Teleports you to the specific player.
| `.blp tla <Name>`  | Accept teleport request from a player.
| `.blp l `  | Lists waypoints available to you

## Credits

- [decaprime (Bloodstone & VCF)](https://github.com/decaprime) 
- [Kaltharos (RPGMods)](https://github.com/Kaltharos/RPGMods) 
- [adainrivers (VRising.GameData)](https://github.com/adainrivers/VRising.GameData)
