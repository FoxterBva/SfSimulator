using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkfrgSimCommon.Model.Buffs.Archer;

namespace SkfrgSimCommon.Model.Abilities.Archer
{
	public class LongAimedShot : AimedShot
	{
		public LongAimedShot()
			: base()
		{
			Parameters.TotalCastTime = 0;		// TODO: set to 2000, adter adding buff ~ first shot out of combat
			Parameters.DmgCoeff *= 1.2;
			Parameters.Name = AbilityNames.Archer.LongAimedShot;
		}
	}
}
