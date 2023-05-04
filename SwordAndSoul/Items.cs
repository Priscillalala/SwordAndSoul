using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;

namespace SwordAndSoul
{
    public class Items : MonoBehaviour
    {
        public static MyAccessoryData LotteryTicket { get; private set; }

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

            LocalizationAPI.AddMany(
                (LotteryTicket.aName, "Lotto Ticket"),
                (LotteryTicket.aDescription, "90% of gamblers quit right before they're about to hit it big."),
                (accessory_LotteryTicket.effectDestriptionKey, $"{accessory_LotteryTicket.chance}% chance to gain an additional {accessory_LotteryTicket.goldReward * 10} gold")
                );
        }
    }
}
