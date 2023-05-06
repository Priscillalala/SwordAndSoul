using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;
using System.Collections;
using UnityEngine.PostProcessing;

namespace SwordAndSoul
{
    public class Accessory_Stimulants : Player_Accessory
    {
        public static GameObject fxPrefab;

        public float hpThreshold;
        public int attackBonus;
        public int evasionBonus;

        private StatusModule_Power power;
        private StatusModule_AttackSpeed attackSpeed;
        private StatusModule_Evasion evasion;
        private GameObject fxInstance;
        private bool active;

        public override void SetPlayer(Player player)
        {
            base.SetPlayer(player);
            power = new StatusModule_Power(attackBonus);
            power.Initialize(player._creature);
            attackSpeed = new StatusModule_AttackSpeed(attackBonus);
            attackSpeed.Initialize(player._creature);
            evasion = new StatusModule_Evasion(evasionBonus);
            evasion.Initialize(player._creature);
        }

        public void Update()
        {
            bool shouldBeActive = player && player._creature.status.hp / player._creature.status.GetMAXHP() <= hpThreshold;
            if (shouldBeActive != active)
            {
                if (shouldBeActive)
                {
                    Activate();
                }
                else
                {
                    Deactivate();
                }
            }
            if (active)
            {
                PostProcessingAPI.RequestUpdate();
            }
        }

        public override void Destroy()
        {
            base.Destroy();
            if (active)
            {
                Deactivate();
            }
        }

        private void Activate()
        {
            power.ApplyEffect();
            attackSpeed.ApplyEffect();
            evasion.ApplyEffect();
            if (!fxInstance)
            {
                fxInstance = Instantiate(fxPrefab);
            }
            PostProcessingAPI.OnUpdatePostProcessing += AddPostProcessingEffect;
            active = true;
        }

        private void AddPostProcessingEffect(PostProcessingProfile obj)
        {
            obj.chromaticAberration.enabled = true;
            obj.chromaticAberration.m_Settings.intensity += (Mathf.Sin(Time.time * 20f) + 1f) * 0.2f;
            obj.bloom.m_Settings.bloom.intensity += 2f;
        }

        private void Deactivate()
        {
            power.RemoveEffect();
            attackSpeed.RemoveEffect();
            evasion.RemoveEffect();
            if (fxInstance)
            {
                Destroy(fxInstance);
            }
            PostProcessingAPI.OnUpdatePostProcessing -= AddPostProcessingEffect;
            active = false;
        }
    }
}
