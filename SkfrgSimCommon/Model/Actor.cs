using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	/// <summary>
	/// Represents general actor
	/// </summary>
	public abstract class Actor : IActor
	{
		EnvironmentContext eContext;
		ActorStats pStats;
		Calculator calc;

		double totalDamage = 0;
		public double TotalDamage { get { return totalDamage; } }

		public List<ActorBuff> Buffs { get; set; }

		public Actor(EnvironmentContext context, ActorStats stats, Calculator clc)
		{
			eContext = context;
			pStats = stats;
			calc = clc;
			Abilities = new Dictionary<string, Ability>();
			Buffs = new List<ActorBuff>();
		}

		public void Tick(int time, IDependencyFactory factory)
		{
			var logger = factory.GetLogger();

			if (!IsImpulseAvailable && time - LastImpulseUsedAt >= 10000 * (1 - 0.01 * pStats.ImpulsePercent))
			{
				LastImpulseUsedAt = 0;
				IsImpulseAvailable = true;
				logger.Log(FormatTime(time) + " Impulse refreshed");
			}

			if (time - LastResourceRechargeAt >= ResourceRechargeRate && MaxResource > CurrentResource)
			{
				LastResourceRechargeAt = time;
				CurrentResource = Math.Min(MaxResource, CurrentResource + ResourceRechargeValue);
				logger.Log(FormatTime(time) + String.Format("[{0,3}] Gain resource", CurrentResource));
			}

			// Hit
			if (LastAbilityUsedAt + LastAbilityUsedCd <= time)
			{
				var ability = SelectAbility(eContext);

				var dmg = calc.GetAbilityDmg(ability.Parameters, pStats, eContext, IsImpulseAvailable);
				totalDamage += dmg.Damage;
				eContext.TargetCurrentHp -= dmg.Damage;
				CurrentResource = Math.Min(MaxResource, CurrentResource - ability.Parameters.ResourceCost);

				if (CurrentResource < 0)
					throw new InvalidOperationException("Current Resource couldn't become negative.");

				bool impulseUsed = false;
				if (ability.Parameters.IsUseImpulse && IsImpulseAvailable)
				{
					IsImpulseAvailable = false;
					LastImpulseUsedAt = time;
					impulseUsed = true;
				}

				LastAbilityUsedAt = time;
				LastAbilityUsedCd = ability.Parameters.CastTime;

				var dmgStr = dmg.Damage.ToString("F0");
				if (dmg.isCritical)
					dmgStr = "*" + dmgStr + "*";
				if (dmg.isCrushing)
					dmgStr = "^" + dmgStr + "^";
				if (dmg.isTestinessed)
					dmgStr = "+" + dmgStr + "+";  

				logger.Log(FormatTime(time) + String.Format("[{6,3}] hits target by {0} with \"{1}\" ({2}){3}. Target hp is: {4:0}/{5:0}.", dmgStr, ability.Parameters.Name, impulseUsed ? "impulse" : "", "", eContext.TargetCurrentHp, eContext.TargetMaxHp, CurrentResource));

				ability.OnCast(eContext);
			}
		}

		public void Reset()
		{
			IsImpulseAvailable = true;
			CurrentResource = MaxResource;
			previousUsedAbility = null;
			totalDamage = 0;
			LastImpulseUsedAt = 0;
			LastAbilityUsedAt = 0;
			LastAbilityUsedCd = 0;
			LastResourceRechargeAt = 0;
		}

		string FormatTime(double time)
		{
			return String.Format("[{0:000.00}]", (double)time / 1000);
		}

		protected Ability previousUsedAbility = null;

		/// <summary>
		/// Override this method to set specific rotation
		/// </summary>
		protected abstract Ability SelectAbility(EnvironmentContext context);

		protected Ability UseAbility(string name)
		{
			var ability = GetByName(name);
			previousUsedAbility = ability;
			return ability;
		}

		protected Ability GetByName(string name)
		{
			return Abilities[name];
		}

		public bool IsImpulseAvailable { get; set; }
		public double MaxResource { get; set; }
		public double CurrentResource { get; set; }
		public int ResourceRechargeRate { get; set; }
		public double ResourceRechargeValue { get; set; }
		public int LastResourceRechargeAt { get; set; }

		public int LastImpulseUsedAt { get; set; }
		public int LastAbilityUsedAt { get; set; }
		public int LastAbilityUsedCd { get; set; }

		public Dictionary<string, Ability> Abilities { get; set; }
	}
}
