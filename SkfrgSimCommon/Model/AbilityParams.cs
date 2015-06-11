using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	public class AbilityParams
	{
		public string Name { get; set; }
		public double DmgCoeff { get; set; }
		public double ImpulseDmgCoeff { get; set; }
		public double ResourceCost { get; set; }
		public int CoolDown { get; set; }
		public int CastTime { get; set; }
		public bool IsUseImpulse { get; set; }

		// TODO: some additional effects
	}
}
