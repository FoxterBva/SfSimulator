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

		public void Reset()
		{
			IsImpulseAvailable = true;
			CurrentResource = MaxResource;
			previousUsedAbility = null;
			LastImpulseUsedAt = 0;
			LastAbilityUsedAt = 0;
			LastAbilityUsedCd = 0;
			LastResourceRechargeAt = 0;
            CurrentHp = MaxHp;
			CombatlogHistory = new List<LogEvent>();
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
            //eContext.Events.Add(new SimEvent() { Time = time + currentParams.TotalCastTime, Callback = eContext.SelectAbility, Priority = EventPriority.AbilitySelect, Type = EventType.AbilitySelect });
			eContext.AddSelectAbility(time + currentParams.BaseParams.TotalCastTime);

			AddHistoryEvent(new LogEvent(time, LogEventType.AbilityCast, ability.Parameters.Name, null));

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
			AddHistoryEvent(new LogEvent(time, LogEventType.ResourceTick, "", null));
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
        public ExtendedAbilityParams GetAbilityParams(string abilityName)
        {
            var ability = Abilities[abilityName];
            ExtendedAbilityParams res = new ExtendedAbilityParams(ability.CopyParams());

            foreach (var buff in Buffs)
            {
                foreach (var eff in buff.Buff.Effects)
                {
                    if (eff.IsAppliedTo(res.BaseParams.Name) && eff.Condition(eContext))
                    {
                        if (eff.ResourceCost != 0)
                        {
							res.BaseParams.ResourceCost = res.BaseParams.ResourceCost + eff.ResourceCost;
							if (res.BaseParams.ResourceCost < 0)
								res.BaseParams.ResourceCost = 0;
                        }

                        if (eff.SkillDamageModPercent > 0)
                            res.AbilityBonusDmgCoeff = res.AbilityBonusDmgCoeff * (1 + 0.01 * eff.SkillDamageModPercent * buff.Stacks);

                        if (eff.TotalDamageModPercent > 0)
                            res.TotalBonusDmgCoeff = res.TotalBonusDmgCoeff * (1 + 0.01 * eff.TotalDamageModPercent * buff.Stacks);
                    }
                }
            }

			// TODO: include additional damage from the amulets. 
			// TODO: How to process random/specific amulet buffs, like: has a 25% chance to increase xx dmg by yy. Damage of each third XX increased by YY.

            return res;
        }

		public void AddHistoryEvent(LogEvent evt)
		{
			CombatlogHistory.Add(evt);
		}

        public List<ActorBuff> Buffs { get; set; }
		public List<LogEvent> CombatlogHistory { get; set; }
	}

	public class LogEvent
	{
		public LogEvent()
		{ }

		public LogEvent(int time, LogEventType type, string abilityName, object det) 
			: this()
		{
			Time = time;
			Type = type;
			AbilityName = abilityName;
			Details = det;
		}

		public string AbilityName { get; set; }
		public int Time { get; set; }
		public LogEventType Type { get; set; }
		public object Details { get; set; }
	}

	public enum LogEventType
	{ 
		AbilityCast,
		AbilityDamage,
		AbilityDotDamage,
		BuffGain,
		BuffRefresh,
		BuffLose,
		ImpulseRefresh,
		ResourceTick
	}
}
