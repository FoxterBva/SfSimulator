using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Archer
{
	public class AimedShot : Ability
	{
		Random rnd;

		public AimedShot()
		{
			Parameters = new AbilityParams()
			{
				Name = AbilityNames.Archer.AimedShot,
				CastTime = 1000,
				CoolDown = 0,
				DmgCoeff = 2.94,
				ImpulseDmgCoeff = 1,
				IsUseImpulse = true,
				ResourceCost = 100
			};

			rnd = new Random((int)DateTime.UtcNow.Ticks);
		}

		public override void OnCast(EnvironmentContext context)
		{
			base.OnCast(context);

			if (rnd.Next() <= 0.2)
			{
				context.ApplyBuffToSource(null);
			}
		}
	}
}
