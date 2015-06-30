using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Paladin
{
    public class SwordOfJustice : Ability
    {
        public SwordOfJustice() : base()
        {
            Parameters = new AbilityParams() 
            {
                Name = AbilityNames.Paladin.LKMx4,
                TotalCastTime = 1600,
                CoolDown = 0,
                DmgCoeff = 0.68,
                ImpulseDmgCoeff = 0,
                IsUseImpulse = false,
                ResourceCost = -70
            };   
        }

        public override void OnCast(EnvironmentContext context)
        {
            base.OnCast(context);
        }
    }
}
