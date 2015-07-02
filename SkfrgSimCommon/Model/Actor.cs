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
		public ActorStats pStats;
		Calculator calc;

		double totalDamage = 0;
        public double TotalDamage { get; set; }

		public Actor(EnvironmentContext context, ActorStats stats, Calculator clc)
		{
			eContext = context;
			pStats = stats;
			calc = clc;
			Abilities = new Dictionary<string, Ability>();
			Buffs = new List<ActorBuff>();
            Reset();
		}

        [Obsolete]
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
				var ability = GetByName(SelectAbility(eContext));

				var dmg = calc.GetAbilityDmg(ability.Parameters, pStats, eContext.TargetHpRatio, IsImpulseAvailable);
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
				LastAbilityUsedCd = ability.Parameters.TotalCastTime;

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
            CurrentHp = MaxHp;
            UsedAbilitiesHistory = new List<string>();
		}

		string FormatTime(double time)
		{
			return String.Format("[{0:000.00}]", (double)time / 1000);
		}

		protected Ability previousUsedAbility = null;

		/// <summary>
		/// Override this method to set specific rotation
		/// </summary>
		protected abstract string SelectAbility(EnvironmentContext context);

		protected Ability UseAbility(string name, int time)
		{
			var ability = GetByName(name);
			return UseAbility(ability, time);
		}

        protected Ability UseAbility(Ability ability, int time)
        {
            ability.OnCast(eContext);
            previousUsedAbility = ability;

            var currentParams = GetAbilityParams(ability.Parameters.Name);
            eContext.Events.Add(new SimEvent() { Time = time + currentParams.TotalCastTime, Callback = eContext.SelectAbility, Priority = EventPriority.AbilitySelect, Type = EventType.AbilitySelect });

            UsedAbilitiesHistory.Add(ability.Parameters.Name);

            return ability;
        }

		protected Ability GetByName(string name)
		{
			return Abilities[name];
		}

        public void SelectAbility(int time)
        {
            var ability = SelectAbility(eContext);
            
            UseAbility(ability, time);
        }

        public void GainResource(int time)
        {
            if (CurrentResource < MaxResource)
            {
                CurrentResource = Math.Min(MaxResource, CurrentResource + ResourceRechargeValue);
            }
            eContext.AddGainResource(time + ResourceRechargeRate);
        }

		public bool IsImpulseAvailable { get; set; }
		public double MaxResource { get; set; }
		public double CurrentResource { get; set; }
		public int ResourceRechargeRate { get; set; }
		public double ResourceRechargeValue { get; set; }
		public int LastResourceRechargeAt { get; set; }

        public double MaxHp { get; set; }
        public double CurrentHp { get; set; }

		public int LastImpulseUsedAt { get; set; }
		public int LastAbilityUsedAt { get; set; }
		public int LastAbilityUsedCd { get; set; }

		public Dictionary<string, Ability> Abilities { get; set; }

        /// <summary>
        /// Gets ability params affected by buffs
        /// </summary>
        public AbilityParams GetAbilityParams(string abilityName)
        {
            var ability = Abilities[abilityName];
            AbilityParams res = ability.CopyParams();

            foreach (var buff in Buffs)
            {
                foreach (var eff in buff.Buff.Effects)
                {
                    if (eff.IsAppliedTo(res.Name) && eff.Condition(eContext))
                    {
                        if (eff.ResourceCost != 0)
                        {
                            res.ResourceCost = res.ResourceCost + eff.ResourceCost;
                            if (res.ResourceCost < 0)
                                res.ResourceCost = 0;
                        }

                        if (eff.SkillDamageModPercent > 0)
                            res.AbilityBonusDmgCoeff = res.AbilityBonusDmgCoeff * (1 + 0.01 * eff.SkillDamageModPercent * buff.Stacks);

                        if (eff.TotalDamageModPercent > 0)
                            res.TotalBonusDmgCoeff = res.TotalBonusDmgCoeff * (1 + 0.01 * eff.TotalDamageModPercent * buff.Stacks);
                    }
                }
            }

            return res;
        }

        public List<ActorBuff> Buffs { get; set; }
        public List<string> UsedAbilitiesHistory { get; set; }
	}
}
