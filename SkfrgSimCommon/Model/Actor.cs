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

		public double TotalDamage { get; set; }

		public Actor(EnvironmentContext context, ActorStats stats)
		{
			eContext = context;
			pStats = stats;
			Abilities = new Dictionary<string, Ability>();
			Buffs = new List<ActorBuff>();
            Reset();
		}

		public void Reset()
		{
			IsImpulseAvailable = true;
			CurrentResource = MaxResource;
			previousUsedAbility = null;
            CurrentHp = MaxHp;
			CombatlogHistory = new List<LogEvent>();
		}

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
            ability.OnCastStart(eContext);
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

		protected Ability previousUsedAbility = null;

		public bool IsImpulseAvailable { get; set; }
		public double MaxResource { get; set; }
		public double CurrentResource { get; set; }
		public int ResourceRechargeRate { get; set; }
		public double ResourceRechargeValue { get; set; }

        public double MaxHp { get; set; }
        public double CurrentHp { get; set; }

		/// <summary>
		/// Actor's ability and it's state
		/// </summary>
		public Dictionary<string, Ability> Abilities { get; set; }

		/// <summary>
		/// Actor's buff and its state
		/// </summary>
		public List<ActorBuff> Buffs { get; set; }

        /// <summary>
        /// Gets ability params affected by actor's buffs
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
