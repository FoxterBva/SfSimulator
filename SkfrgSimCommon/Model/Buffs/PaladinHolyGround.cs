using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Buffs
{
    public class PaladinHolyGround : Buff
    {
        public PaladinHolyGround()
        {
            this.DurationSec = 20;
            this.Name = "Святая земля";
            this.Effects = new List<BuffEffect>() 
            {
                new BuffEffect()
                {
                    ResourceCost = -1000
                }
            };
        }
    }
}
