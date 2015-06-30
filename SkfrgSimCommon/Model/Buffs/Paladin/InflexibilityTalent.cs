using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Buffs.Paladin
{
    /// <summary>
    /// Паладин Непреклонность талант
    /// </summary>
    public class Inflexibility : Buff
    {
        public Inflexibility() : base()
        {
            this.DurationSec = 0;
            this.Name = BuffNames.Paladin.InflexibilityTalent;
            this.Effects = new List<BuffEffect>() 
            {
                new BuffEffect() 
                    {
                        SkillDamageModPercent = 300,
                        Condition = (ec) => ec.TargetHpRatio <= 0.4,
                        IsAppliedTo = (name) => name == AbilityNames.Paladin.LKMx4
                    }
            };
        }
    }
}
