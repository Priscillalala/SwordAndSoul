using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;
using UnityEngine.PostProcessing;

namespace SwordAndSoul
{
    public class Items : MonoBehaviour
    {
        public static MyAccessoryData LotteryTicket { get; private set; }
        private Accessory_LotteryTicket accessory_LotteryTicket;
        public static MyAccessoryData Stimulants { get; private set; }
        private Accessory_Stimulants accessory_Stimulants;
        public static MyAccessoryData TacticalSuit { get; private set; }
        private Accessory_TacticalSuit accessory_TacticalSuit;

        private void Awake()
        {
            LotteryTicket = CreateLotteryTicket();
            Stimulants = CreateStimulants();
            TacticalSuit = CreateTacticalSuit();

            Localization();
        }

        private void Localization()
        {
            LocalizationAPI.AddMany(
                (TacticalSuit.aName, "Tactical Suit"),
                (TacticalSuit.aDescription, "Made with the finest materials. And it has SO many pockets. That's what makes it awesome."),
                (accessory_TacticalSuit.effectDestriptionKey, $"Access your inventory while in battle"),
                (Stimulants.aName, "Stimulant"),
                (Stimulants.aDescription, "AAA."),
                (accessory_Stimulants.effectDestriptionKey, $"Increase power and attack speed by {accessory_Stimulants.attackBonus} and evasion by {accessory_Stimulants.evasionBonus} when HP is below {accessory_Stimulants.hpThreshold:0%}"),
                (LotteryTicket.aName, "Lotto Ticket"),
                (LotteryTicket.aDescription, "90% of gamblers quit right before they're about to hit it big."),
                (accessory_LotteryTicket.effectDestriptionKey, $"{accessory_LotteryTicket.chance}% chance to gain an additional {accessory_LotteryTicket.goldReward * 10} gold")
                );
        }

        private MyAccessoryData CreateTacticalSuit()
        {
            GameObject suitPrefab = PrefabAPI.CreatePrefab("TacticalSuit");
            suitPrefab.AddComponent(out accessory_TacticalSuit);
            accessory_TacticalSuit.effectDestriptionKey = "Accessory_GS_TacticalSuit";

            return  ItemAPI.AddNewAccessory(
                name: "GS_TacticalSuit",
                rarity: ItemRarityTier.RARE,
                price: 4500,
                icon: SwordAndSoul.assets.LoadAsset<Sprite>("TacticalSuit"),
                prefab: suitPrefab,
                defense: 12,
                effects: new[] { Effects.TOUGHNESS(1) }
                );
        }

        private MyAccessoryData CreateStimulants()
        {
            Accessory_Stimulants.fxPrefab = PrefabAPI.CreatePrefab("StimulantsEffect");
            /*Accessory_Stimulants.fxPrefab.AddComponent(out PostProcessingBehaviour ppBehaviour);
            ppBehaviour.profile = ScriptableObject.CreateInstance<PostProcessingProfile>();
            ppBehaviour.profile.bloom.m_Settings.bloom = new BloomModel.BloomSettings
            {
                intensity = 1.5f,
                threshold = 1.075f,
                softKnee = 0.5f,
                radius = 4f,
                antiFlicker = false,
            };
            ppBehaviour.profile.bloom.enabled = true;
            ppBehaviour.profile.chromaticAberration.m_Settings.intensity = 1f;
            ppBehaviour.profile.chromaticAberration.enabled = true;*/

            GameObject stimsPrefab = PrefabAPI.CreatePrefab("Stimulants");
            stimsPrefab.AddComponent(out accessory_Stimulants);
            accessory_Stimulants.effectDestriptionKey = "Accessory_GS_Stimulants";
            accessory_Stimulants.hpThreshold = 0.2f;
            accessory_Stimulants.attackBonus = 40;
            accessory_Stimulants.evasionBonus = 80;

            return ItemAPI.AddNewAccessory(
                name: "GS_Stimulants",
                rarity: ItemRarityTier.RARE,
                price: 4000,
                icon: SwordAndSoul.assets.LoadAsset<Sprite>("Stimulants"),
                prefab: stimsPrefab
                );
        }

        private MyAccessoryData CreateLotteryTicket()
        {
            GameObject lottoPrefab = PrefabAPI.CreatePrefab("LotteryTicket");
            lottoPrefab.AddComponent(out accessory_LotteryTicket);
            accessory_LotteryTicket.chance = 1;
            accessory_LotteryTicket.goldReward = 500;
            accessory_LotteryTicket.effectDestriptionKey = "Accessory_GS_LotteryTicket";

            return ItemAPI.AddNewAccessory(
                name: "GS_LotteryTicket",
                rarity: ItemRarityTier.UNCOMMON,
                price: 2500,
                icon: SwordAndSoul.assets.LoadAsset<Sprite>("LotteryTicket"),
                prefab: lottoPrefab,
                effects: new[] { Effects.EVASION(-5) }
                );
        }
    }
}
