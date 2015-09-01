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
				TotalCastTime = 600,
				CoolDown = 0,
				DmgCoeff = 0.28,
				ImpulseDmgCoeff = 1,
				IsUseImpulse = true,
				ResourceCost = -15
			};
		}
        
		public override void OnCastStart(EnvironmentContext context)
		{
			base.OnCastStart(context);

            // TODO: if actor has a buff to stacks -> place debuff to the target
			//context.ApplyBuff(new ArcherPristrel(), this.Parameters.Name);
		}
	}
}
