﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Archer
{
	public class FireArrow : Ability
	{
		public FireArrow()
			: base()
		{
			Parameters.Name = AbilityNames.Archer.FireArrow;
		}
	}
}