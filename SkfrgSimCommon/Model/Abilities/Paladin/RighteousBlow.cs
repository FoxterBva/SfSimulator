using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Paladin
{
    public class RighteousBlow : Ability
    {
        public RighteousBlow() : base()
        {
            Parameters = new AbilityParams() 
            {
                Name = AbilityNames.Paladin.LKM,
                TotalCastTime = 400,
                CoolDown = 0,
                DmgCoeff = 0.06,
                ImpulseDmgCoeff = 0,
                IsUseImpulse = false,
                ResourceCost = 0
            };   
        }

        public override void OnCastStart(EnvironmentContext context)
        {
            base.OnCastStart(context);
        }
    }
}
