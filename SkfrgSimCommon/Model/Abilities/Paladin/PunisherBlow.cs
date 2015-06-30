using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Paladin
{
    public class PunisherBlow : Ability
    {
        public PunisherBlow() : base() 
        {
            Parameters = new AbilityParams() 
            {
                Name = AbilityNames.Paladin.LKMx2,
                TotalCastTime = 400,
                CoolDown = 0,
                DmgCoeff = 0.19,
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
