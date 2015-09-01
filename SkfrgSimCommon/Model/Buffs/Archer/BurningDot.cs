using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Buffs.Archer
{
	public class BurningDot : DotBuff
	{
		public BurningDot()
			: base()
		{
			this.Name = BuffNames.Archer.BurningDot;
			this.DmgCoeff = 4;	// TODO:
			this.DurationSec = 12;
			this.MaxStack = 1;
			this.Ticks = 24;
			this.FirstTickDelayMS = 500;
			this.TickDelayMS = 500;
		}
	}
}
