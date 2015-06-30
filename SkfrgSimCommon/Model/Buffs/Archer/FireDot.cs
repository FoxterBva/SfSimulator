using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Buffs.Archer
{
    class FireDot : Buff
    {
        public FireDot()
            : base()
        {
            this.DurationSec = 12;
            this.Name = BuffNames.Archer.FireDot;
            this.MaxStack = 1;
        }
    }
}
