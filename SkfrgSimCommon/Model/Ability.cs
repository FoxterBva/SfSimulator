using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	/// <summary>
	/// Represents ability state of the actor
	/// </summary>
	public class Ability
	{
		public Ability()
		{
			Parameters = new AbilityParams();
			LastExecutedAt = -1;
		}

		public int LastExecutedAt { get; set; }
		
		public AbilityParams Parameters { get; set; }

		public virtual void OnCast(EnvironmentContext context)
		{
            var currentParams = context.Actor.GetAbilityParams(this.Parameters.Name);

            // if cost < 0 -> this abiliy regens resource
            if (currentParams.ResourceCost < 0 && context.Actor.CurrentResource < context.Actor.MaxResource)
                context.Actor.CurrentResource = Math.Min(context.Actor.MaxResource, context.Actor.CurrentResource - currentParams.ResourceCost);

            var abilityCost = currentParams.ResourceCost;
            

            if (abilityCost < 0)
                abilityCost = 0;

            if (abilityCost > 0)
                context.Actor.CurrentResource -= abilityCost;

            LastExecutedAt = context.CurrentTime;

            if (currentParams.DmgCoeff > 0)
                context.ApplyDamage(this);
		}

        public bool IsOnCd(int currTime)
        {
            return currTime - LastExecutedAt < Parameters.CoolDown * 1000 && LastExecutedAt >= 0;
        }

        public AbilityParams CopyParams()
        {
            AbilityParams p = new AbilityParams()
            {
                CoolDown = Parameters.CoolDown,
                DmgCoeff = Parameters.DmgCoeff,
                DmgDelay = Parameters.DmgDelay,
                ImpulseDmgCoeff = Parameters.ImpulseDmgCoeff,
                IsUseImpulse = Parameters.IsUseImpulse,
                Name = Parameters.Name,
                ResourceCost = Parameters.ResourceCost,
                TotalCastTime = Parameters.TotalCastTime
            };

            return p;
        }
	}
}
