using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Buffs.Archer
{
	/// <summary>
	/// Следующий огненный обсрел не имеет стоимости
	/// </summary>
	public class FireShellingBuff : Buff
	{
		public FireShellingBuff()
            : base()
        {
            this.DurationSec = 4;
            this.Name = BuffNames.Archer.FireShellingBuff;
            this.MaxStack = 1;
			this.IsDot = false;
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
