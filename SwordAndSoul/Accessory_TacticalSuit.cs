using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;
using System.Collections;

namespace SwordAndSoul
{
    public class Accessory_TacticalSuit : Player_Accessory
    {
        private TimeScaleAPI.Handle timeScaleHandle;

        public override void SetPlayer(Player player)
        {
            base.SetPlayer(player);
            On.Player.EquipItemInInventory += Player_EquipItemInInventory;
            On.Player.UnequipItem += Player_UnequipItem;
            //On.Chest.Interactive += Chest_Interactive;
            On.UIManager.Update += UIManager_Update;
        }

        private void Player_EquipItemInInventory(On.Player.orig_EquipItemInInventory orig, Player self, int inventoryIndex, EquipmentSlot slot, bool autoRequest)
        {
            InventoryMethodWrapper(self, () => orig(self, inventoryIndex, slot, autoRequest));
        }

        private void Player_UnequipItem(On.Player.orig_UnequipItem orig, Player self, EquipmentSlot slot, bool ignoreBattleState, bool ignoreLockEquipment)
        {
            InventoryMethodWrapper(self, () => orig(self, slot, ignoreBattleState, ignoreLockEquipment));
        }

        private void Chest_Interactive(On.Chest.orig_Interactive orig, Chest self, GameObject actor)
        {
            InventoryMethodWrapper(GameManager.Instance?.currentPlayer, () => orig(self, actor));
        }

        private void UIManager_Update(On.UIManager.orig_Update orig, UIManager self)
        {
            InventoryMethodWrapper(GameManager.Instance?.currentPlayer, () => orig(self));
        }

        public override void Destroy()
        {
            On.Player.EquipItemInInventory -= Player_EquipItemInInventory;
            On.Player.UnequipItem -= Player_UnequipItem;
            //On.Chest.Interactive -= Chest_Interactive;
            On.UIManager.Update -= UIManager_Update;
            if (timeScaleHandle != null)
            {
                TimeScaleAPI.UnsetTimeScale(ref timeScaleHandle);
            }
            base.Destroy();
        }

        public void InventoryMethodWrapper(Player targetPlayer, Action invokeMethod)
        {
            int? previousBattleCount = null;
            if (player && player == targetPlayer && player.CanControl)
            {
                previousBattleCount = player._creature.onBattleCount;
                player._creature.onBattleCount = 0;
            }
            invokeMethod();
            if (previousBattleCount != null)
            {
                player._creature.onBattleCount = (int)previousBattleCount;
            }
        }

        public void Update()
        {
            bool needsTimeScale = GameManager.Instance && GameManager.Instance.currentPlayer._creature.IsOnBattle
                && UIManager.Instance && (UIManager.Instance.inventoryPanel.IsOpened() || UIManager.Instance.mapPanel.IsOpened());
            bool timeScaleActive = timeScaleHandle != null;
            if (needsTimeScale != timeScaleActive)
            {
                if (needsTimeScale)
                {
                    TimeScaleAPI.SetTimeScale(0.1f, out timeScaleHandle);
                }
                else
                {
                    TimeScaleAPI.UnsetTimeScale(ref timeScaleHandle);
                }
            }
        }
    }
}
