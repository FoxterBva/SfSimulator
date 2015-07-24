using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	public class AbilityParams
	{
        public AbilityParams()
        {
			Ticks = 1;
        }

		public string Name { get; set; }

		/// <summary>
		/// Damage coefficient of the ability
		/// </summary>
		public double DmgCoeff { get; set; }

		/// <summary>
		/// Coefficient of the impulse damage
		/// </summary>
		public double ImpulseDmgCoeff { get; set; }

		/// <summary>
		/// Ability cost
		/// </summary>
		public double ResourceCost { get; set; }

		/// <summary>
		/// Number of ticks of ability (for ex. for channeling/multihit abilities)
		/// </summary>
		public double Ticks { get; set; }

		/// <summary>
		/// Delay between ability cast and dmg apply (in ms)
		/// </summary>
		public int DmgDelay { get; set; }


		/// <summary>
		/// Delay between ticks (in ms)
		/// </summary>
		public int TickDelay { get; set; } 

        /// <summary>
        /// Ability cooldown (in sec) 
        /// </summary>
		public int CoolDown { get; set; }

		/// <summary>
		/// Ability has multiple ticks (like channel ability)
		/// </summary>
		public bool IsMultihit { get; set; }

        /// <summary>
        /// Total cast time (in ms)
        /// </summary>
		public int TotalCastTime { get; set; }  

		public bool IsUseImpulse { get; set; }

		public AbilityParams GetCopy()
		{
			return new AbilityParams()
			{
				CoolDown = CoolDown,
				DmgCoeff = DmgCoeff,
				DmgDelay = DmgDelay,
				ImpulseDmgCoeff = ImpulseDmgCoeff,
				IsUseImpulse = IsUseImpulse,
				Name = Name,
				ResourceCost = ResourceCost,
				TotalCastTime = TotalCastTime,
				TickDelay = TickDelay,
				Ticks = Ticks,
				IsMultihit = IsMultihit
			};
		}
	}

	public class ExtendedAbilityParams 
	{
		public ExtendedAbilityParams(AbilityParams param)
		{
			AbilityBonusDmgCoeff = 1;
			TotalBonusDmgCoeff = 1;
			BaseParams = param;
		}

		public AbilityParams BaseParams { get; set; }

		public double AbilityBonusDmgCoeff { get; set; }

		public double TotalBonusDmgCoeff { get; set; }

		public double AdditionalAbilityDamage { get; set; }
	}
}
