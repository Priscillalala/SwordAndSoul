using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;
using UnityEngine.PostProcessing;

namespace SwordAndSoul
{
    public class Sets : MonoBehaviour
    {
        public static MySetEffectData JohnWick { get; private set; }
        private SetEffect_JohnWick setEffect_JohnWick;

        private void Awake()
        {
            if (Items.TacticalSuit)
            {
                JohnWick = CreateJohnWick();
            }

            Localization();
        }

        private void Localization()
        {
            LocalizationAPI.AddMany(
                (JohnWick.aName, "Excommunicado"),
                (JohnWick.aDescription, "."),
                (setEffect_JohnWick.effectDestriptionKey, $"Block all ranged attacks")
                );
        }

        private MySetEffectData CreateJohnWick()
        {
            GameObject johnWickPrefab = PrefabAPI.CreatePrefab("LotteryTicket");
            johnWickPrefab.AddComponent(out setEffect_JohnWick);
            setEffect_JohnWick.effectDestriptionKey = "SetEffect_GS_LotteryTicket";

            return SetEffectAPI.AddNew(
                name: "GS_JohnWick",
                icon: SwordAndSoul.assets.LoadAsset<Sprite>("JohnWick"),
                mainWeapon: Resources.Load<MyWeaponData>("items/0002_Pistol"),
                accessories: new[] { Items.TacticalSuit },
                prefab: johnWickPrefab,
                effects: new[] { Effects.POWER(50) }
                );
        }
    }
}
