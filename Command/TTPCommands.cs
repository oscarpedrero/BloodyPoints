using Bloody.Core.API.v1;
using Bloody.Core.GameData.v1;
using Bloody.Core.Methods;
using Bloody.Core.Models.v1;
using BloodyPoints.Helpers;
using ProjectM;
using Stunlock.Core;
using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using VampireCommandFramework;

namespace BloodyPoints.Command
{
    [CommandGroup(name: "bloodypoint", shortHand: "blp")]
    internal class TTPCommands
    {

        private static Dictionary<string, string> TeleportsRequest = new Dictionary<string, string>();

        [Command(name: "teleportoplayer", shortHand: "tlp", adminOnly: false, usage: "<PlayerName>", description: "Teleports you to the specific player.")]
        public static void TeleportToPlayerCommand(ChatCommandContext ctx, string name)
        {
            try
            {
                UserModel Target = GameData.Users.GetUserByCharacterName(name);
                UserModel Owner = GameData.Users.GetUserByCharacterName(ctx.User.CharacterName.Value);
                if (Plugin.RequestTeleportPlayer.Value)
                {
                    if (RequestTeleportToPlayer(Owner, Target, out string message))
                    {
                        ctx.Reply(message);
                    }
                    else
                    {
                        throw ctx.Error(message);
                    }

                } else
                {
                    if (Plugin.Cost.Value)
                    {
                        if (RetriveItemsFromInventory(Owner, out string messageItem))
                        {
                            float3 location = Plugin.SystemsCore.EntityManager.GetComponentData<LocalToWorld>(Target.Character.Entity).Position;
                            Helper.TeleportTo(Owner.Entity, Owner.Character.Entity, location);
                            ctx.Reply($"Successfully performed teleport towards the player {name}");
                        }
                        else
                        {
                            throw ctx.Error(messageItem);
                        }
                    } else
                    {
                        float3 location = Plugin.SystemsCore.EntityManager.GetComponentData<LocalToWorld>(Target.Character.Entity).Position;
                        Helper.TeleportTo(Owner.Entity, Owner.Character.Entity, location);
                        ctx.Reply($"Successfully performed teleport towards the player {name}");
                    }
                }

            } catch (Exception e)
            {
                throw ctx.Error(e.Message);
            }
            
        }

        [Command(name: "teleportaccept", shortHand: "tla", adminOnly: false, usage: "<PlayerName>", description: "Accept teleport request from a player.")]
        public static void AceptTeleportCommand(ChatCommandContext ctx, string name)
        {
            try
            {
                UserModel Target = GameData.Users.GetUserByCharacterName(ctx.User.CharacterName.Value);
                UserModel Owner = GameData.Users.GetUserByCharacterName(name);

                if (!TeleportsRequest.TryGetValue(Owner.CharacterName, out string value) && value != Target.CharacterName)
                {
                    throw ctx.Error($"You do not have any teleport requests from {FontColorChatSystem.White($"{name}")}");
                }

                if (Plugin.Cost.Value)
                {
                    if (RetriveItemsFromInventory(Owner, out string message))
                    {
                        float3 location = Plugin.SystemsCore.EntityManager.GetComponentData<LocalToWorld>(Target.Character.Entity).Position;
                        Helper.TeleportTo(Owner.Entity, Owner.Character.Entity, location);
                        ctx.Reply($"Successfully teleport the {FontColorChatSystem.White($"{name}")} player to your position.");
                    }
                    else
                    {
                        throw ctx.Error(message);
                    }
                } else
                {
                    TeleportsRequest.Remove(Owner.CharacterName);
                    float3 location = Plugin.SystemsCore.EntityManager.GetComponentData<LocalToWorld>(Target.Character.Entity).Position;
                    Helper.TeleportTo(Owner.Entity, Owner.Character.Entity, location);
                    ctx.Reply($"Successfully teleport the {FontColorChatSystem.White($"{name}")} player to your position.");
                }
            }
            catch (Exception e)
            {
                throw ctx.Error(e.Message);
            }

        }
        internal static bool RequestTeleport(UserModel playerRequest, UserModel playerTarget, out string message)
        {
            if (TeleportsRequest.TryGetValue(playerRequest.CharacterName, out string playerTargetDic))
            {
                if (playerTargetDic == playerTarget.CharacterName)
                {
                    message = $"You already have an active request for the player {FontColorChatSystem.White($"{playerTarget.CharacterName}")}.";
                    return false;
                }
                else
                {
                    TeleportsRequest.Remove(playerRequest.CharacterName);
                }
            }

            TeleportsRequest.Add(playerRequest.CharacterName, playerTarget.CharacterName);

            ServerChatUtils.SendSystemMessageToClient(Plugin.SystemsCore.EntityManager, (ProjectM.Network.User)playerTarget.Internals.User, $"{FontColorChatSystem.White($"{playerRequest.CharacterName}")} has asked you to teleport to your position. If you want to accept it write {FontColorChatSystem.White($".blp tpa {playerRequest.CharacterName}")}");

            message = $"The teleport request has been successfully sent to player {FontColorChatSystem.White($"{playerTarget.CharacterName}")}";
            return true;

        }

