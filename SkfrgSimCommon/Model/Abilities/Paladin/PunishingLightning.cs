using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Paladin
{
    /// <summary>
    /// Карающая молния
    /// </summary>
    public class PunishingLightning : Ability
    {
        public PunishingLightning() : base() 
        {
            Parameters = new AbilityParams() 
            {
                Name = AbilityNames.Paladin.LKMx2PKM,
                TotalCastTime = 1200,
                CoolDown = 0,
                DmgCoeff = 2.51,
                ImpulseDmgCoeff = 1,
                IsUseImpulse = true,
                ResourceCost = 130
            };   
        }

        public override void OnCast(EnvironmentContext context)
        {
            base.OnCast(context);
        }
    }
}
