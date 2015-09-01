using SkfrgSimCommon.Model;
using SkfrgSimCommon.Model.Abilities.GuardianOfLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Classes
{
    public class GuardianOfLight : Actor
    {
        public GuardianOfLight(EnvironmentContext context, ActorStats stats)
            : base(context, stats)
        {
            Abilities = new Dictionary<string, Ability>() { 
				{ AbilityNames.GuardianOfLight.FlashingSpark, new FlashingSpark() },
                { AbilityNames.GuardianOfLight.SparkOfAnger, new SparkOfAnger() },
                { AbilityNames.GuardianOfLight.StarStorm, new StarStorm() }
			};

            MaxResource = 1000;
            CurrentResource = MaxResource;
            ResourceRechargeRate = 1000;
            ResourceRechargeValue = 25;
        }

        protected override string SelectAbility(EnvironmentContext context)
        {
            return String.Empty;
        }
    }
}
