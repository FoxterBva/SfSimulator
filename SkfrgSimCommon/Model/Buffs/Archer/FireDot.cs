using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Buffs.Archer
{
    public class FireDot : Buff
    {
        public FireDot()
            : base()
        {
            this.DurationSec = 12;
            this.Name = BuffNames.Archer.FireDot;
            this.MaxStack = 1;
			this.IsDot = true;
			this.Effects = new List<BuffEffect>()
            {
                new BuffEffect()
                {
                    ResourceCost = -1000,
                    IsAppliedTo = (name) => name == AbilityNames.Archer.FireShelling
                }
            };
        }
    }
}
