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

		/// <summary>
		/// Fired on ability cast
		/// </summary>
		/// <param name="context"></param>
		public virtual void OnCast(EnvironmentContext context)
		{
            var currentParams = context.Actor.GetAbilityParams(this.Parameters.Name);

            // if cost < 0 -> this abiliy regens resource
            if (currentParams.BaseParams.ResourceCost < 0 && context.Actor.CurrentResource < context.Actor.MaxResource)
				context.Actor.CurrentResource = Math.Min(context.Actor.MaxResource, context.Actor.CurrentResource - currentParams.BaseParams.ResourceCost);

			var abilityCost = currentParams.BaseParams.ResourceCost;
            

            if (abilityCost < 0)
                abilityCost = 0;

            if (abilityCost > 0)
                context.Actor.CurrentResource -= abilityCost;

            LastExecutedAt = context.CurrentTime;

			if (currentParams.BaseParams.DmgCoeff > 0)
			{
				if (currentParams.BaseParams.Ticks == 1)
					context.ApplyDamage(this);
				else if (currentParams.BaseParams.IsMultihit)
					context.ApplyMultiDamage(this);
			}
		}

		/// <summary>
		/// Fired on ability damage
		/// </summary>
		/// <param name="context"></param>
		public virtual void OnDamage(EnvironmentContext context)
		{ 

		}

        public bool IsOnCd(int currTime)
        {
            return currTime - LastExecutedAt < Parameters.CoolDown * 1000 && LastExecutedAt >= 0;
        }

        public AbilityParams CopyParams()
        {
			AbilityParams p = Parameters.GetCopy();

            return p;
        }
	}
}
