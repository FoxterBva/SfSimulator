using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Buffs.Paladin
{
    public class Fanaticism : Buff
    {
        public Fanaticism() : base() 
        {
            this.DurationSec = 0;
            this.Name = BuffNames.Paladin.FanaticismTalent;
            this.Effects = new List<BuffEffect>() 
            {
                new BuffEffect()
                {
                    SkillDamageModPercent = 120,
                    IsAppliedTo = (name) => 
                        name == AbilityNames.Paladin.LKM ||
                        name == AbilityNames.Paladin.LKMx2 ||
                        name == AbilityNames.Paladin.LKMx3 
                }
            };
        }
    }
}
