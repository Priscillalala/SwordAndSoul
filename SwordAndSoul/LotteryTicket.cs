using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;
using System.Collections;

namespace SwordAndSoul
{
    public class Accessory_LotteryTicket : Player_Accessory
    {
        public float chance;
        public int goldReward;

        public override void SetPlayer(Player player)
        {
            base.SetPlayer(player);
            On.DropGold.Drop += DropGold_Drop;
            On.Chest.CoinCoroutine += Chest_CoinCoroutine;
        }

        public override void Destroy()
        {
            On.DropGold.Drop -= DropGold_Drop;
            On.Chest.CoinCoroutine -= Chest_CoinCoroutine;
            base.Destroy();
        }

        private void DropGold_Drop(On.DropGold.orig_Drop orig, DropGold self)
        {
            if (self.enable)
            {
                RollLottery(self.goldDropPos ? self.goldDropPos.position : self._creature.CenterPosition);
            }
            orig(self);
        }

        private IEnumerator Chest_CoinCoroutine(On.Chest.orig_CoinCoroutine orig, Chest self)
        {
            RollLottery(new Vector3(self.transform.position.x, self.transform.position.y + 0.7f));
            return orig(self);
        }

        public void RollLottery(Vector3 position)
        {
            if (player != null && GameManager.Instance?.currentPlayer == player && UnityEngine.Random.Range(0f, 100f) <= chance)
            {
                StartCoroutine(DropGoldReward(position));
            }
        }

        private IEnumerator DropGoldReward(Vector3 position)
        {
            int gold = goldReward;
            while (gold > 0)
            {
                if (gold >= 10 && UnityEngine.Random.Range(0f, 1f) <= 0.5f)
                {
                    gold -= 10;
                    FxPool.Instance.CreateBullion(position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 0f));
                }
                else
                {
                    gold--;
                    FxPool.Instance.CreateCoin(position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 0f));
                }
                yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.025f));
            }
        }
    }
}
