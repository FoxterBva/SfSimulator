using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkfrgSimCommon.Model;

namespace SkfrgSimCommon
{
	public class Calculator
	{
		Random rnd;

		public Calculator()
		{
			rnd = new Random();
		}

		public AbilityDmg GetAbilityDmg(AbilityParams ability, ActorStats stats, EnvironmentContext context, bool IsImpulseAvailable)
		{
			double AdditionalAbilityDamage = 0;	// TODO: buffs from amulets
			double AdditionalAbilityDamagePercent = 0;	// TODO: % buffs to ability damage
			double TotalDamagePercent = 0;	// TODO: % buffs to the total damage output;

			var res = new AbilityDmg();

			// Базовое могущество + массивность
			var might = stats.Might + stats.Stamina * (0.01 * stats.SolidityPercent);

			// мин./макс. базовый урон
			var minBase = might * Constants.MightCoeff;
			var maxBase = might * (0.1 + 0.1 - Constants.MightCoeff) + Constants.StrCoeff * stats.Str * (1 + 0.01 * stats.PrecisePercent + stats.StrBonus);

			var deltaBase = maxBase - minBase;

			// мин. урон с учетом точности
			minBase += deltaBase * 0.01 * stats.PrecisePercent;

			// верхняя граница доп. урона с учетом вспыльчивости
			var maxAddDmg = Constants.BraveCoeff * stats.Brave * (1 + 0.01 * stats.TestinessPercent + stats.BraveBonus);

			// проки
			bool isCrit = rnd.NextDouble() * 100 <= stats.CritChancePercent;
			bool isTestinessed = rnd.NextDouble() * 100 <= stats.TestinessPercent;
			bool isCrushing = rnd.NextDouble() * 100 <= stats.CrushingChancePercent;

			var currentBaseDmg = minBase + rnd.NextDouble() * (maxBase - minBase);
			var currentCritDmg = isCrit ? stats.Lucky * (1 + stats.LuckyBonus) : 0;
			var currentAddDmg = isTestinessed ? maxAddDmg : context.TargetHpRatio * maxAddDmg;
			var currentImpulseDmg = IsImpulseAvailable && ability.IsUseImpulse ? stats.Spirit * (1 + 0.01 * stats.ImpulsePercent + stats.SpiritBonus) * ability.ImpulseDmgCoeff : 0;

			res.AbilityName = ability.Name;
			res.isCritical = isCrit;
			res.isTestinessed = isTestinessed;
			res.isCrushing = isCrushing;
			res.Damage = 
				 (currentBaseDmg * (isCrushing ? 2 : 1) + currentAddDmg + currentCritDmg) * ability.DmgCoeff * (1 + 0.01 * AdditionalAbilityDamagePercent) +
				  AdditionalAbilityDamage +
				  currentImpulseDmg;

			res.Damage *= (1 + 0.01 * TotalDamagePercent);

			return res;
		}
	}

	public class AbilityDmg
	{
		public string AbilityName { get; set; }
		public double Damage { get; set; }
		public bool isCritical { get; set; }
		public bool isTestinessed { get; set; }
		public bool isCrushing { get; set; }
	}
}
