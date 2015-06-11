using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Archer
{
	public class StandardShot : Ability
	{
		public StandardShot()
			: base()
		{
			Parameters = new AbilityParams()
			{
				Name = AbilityNames.Archer.StandardShot,
				CastTime = 250,
				CoolDown = 0,
				DmgCoeff = 0.25,
				ImpulseDmgCoeff = 1,
				IsUseImpulse = true,
				ResourceCost = -15
			};
		}

		public override void OnCast(EnvironmentContext context)
		{
			base.OnCast(context);

			context.ApplyBuffToSource(null);
		}
	}
}
