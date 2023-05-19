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
        public static MySetEffectData MasterSwordsman { get; private set; }


        private void Awake()
        {
            if (Items.TacticalSuit)
            {
                //JohnWick = CreateJohnWick();
            }
            if (Items.PaperArmy)
            {
                MasterSwordsman = CreateMasterSwordsman();
            }

            Localization();
        }

        private void Localization()
        {
            LocalizationAPI.AddMany(
                (MasterSwordsman?.aName, "Master Swordsman"),
                (MasterSwordsman?.aDescription, "May you strike once and strike true."),
                (JohnWick?.aName, "Excommunicado"),
                (JohnWick?.aDescription, "."),
                (setEffect_JohnWick?.effectDestriptionKey, $"Block all ranged attacks")
                );
        }

        private MySetEffectData CreateMasterSwordsman()
        {
            return SetEffectAPI.AddNew(
                name: "GS_MasterSwordsman",
                icon: SwordAndSoul.assets.LoadAsset<Sprite>("MasterSwordsman"),
                mainWeapon: Resources.Load<MyWeaponData>("items/0281_KujiKanesada"),
                accessories: new[] { Items.PaperArmy },
                effects: new[] { Effects.IGNORE_DEFENSE(100) }
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
