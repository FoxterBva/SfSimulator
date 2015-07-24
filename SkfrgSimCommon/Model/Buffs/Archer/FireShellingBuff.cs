using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Buffs.Archer
{
	public class FireShellingBuff : Buff
	{
		public FireShellingBuff()
            : base()
        {
            this.DurationSec = 4;
            this.Name = BuffNames.Archer.FireShellingBuff;
            this.MaxStack = 1;
			this.IsDot = false;
        }
	}
}
