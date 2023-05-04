using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;

namespace SwordAndSoul
{
    public class Items : MonoBehaviour
    {
        public static MyAccessoryData LotteryTicket { get; private set; }
        public static MyAccessoryData Stimulants { get; private set; }

        public void Awake()
        {
            GameObject lottoPrefab = PrefabAPI.CreatePrefab("LotteryTicket");
            lottoPrefab.AddComponent(out Accessory_LotteryTicket accessory_LotteryTicket);
            accessory_LotteryTicket.chance = 1;
            accessory_LotteryTicket.goldReward = 500;
            accessory_LotteryTicket.effectDestriptionKey = "Accessory_GS_LotteryTicket";

            LotteryTicket = ItemAPI.AddNewAccessory(
                name: "GS_LotteryTicket", 
                rarity: ItemRarityTier.UNCOMMON,
                price: 2500, 
                icon: SwordAndSoul.assets.LoadAsset<Sprite>("LotteryTicket"),
                prefab: lottoPrefab,
                effects: new[] { Effects.EVASION(-5) }
                );

            GameObject stimsPrefab = PrefabAPI.CreatePrefab("Stimulants");
            stimsPrefab.AddComponent(out Accessory_Stimulants accessory_Stimulants);
            accessory_Stimulants.effectDestriptionKey = "Accessory_GS_Stimulants";
            accessory_Stimulants.hpThreshold = 0.2f;
            accessory_Stimulants.attackBonus = 40;
            accessory_Stimulants.evasionBonus = 100;

            Stimulants = ItemAPI.AddNewAccessory(
                name: "GS_Stimulants",
                rarity: ItemRarityTier.RARE,
                price: 4000,
                icon: SwordAndSoul.assets.LoadAsset<Sprite>("Stimulants"),
                prefab: stimsPrefab
                );

            LocalizationAPI.AddMany(
                (LotteryTicket.aName, "Lotto Ticket"),
                (LotteryTicket.aDescription, "90% of gamblers quit right before they're about to hit it big."),
                (accessory_LotteryTicket.effectDestriptionKey, $"{accessory_LotteryTicket.chance}% chance to gain an additional {accessory_LotteryTicket.goldReward * 10} gold"), 
                (Stimulants.aName, "Stimulant"),
                (Stimulants.aDescription, "AAA."),
                (accessory_Stimulants.effectDestriptionKey, $"Increase power and attack speed by {accessory_Stimulants.attackBonus} and evasion by {accessory_Stimulants.evasionBonus} when HP is below {accessory_Stimulants.hpThreshold:0%}")
                );
        }
    }
}
