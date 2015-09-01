using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	/// <summary>
	/// Represents parameters of the ability.
	/// </summary>
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
		/// Delay between ability cast and dmg apply (in ms) (cast time)
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

		/// <summary>
		/// Returns copy of this class
		/// </summary>
		public AbilityParams GetCopy()
		{
			return new AbilityParams()
			{
				Name = Name,
				DmgCoeff = DmgCoeff,
				ImpulseDmgCoeff = ImpulseDmgCoeff,
				ResourceCost = ResourceCost,
				Ticks = Ticks,
				DmgDelay = DmgDelay,
				TickDelay = TickDelay,
				CoolDown = CoolDown,
				IsMultihit = IsMultihit,
				TotalCastTime = TotalCastTime,
				IsUseImpulse = IsUseImpulse,
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

		/// <summary>
		/// Bonus modificator aopplied to the ability damage (doesn't affect impulse and additional ability damage)
		/// </summary>
		public double AbilityBonusDmgCoeff { get; set; }

		/// <summary>
		/// Bonus modificator applied to the total damage (affects impulse too)
		/// </summary>
		public double TotalBonusDmgCoeff { get; set; }

		/// <summary>
		/// Bonus value applied to the ability damage
		/// </summary>
		public double AdditionalAbilityDamage { get; set; }
	}
}
