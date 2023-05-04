using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;
using System.Security.Permissions;
using System.Security;

[module: UnverifiableCode]
#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete

namespace SwordAndSoul
{
    [BepInDependency("com.groovesalad.DungreedAPI")]
    [BepInPlugin("com.groovesalad.SwordAndSoul", "SwordAndSoul", "1.0.0")]
    public class SwordAndSoul : BaseUnityPlugin
    {
        public static AssetBundle assets { get; private set; }
        public void Awake()
        {
            assets = AssetBundle.LoadFromFile(this.GetRelativePath("swordandsoulassets"));

            GameObject swordAndSoul = new GameObject("SwordAndSoul", typeof(Items), typeof(Food));
            DontDestroyOnLoad(swordAndSoul);
        }
    }
}