        internal static bool RequestTeleportToPlayer(UserModel playerRequest, UserModel playerTarget, out string message)
        {
            try
            {
                if (RequestTeleport(playerRequest, playerTarget, out string messageRequest))
                {
                    message = messageRequest;
                    return true;
                }
                else
                {
                    message = messageRequest;
                    return false;
                }
            } catch (Exception e)
            {
                message = e.Message;
                return false;
            }
            
        }
        
        internal static bool RetriveItemsFromInventory(UserModel playerRequest, out string message)
        {
            try
            {

                var prefabGameData = GameData.Items.GetPrefabById(new PrefabGUID(Plugin.PrefabGUID.Value));

                var userEntity = playerRequest.Character.Entity;

                int totalSlots = InventoryUtilities.GetInventorySize(Plugin.SystemsCore.EntityManager, userEntity);

                var gameDataSystem = Plugin.SystemsCore.GameDataSystem;

                var totalItemsRemove = Plugin.Amount.Value;

                for (int i = 0; i < totalSlots; i++)
                {

                    if (InventoryUtilities.TryGetItemAtSlot(Plugin.SystemsCore.EntityManager, userEntity, i, out var item))
                    {
                        var itemData = GameData.Items.GetPrefabById(item.ItemType);

                        if (itemData != null)
                        {
                            if (itemData.PrefabName == prefabGameData.PrefabName)
                            {
                                if (item.Amount >= totalItemsRemove)
                                {
                                    InventoryUtilitiesServer.TryRemoveItemAtIndex(Plugin.SystemsCore.EntityManager, userEntity, item.ItemType, totalItemsRemove, i, false);
                                    totalItemsRemove = 0;
                                    break;
                                }
                                else if (item.Amount < totalItemsRemove)
                                {
                                    InventoryUtilitiesServer.TryRemoveItemAtIndex(Plugin.SystemsCore.EntityManager, userEntity, item.ItemType, item.Amount, i, true);
                                    totalItemsRemove -= item.Amount;
                                }

                                if (totalItemsRemove == 0)
                                {
                                    break;
                                }

                            }
                        }
                    }
                }
                if (totalItemsRemove > 0)
                {
                    message = $"You do not have the necessary amount of items in your inventory to perform this action";
                    AdditemToInventory(playerRequest.CharacterName, prefabGameData.PrefabGUID, Plugin.Amount.Value - totalItemsRemove);
                    return false;
                }

                message = $"";
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }

        public static bool AdditemToInventory(string characterName, PrefabGUID prefabGUID, int quantity)
        {

            try
            {

                var user = GameData.Users.GetUserByCharacterName(characterName);


                var hasAdded = user.TryGiveItem(prefabGUID, quantity, out Entity itemEntity);
                if (!hasAdded)
                {
                    user.DropItemNearby(prefabGUID, quantity);
                }


                return true;

            }
            catch (Exception error)
            {
                Plugin.Logger.LogError($"Error {error.Message}");
                return false;
            }

        }
    }
}
