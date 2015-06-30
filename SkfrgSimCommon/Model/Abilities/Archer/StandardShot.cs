using SkfrgSimCommon.Model.Buffs;
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
				TotalCastTime = 250,
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

            // TODO: if actor has a buff to stacks -> place debuff to the target
			context.ApplyBuff(new ArcherPristrel());
		}
	}
}
