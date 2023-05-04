using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;

namespace SwordAndSoul
{
    public class Food : MonoBehaviour
    {
        public static MyFoodData Gingersnap { get; private set; }

        public void Awake()
        {
            Gingersnap = FoodAPI.AddNew(name: "GS_Gingersnap", 
                basePrice: 1200, 
                satiety: 35, 
                randomHealing: new Range<int>(3, 6), 
                randomPower: new Range<float>(0, 10), 
                icon: SwordAndSoul.assets.LoadAsset<Sprite>("Gingersnap"),
                foodType: MyFoodData.FoodType.SPECIALTY,
                effects: new[] { Effects.EVASION(25) },
                canAmplifyEffects: false,
                eatClip: Resources.Load<MyFoodData>("foods/VS110_ChocolateCookie").eatClip
                );

            LocalizationAPI.AddMany(
                (Gingersnap.aName, "Gingersnap"), 
                (Gingersnap.aDescription, "Really gets the blood pumping...probably my secret ingredient, sugar.")
                );
        }
    }
}
