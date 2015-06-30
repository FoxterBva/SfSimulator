using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Buffs
{
    public class ArcherPristrel : Buff
    {
        public ArcherPristrel() : base()
        {
            this.DurationSec = 9;
            this.Name = BuffNames.Archer.Pristrel;
            this.MaxStack = 3;
            this.Effects = new List<BuffEffect>() 
            {
                new BuffEffect()
                {
                    TotalDamageModPercent = 4,
                    Condition = (ec) => true,
                    IsAppliedTo = (name) => true
                }
            };
        }

    }
}
