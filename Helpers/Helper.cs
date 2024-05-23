using Bloodstone.API;
using ProjectM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using BloodyPoints.DB;
using ProjectM.Network;
using Unity.Mathematics;
using VampireCommandFramework;

namespace BloodyPoints.Helpers
{
    
    internal class Helper
    {
        public static EntityManager entityManager = VWorld.Server.EntityManager;

        public static bool IsPlayerInCombat(Entity player)
        {
            return BuffUtility.HasBuff(entityManager, player, Database.Buff.InCombat) || BuffUtility.HasBuff(entityManager, player, Database.Buff.InCombat_PvP);
        }

        public static void TeleportTo(Entity userEntity, Entity characterEntity, float3 position)
        {
            var entity = entityManager.CreateEntity(
                    ComponentType.ReadWrite<FromCharacter>(),
                    ComponentType.ReadWrite<PlayerTeleportDebugEvent>()
                );

            entityManager.SetComponentData<FromCharacter>(entity, new()
            {
                User = userEntity,
                Character = characterEntity
            });

            entityManager.SetComponentData<PlayerTeleportDebugEvent>(entity, new()
            {
                Position = new float3(position.x, position.y, position.z),
                Target = PlayerTeleportDebugEvent.TeleportTarget.Self
            });
        }

        public static bool checkDracualaRoom(WaypointData wp)
        {
            if((wp.X == 18 || wp.X == 19 ) && wp.Y == 16)
            {
                return true;
            }

            if ((wp.Y == 1 || wp.Y == 2) && wp.X == 24)
            {
                return true;
            }

            return false;
        }
    }
}
