using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Paladin
{
    public class JudgeBlow : Ability
    {
        public JudgeBlow() : base() 
        {
            Parameters = new AbilityParams() 
            {
                Name = AbilityNames.Paladin.LKMx3,
                TotalCastTime = 600,
                CoolDown = 0,
                DmgCoeff = 0.46,
                ImpulseDmgCoeff = 0,
                IsUseImpulse = false,
                ResourceCost = 0
            };   
        }

        public override void OnCast(EnvironmentContext context)
        {
            base.OnCast(context);
        }
    }
}
