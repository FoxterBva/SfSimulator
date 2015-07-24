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

        public AbilityDmg GetAbilityDmg(ExtendedAbilityParams ability, double hpRatio, Actor actor)
        {
            return GetAbilityDmg(ability, actor.pStats, hpRatio, actor.IsImpulseAvailable);
        }

		public AbilityDmg GetAbilityDmg(ExtendedAbilityParams ability, ActorStats stats, double hpRatio, bool IsImpulseAvailable)
		{
			double AdditionalAbilityDamage = ability.AdditionalAbilityDamage;
			double AdditionalAbilityDamageMod = ability.AbilityBonusDmgCoeff;
            double TotalDamageMod = ability.TotalBonusDmgCoeff;	

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
			bool isCrit = stats.CritChancePercent > 0 && rnd.NextDouble() * 100 <= stats.CritChancePercent;
			bool isTestinessed = stats.TestinessPercent > 0 && rnd.NextDouble() * 100 <= stats.TestinessPercent;
			bool isCrushing = stats.CrushingChancePercent > 0 && rnd.NextDouble() * 100 <= stats.CrushingChancePercent;

			var currentBaseDmg = minBase + rnd.NextDouble() * (maxBase - minBase);
			var currentCritDmg = isCrit ? Constants.CritCoeff * stats.Lucky * (1 + stats.LuckyBonus) : 0;
			var currentAddDmg = isTestinessed ? maxAddDmg : hpRatio * maxAddDmg;
			var currentImpulseDmg = IsImpulseAvailable && ability.BaseParams.IsUseImpulse ? stats.Spirit * (1 + 0.01 * stats.ImpulsePercent + stats.SpiritBonus) * ability.BaseParams.ImpulseDmgCoeff : 0;

			res.isCritical = isCrit;
			res.isTestinessed = isTestinessed;
			res.isCrushing = isCrushing;
			res.isImpulse = IsImpulseAvailable && ability.BaseParams.IsUseImpulse;
			res.Damage =
				 (currentBaseDmg * (isCrushing ? 2 : 1) + currentAddDmg + currentCritDmg) * ability.BaseParams.DmgCoeff / ability.BaseParams.Ticks * AdditionalAbilityDamageMod +
				  AdditionalAbilityDamage +
				  currentImpulseDmg;

			res.Damage *= TotalDamageMod;
			res.ImpulseDamage = currentImpulseDmg * TotalDamageMod;

			return res;
		}
	}

	public class AbilityDmg
	{
		/// <summary>
		/// Total damage (includes impulse part)
		/// </summary>
		public double Damage { get; set; }

		/// <summary>
		/// Impulse related part of the damage
		/// </summary>
		public double ImpulseDamage { get; set; }
		public bool isCritical { get; set; }
		public bool isTestinessed { get; set; }
		public bool isCrushing { get; set; }
        public bool isImpulse { get; set; }
	}
}
