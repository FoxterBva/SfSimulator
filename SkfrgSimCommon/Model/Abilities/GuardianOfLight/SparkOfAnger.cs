using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.GuardianOfLight
{
    public class SparkOfAnger : Ability
    {
        public SparkOfAnger() : base()
        {
            Parameters = new AbilityParams()
            {
                Name = AbilityNames.GuardianOfLight.SparkOfAnger,
                TotalCastTime = 1000,
                CoolDown = 0,
                DmgCoeff = 1.16,
                ImpulseDmgCoeff = 1,
                IsUseImpulse = true,
                ResourceCost = 100
            };
        }

        public override void OnCastStart(EnvironmentContext context)
        {
            base.OnCastStart(context);
        }
    }
}
