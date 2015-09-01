using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.GuardianOfLight
{
    public class FlashingSpark : Ability
    {
        Random rnd;

        public FlashingSpark()
            : base()
        {
            Parameters = new AbilityParams() 
            {
                Name = AbilityNames.GuardianOfLight.FlashingSpark,
                CoolDown = 0,
                DmgCoeff = 0.238,
                DmgDelay = 0,
                ImpulseDmgCoeff = 0,
                IsUseImpulse = false,
                ResourceCost = 0,
                TotalCastTime = 575
            };

            rnd = new Random((int)DateTime.UtcNow.Ticks);
        }

        public override void OnCastStart(EnvironmentContext context)
        {
            base.OnCastStart(context);
        }
    }
}
