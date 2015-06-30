using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Archer
{
	public class PiercingShot : Ability
	{
		public PiercingShot()
			: base()
		{
            Parameters = new AbilityParams();
			Parameters.Name = AbilityNames.Archer.PiercingShot;
		}
	}
}
