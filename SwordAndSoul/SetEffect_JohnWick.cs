using System;
using BepInEx;
using UnityEngine;
using DungreedAPI;
using System.Collections;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Collections.Generic;

namespace SwordAndSoul
{
    public class SetEffect_JohnWick : Player_SetEffect
    {
        public override void Activate()
        {
            base.Activate();
            if (player)
            {
                Util.AddToDelegate(ref player._creature.beHitDelegate, BeHit);
            }
        }

        public override void Inactivate()
        {
            if (player)
            {
                Util.RemoveFromDelegate(ref player._creature.beHitDelegate, BeHit);
            }
            base.Inactivate();
        }

        public override void Destroy()
        {
            if (player)
            {
                Util.RemoveFromDelegate(ref player._creature.beHitDelegate, BeHit);
            }
            base.Destroy();
        }

        private void BeHit(Creature creature, Creature.HitData hitData)
        {
            if (hitData.hitType == Creature.HitType.BULLET)
            {
                hitData.block = Mathf.Infinity;
            }
        }
    }
}
