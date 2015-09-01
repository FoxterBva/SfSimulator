using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkfrgSimCommon.Model.Buffs.Archer;

namespace SkfrgSimCommon.Model.Abilities.Archer
{
	public class AimedShot : Ability
	{
		protected Random rnd;

		public AimedShot() : base()
		{
			Parameters = new AbilityParams()
			{
				Name = AbilityNames.Archer.AimedShot,
				TotalCastTime = 1000,
				CoolDown = 0,
				DmgCoeff = 2.94,
				ImpulseDmgCoeff = 1,
				IsUseImpulse = true,
				ResourceCost = 100
			};

			rnd = new Random((int)DateTime.UtcNow.Ticks);
		}

		public override void OnCastStart(EnvironmentContext context)
		{
			base.OnCastStart(context);

			if (rnd.NextDouble() <= 0.2)
			{
				context.ApplyBuff(new FireShellingBuff(), this.Parameters.Name);
			}
		}
	}
}
