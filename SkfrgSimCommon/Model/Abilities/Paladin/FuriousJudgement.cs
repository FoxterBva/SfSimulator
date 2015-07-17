using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Paladin
{
	public class FuriousJudgement : Ability
	{
		public FuriousJudgement()
			: base()
		{
			Parameters.Name = AbilityNames.Paladin.Ability4;
			Parameters.TotalCastTime = 950;
			Parameters.CoolDown = 16;
			Parameters.DmgCoeff = 2.36;
			Parameters.ImpulseDmgCoeff = 1;
			Parameters.IsUseImpulse = true;
			Parameters.ResourceCost = 0;
		}
	}
}
