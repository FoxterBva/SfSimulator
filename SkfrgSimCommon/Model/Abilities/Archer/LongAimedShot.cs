using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Archer
{
	public class LongAimedShot : AimedShot
	{
		public LongAimedShot()
			: base()
		{
            Parameters = new AbilityParams();
			Parameters.TotalCastTime = 2000;
			Parameters.DmgCoeff *= 1.2;
			Parameters.Name = AbilityNames.Archer.LongAimedShot;
		}
	}
}
