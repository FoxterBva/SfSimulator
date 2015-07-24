using SkfrgSimCommon.Model.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Paladin
{
    public class HolyGround : Ability
    {
        public HolyGround()
        {
            this.Parameters = new AbilityParams()
            {
                CoolDown = 60,
                Name = AbilityNames.Paladin.HolyGround
            };
        }

        public override void OnCast(EnvironmentContext context)
        {
            base.OnCast(context);

            context.ApplyBuff(new PaladinHolyGround(), this.Parameters.Name);
        }
    }
}
