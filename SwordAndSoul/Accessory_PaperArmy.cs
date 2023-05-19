using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;
using System.Collections;

namespace SwordAndSoul
{
    public class Accessory_PaperArmy : Player_Accessory
    {
        public int bonus;
        
        public int GetDurationCoefficient(float baseDuration)
        {
            return (int)(1f / baseDuration * bonus * 100f);
        }

        public override void SetPlayer(Player player)
        {
            base.SetPlayer(player);
            Creature creature = player?._creature;
            if (creature)
            {
                Status status = creature.status;
                if (status != null)
                {
                    status.offense += bonus;
                    status.maxOffense += bonus;
                    status.power += bonus;
                    status.defense += bonus;
                    status.critical += bonus;
                    status.toughness += bonus;
                    status.criticalDamageRate += bonus;
                    status.additionalTrueDamage += bonus;
                    status.block += bonus;
                    status.evasion += bonus;

                    status.burnDuration += GetDurationCoefficient(3f);
                    status.burnAdditionalDamage += bonus;
                    status.burnSpeed += bonus;
                    status.allowedOverlapBurn += bonus;

                    status.poisonDuration += GetDurationCoefficient(4f);
                    status.poisonAdditionalDamage += bonus;
                    status.poisonSpeed += bonus;
                    status.allowedOverlapPoison += bonus;

                    status.chillDuration += GetDurationCoefficient(5f);
                    status.chillDecreaseSpeed += bonus;

                    status.shockDuration += GetDurationCoefficient(5f);

                    status.stunDuration += GetDurationCoefficient(1f);

                    status.hp += bonus;
                    status.precision += bonus;
                }
                CharacterController2D characterController2D = creature._controller2D;
                if (characterController2D != null)
                {
                    characterController2D.speedBonus += bonus;
                }
                PlayerDashAttackModule playerDashAttackModule = creature.GetComponent<PlayerDashAttackModule>();
                if (playerDashAttackModule)
                {
                    playerDashAttackModule.damageRate += bonus / 100f;
                }
                creature.atkSpeedBonus += bonus;
                creature.reloadSpeedBonus += bonus;
                creature.activeSkillCoolDownBonus += bonus;
            }
        }

        public override void Destroy()
        {
            Creature creature = player?._creature;
            if (creature)
            {
                Status status = creature.status;
                if (status != null)
                {
                    status.offense -= bonus;
                    status.maxOffense -= bonus;
                    status.power -= bonus;
                    status.defense -= bonus;
                    status.critical -= bonus;
                    status.toughness -= bonus;
                    status.criticalDamageRate -= bonus;
                    status.additionalTrueDamage -= bonus;
                    status.block -= bonus;
                    status.evasion -= bonus;

                    status.burnDuration -= GetDurationCoefficient(3f);
                    status.burnAdditionalDamage -= bonus;
                    status.burnSpeed -= bonus;
                    status.allowedOverlapBurn -= bonus;

                    status.poisonDuration -= GetDurationCoefficient(4f);
                    status.poisonAdditionalDamage -= bonus;
                    status.poisonSpeed -= bonus;
                    status.allowedOverlapPoison -= bonus;

                    status.chillDuration -= GetDurationCoefficient(5f);
                    status.chillDecreaseSpeed -= bonus;

                    status.shockDuration -= GetDurationCoefficient(5f);

                    status.stunDuration -= GetDurationCoefficient(1f);

                    status.hp -= bonus;
                    status.precision -= bonus;
                }
                CharacterController2D characterController2D = creature._controller2D;
                if (characterController2D != null)
                {
                    characterController2D.speedBonus -= bonus;
                }
                PlayerDashAttackModule playerDashAttackModule = creature.GetComponent<PlayerDashAttackModule>();
                if (playerDashAttackModule)
                {
                    playerDashAttackModule.damageRate -= bonus / 100f;
                }
                creature.atkSpeedBonus -= bonus;
                creature.reloadSpeedBonus -= bonus;
                creature.activeSkillCoolDownBonus -= bonus;
            }
            base.Destroy();
        }
    }
}
