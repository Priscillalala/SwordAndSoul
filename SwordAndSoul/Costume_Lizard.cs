using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;
using System.Collections;
using System.Collections.Generic;
using static EnhancedEnemy;
using static StatusModule_Condition;

namespace SwordAndSoul
{
    public class Costume_Lizard : Player_Accessory
    {
        const int maxEnhancement = (int)EnhancedType.Shock;

        private StatusModule_Condition condition;
        private StatusModule_ConditionResist conditionResist;
        private GameObject fxInstance;
        private EnhancedType previousEnhancement = (EnhancedType)UnityEngine.Random.Range(0, maxEnhancement + 1);

        public override void SetPlayer(Player player)
        {
            base.SetPlayer(player);
            Util.AddToDelegate(ref player._creature.onStartBattle, OnStartBattle);
            Util.AddToDelegate(ref player._creature.onEndBattle, OnEndBattle);
        }

        public override void Destroy()
        {
            Util.RemoveFromDelegate(ref player._creature.onStartBattle, OnStartBattle);
            Util.RemoveFromDelegate(ref player._creature.onEndBattle, OnEndBattle);
            SetEnhancement(EnhancedType.NONE);
            base.Destroy();
        }

        public void OnStartBattle(Creature creature)
        {
            EnhancedType enhancedType = (EnhancedType)UnityEngine.Random.Range(0, maxEnhancement);
            if (enhancedType == previousEnhancement)
            {
                enhancedType = (EnhancedType)maxEnhancement;
            }
            SetEnhancement(enhancedType);
        }

        public void OnEndBattle(Creature creature)
        {
            if (!creature.IsOnBattle)
            {
                SetEnhancement(EnhancedType.NONE);
            }
        }

        public void SetEnhancement(EnhancedType enhancedType)
        {
            ClearEnhancement();
            if (enhancedType == EnhancedType.NONE)
            {
                return;
            }

            GameObject fxPrefabResource = Resources.Load<GameObject>("FX/EnhancedMonster/" + enhancedType.ToString());
            if (fxPrefabResource)
            {
                fxInstance = Instantiate(fxPrefabResource, player._creature.transform.position + new Vector3(0f, 0.6f), Quaternion.identity, player._creature.transform);
                if (fxInstance && fxInstance.TryGetComponent(out CircleFxEmitter circleFxEmitter))
                {
                    circleFxEmitter.radius = 0.3f;
                }
            }

            Condition conditionType;
            switch (enhancedType)
            {
                case EnhancedType.Burn:
                    conditionType = Condition.BURN;
                    break;
                case EnhancedType.Chill:
                    conditionType = Condition.CHILL;
                    break;
                case EnhancedType.Poison:
                    conditionType = Condition.POISON;
                    break;
                case EnhancedType.Shock:
                    conditionType = Condition.SHOCK;
                    break;
                default:
                    conditionType = (Condition)(-1);
                    break;
            }
            if (conditionType >= 0)
            {
                condition = new StatusModule_Condition(conditionType, 100f);
                condition.Initialize(player._creature);
                condition.ApplyEffect();
                conditionResist = new StatusModule_ConditionResist(conditionType);
                conditionResist.Initialize(player._creature);
                conditionResist.ApplyEffect();
                previousEnhancement = enhancedType;
            }
        }

        public void ClearEnhancement()
        {
            if (condition != null)
            {
                condition.RemoveEffect();
                condition = null;
            }
            if (conditionResist != null)
            {
                conditionResist.RemoveEffect();
                conditionResist = null;
            }
            if (fxInstance != null)
            {
                if (fxInstance.TryGetComponent(out CircleFxEmitter circleFxEmitter))
                {
                    circleFxEmitter.enabled = false;
                    fxInstance.AddComponent<FinishCircleFx>();
                }
                else
                {
                    Destroy(fxInstance);
                }
                fxInstance = null;
            }
        }

        public class FinishCircleFx : MonoBehaviour
        {
            public void Update()
            {
                maxDuration -= Time.deltaTime;
                if (maxDuration <- 0f || base.transform.childCount == 0)
                {
                    Destroy(base.gameObject);
                }
            }

            private float maxDuration = 30f;
        }
    }
}
