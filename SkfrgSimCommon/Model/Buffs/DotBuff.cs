﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Buffs
{
    public class DotBuff : Buff
    {
        public DotBuff()
            : base()
        {
			this.IsDot = true;
        }

		public double DmgCoeff { get; set; }
		public int Ticks { get; set; }
        public int FirstTickDelayMS { get; set; }
        public int TickDelayMS { get; set; }
    }
}
