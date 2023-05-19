using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;
using UnityEngine.PostProcessing;

namespace SwordAndSoul
{
    public class Costumes : MonoBehaviour
    {
        public static MyCostumeData Lizard { get; private set; }
        private Costume_Lizard costume_Lizard;

        private void Awake()
        {
            Lizard = CreateLizard();            

            Localization();
        }

        private void Localization()
        {
            LocalizationAPI.AddMany(
                (Lizard.aName, "Alchemist"),
                (Lizard.aDescription, "Description!"),
                (costume_Lizard.effectDestriptionKey, $"Fiendish Concoction: Brew random enhancements in battle.")
                );
        }

        private MyCostumeData CreateLizard()
        {
            GameObject lizardPrefab = SwordAndSoul.assets.LoadAsset<GameObject>("Lizard");
            lizardPrefab.AddComponent(out costume_Lizard);
            costume_Lizard.effectDestriptionKey = "Costume_GS_Lizard";
            lizardPrefab.AddComponent(out Char_Costume char_Costume);
            char_Costume._accessory = costume_Lizard;

            return CostumeAPI.AddNew(
                name: "GS_Lizard",
                prefab: lizardPrefab,
                icon: SwordAndSoul.assets.LoadAsset<Sprite>("JohnWick"),
                handSprite: SwordAndSoul.assets.LoadAsset<Sprite>("LizardHand"),
                effects: new[] 
                    {
                        Effects.ATTACK_SPEED(-30f),
                        Effects.HP(-20),
                        Effects.DEFENSE(-20f),
                    }
                );
        }
    }
}
