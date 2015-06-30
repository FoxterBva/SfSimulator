using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Buffs.Paladin
{
    public class Detachment : Buff
    {
        public Detachment()
            : base()
        {
            this.DurationSec = 0;
            this.Name = BuffNames.Paladin.DetachmentTalent;
            this.Effects = new List<BuffEffect>()
            {
                new BuffEffect()
                {
                    ResourceCost = -30,
                    IsAppliedTo = (name) => name == AbilityNames.Paladin.LKMx2PKM
                }
            };

        }
    }
}
