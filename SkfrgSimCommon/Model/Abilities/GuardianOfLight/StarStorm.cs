using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.GuardianOfLight
{
    public class StarStorm : Ability
    {
        public StarStorm()
            : base()
        {
            Parameters = new AbilityParams() 
            {
                Name = AbilityNames.GuardianOfLight.FlashingSpark,
                CoolDown = 20,
                TotalCastTime = 1550,
                DmgCoeff = 2.46,
                DmgDelay = 0,
                ImpulseDmgCoeff = 1,
                IsUseImpulse = true,
                ResourceCost = 0
            };
        }

        public override void OnCastStart(EnvironmentContext context)
        {
            base.OnCastStart(context);
        }
    }
}
