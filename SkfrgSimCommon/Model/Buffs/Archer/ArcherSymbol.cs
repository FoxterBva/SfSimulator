using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Buffs.Archer
{
	public class ArcherSymbol : Buff
	{
		public ArcherSymbol()
			: base()
        {
            this.DurationSec = 0;
			this.Name = BuffNames.Archer.ArcherSymbol;
            this.Effects = new List<BuffEffect>() 
            {
                new BuffEffect() 
                    {
                        TotalDamageModPercent = 12,
                        Condition = (ec) => ec.TargetHpRatio >= 0.5,
                        IsAppliedTo = (name) => true
                    }
            };
        }
	}
}
