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
    public class StatusModule_AllStats : StatusModule
    {
		public StatusModule_AllStats(int value)
		{
			this.value = value;
			this.baseValue = value;
		}

		public override void _Apply()
		{
			if (Parent != null)
			{
				Parent.status.power += value;
				Parent.status.power += value;
			}
		}

		public override void _Remove()
		{
			if (Parent != null)
			{
				Parent.status.toughness -= value;
			}
		}

		public override void _Amplify(float ratio)
		{
			this.value = (int)(this.value * ratio);
		}

		public override void _ClearAmplify()
		{
			this.value = this.baseValue;
		}

		public override string ToString()
		{
			return ((this.value < 0) ? "<color=#ff0000ff>" : "<color=#00ff00ff>") + this.value.ToString("+0;-#") + "</color> " + I._("StatusModule_GS_AllStats");
		}

		public override string GetString()
		{
			return "GS_ALLSTATS/" + this.baseValue.ToString();
		}

		public int value;

		private readonly int baseValue;
	}
}
