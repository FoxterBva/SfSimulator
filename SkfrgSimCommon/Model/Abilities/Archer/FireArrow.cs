﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkfrgSimCommon.Model.Buffs.Archer;

namespace SkfrgSimCommon.Model.Abilities.Archer
{
	public class FireArrow : Ability
	{
		protected Random rnd;

		public FireArrow()
			: base()
		{
            Parameters = new AbilityParams()
            {
                Name = AbilityNames.Archer.FireArrow,
				CoolDown = 0,
				DmgCoeff = 4.8,
				DmgDelay = 500,
				ImpulseDmgCoeff = 0,
				IsUseImpulse = false,
				ResourceCost = 100,
				TickDelay = 500,
				Ticks = 24,
				TotalCastTime = 1000
            };

			rnd = new Random((int)DateTime.UtcNow.Ticks);
		}

		public override void OnCast(EnvironmentContext context)
		{
			base.OnCast(context);

			context.ApplyBuff(new FireDot(), this.Parameters.Name);

			if (rnd.NextDouble() <= 0.2)
			{
				context.ApplyBuff(new FireShellingBuff(), this.Parameters.Name);
			}
		}
	}
}
