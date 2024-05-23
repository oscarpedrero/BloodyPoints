# BloodyPoints Mod so that personal or global waypoints can be created

If you had the previous version installed, you must delete the configuration files for the server and restart the server.

## Requirements:

For the correct functioning of this mod you must have the following dependencies installed on your server:

1. [BepInEx](https://github.com/BepInEx/BepInEx)
2. [VampireCommandFramework](https://github.com/decaprime/VampireCommandFramework)
3. [Bloodstone](https://github.com/decaprime/Bloodstone)
3. [Bloody.Core](https://github.com/oscarpedrero/BloodyCore)


<details>
<summary>Changelog</summary>

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
|Config|`Waypoint Limit`            | Set a waypoint limit per user.s              | 1
 
 ```
 ## Settings file was created by plugin BloodyPoints v1.0.0
## Plugin GUID: BloodyPoints

[Config]

## Set a waypoint limit per user.
# Setting type: Int32
# Default value: 1
Waypoint Limit = 1
 ```

## Chat Commands

| COMMAND                                          |DESCRIPTION
|--------------------------------------------------|-------------------------------|
| `.help bp`                                   | Command that returns all available commands    
| `.bp wpg <Name>` (Only Admins)                   | Creates the specified global waypoint.
| `.bp rmg <Name>` (Only Admins)   | Removes the specified global waypoint.
| `.bp tpp <Name> <PlayerName/all>` (Only Admins)  | Teleports player to the specific waypoint. If we type "all" instead of the player's name it will teleport all online players to the specified point.
| `.bp wp <Name>`  | Creates the specified personal waypoint.
| `.bp rm <Name>`  | Removes the specified personal waypoint.
| `.bp tp <Name>`  | Teleports you to the specific waypoint.
| `.bp l `  | Lists waypoints available to you

## Credits

- [decaprime (Bloodstone & VCF)](https://github.com/decaprime) 
- [Kaltharos (RPGMods)](https://github.com/Kaltharos/RPGMods) 
- [adainrivers (VRising.GameData)](https://github.com/adainrivers/VRising.GameData)