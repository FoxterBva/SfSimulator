using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	public class EnvironmentContext
	{
        Calculator calc = new Calculator();
        ILogger logger;
        EventProcessor eventProcessor;

		public EnvironmentContext(ILogger log)
		{
            logger = log;
            Reset();
		}

		public void Reset()
		{
            if(Target != null)
                Target.Reset();

            eventProcessor = new EventProcessor(this);

            Events = new List<SimEvent>() { new SimEvent() { Time = 0, Callback = SelectAbility, Priority = EventPriority.AbilitySelect, Type = EventType.AbilitySelect }, new SimEvent() { Time = 0, Callback = GainResource, Priority = EventPriority.ResourceGain, Type = EventType.ResourceGain } };
		}

        public double TargetMaxHp { get { return Target.MaxHp; } }
        public double TargetCurrentHp { get { return Target.CurrentHp; } set { Target.CurrentHp = value; } }

		public double TargetHpRatio {
			get { return TargetMaxHp == 0 ? 0 : (TargetCurrentHp / TargetMaxHp); }
		}

		public Actor Actor { get; set; }
		public Actor Target { get; set; }

		public void ApplyBuffToSource(object buff)
		{
			
		}

		public void ApplyBuffToTarget(object buff)
		{

		}

		public void DamageTarget(Ability ability)
		{ }

        public List<SimEvent> Events { get; set; }

        public SimEvent GetEvent()
        {
            Events.Sort();
            var evt = Events.First();
            CurrentTime = evt.Time;

            return evt;
        }

        public SimEvent ProcessNextEvent()
        {
            var evt = GetEvent();

            eventProcessor.ProcessEvent(evt);

            Events.Remove(evt);

			return evt;
        }

        #region EventActions

        public int CurrentTime { get; set; }

        public void SelectAbility(int time)
        {
            Actor.SelectAbility(time);
        }

		public void AddSelectAbility(int nextTime)
		{
			Events.Add(new SimEvent() { Time = nextTime, Callback = SelectAbility, Priority = EventPriority.AbilitySelect, Type = EventType.AbilitySelect });
		}

        public void DeleteBuff()
        { }

        public void AddGainResource(int newTime)
        {
            Events.Add(new SimEvent() { Time = newTime, Callback = GainResource, Priority = EventPriority.ResourceGain, Type = EventType.ResourceGain });
        }

        public void GainResource(int time)
        {
            Actor.GainResource(time);
			Actor.AddHistoryEvent(new LogEvent(CurrentTime, LogEventType.ResourceTick, "ResourceTick", null));
            LogInfo(String.Format("({0}) тик востановления ресурса. ", Actor.CurrentResource.ToString().PadLeft(3, '0')));
        }

        public void ApplyBuff(Buff buff, string abilityName)
        {
			ExtendedAbilityParams ap = null;
			if (!String.IsNullOrEmpty(abilityName))
			{ 
				ap = Actor.GetAbilityParams(abilityName);
			}

            var existed = Actor.Buffs.FirstOrDefault(b => b.Buff.Name == buff.Name);
            if (existed == null)
            {
				// it is a new buff
				var abuff = new ActorBuff(buff) { StartTime = CurrentTime, AbilityParams = ap,ActorStats = Actor.pStats };
				Actor.Buffs.Add(abuff);
				if (buff.DurationSec > 0)
				{
					Events.Add(new SimEvent()
					{
						Time = CurrentTime + buff.DurationSec * 1000,
						Callback = (t) => RemoveBuff(abuff),
						Priority = EventPriority.RemoveBuff,
						Type = EventType.RemoveBuff,
						Parameter = abuff.Buff.Name
					});
					abuff.EndTime = CurrentTime + buff.DurationSec * 1000;
				}

				if (buff.IsDot)
				{
					Events.Add(new SimEvent() 
					{
						Time = CurrentTime + abuff.AbilityParams.BaseParams.DmgDelay,
						Priority = EventPriority.Damage,
						Type = EventType.DotTick,
						Parameter = abuff.Buff.Name,
						Callback = (t) => ApplyDotDamage(abuff)
					});
				}

				LogInfo("Получен эффект " + buff.Name);
				Actor.AddHistoryEvent(new LogEvent(CurrentTime, LogEventType.BuffGain, buff.Name, null));
            }
            else
            {
				if (existed.Stacks < existed.Buff.MaxStack)
				{
					existed.Stacks++;
					existed.AbilityParams = ap;
					existed.ActorStats = Actor.pStats;

					LogInfo("Получен стак " + existed.Stacks.ToString() + " для " + buff.Name);
					Actor.AddHistoryEvent(new LogEvent(CurrentTime, LogEventType.BuffRefresh, buff.Name, null));
				}

				if (buff.DurationSec > 0)
				{
					// extend buff duration
					var delEvent = Events.FirstOrDefault(e => e.Priority == EventPriority.RemoveBuff && e.Parameter == buff.Name);
					if (delEvent != null)
					{
						delEvent.Time = CurrentTime + buff.DurationSec * 1000;
					}
					existed.EndTime = delEvent.Time;
					LogInfo("Обновлена длительность эффекта " + buff.Name);
					Actor.AddHistoryEvent(new LogEvent(CurrentTime, LogEventType.BuffRefresh, buff.Name, null));
				}
            }
        }

        public void RemoveBuff(ActorBuff buff)
        {
            Actor.Buffs.Remove(buff);
			
			// Remove all related events of this buff
			var relatedEvents = Events.Where(e => e.Parameter == buff.Buff.Name).ToList();
			relatedEvents.ForEach(e => Events.Remove(e));
            LogInfo("Потерян эффект " + buff.Buff.Name);
			Actor.AddHistoryEvent(new LogEvent(CurrentTime, LogEventType.BuffLose, buff.Buff.Name, null));
        }

        public void ApplyDamage(Ability a)
        {
            var currentParams = Actor.GetAbilityParams(a.Parameters.Name);

            ApplyDamage(currentParams);
			a.OnDamage(this);
        }

		public void ApplyDotDamage(ActorBuff buff)
		{
			var ap = buff.AbilityParams;

			var dmg = calc.GetAbilityDmg(ap, buff.ActorStats, this.TargetHpRatio, false);
			TargetCurrentHp -= dmg.Damage;

			var dmgStr = dmg.Damage.ToString("F0");
			if (dmg.isCritical)
				dmgStr = "*" + dmgStr + "*";
			if (dmg.isCrushing)
				dmgStr = "^" + dmgStr + "^";
			if (dmg.isTestinessed)
				dmgStr = "+" + dmgStr + "+";

			LogInfo(String.Format("({3}) ## {0} (тик) на {1}{4}. Hp:{2}", ap.BaseParams.Name, dmgStr, TargetCurrentHp.ToString("F0"), Actor.CurrentResource.ToString().PadLeft(3, '0'), dmg.isImpulse ? String.Format(" ({0:F0} + {1} импульс)", dmg.Damage - dmg.ImpulseDamage, dmg.ImpulseDamage) : ""));
			Actor.AddHistoryEvent(new LogEvent(CurrentTime, LogEventType.AbilityDotDamage, ap.BaseParams.Name, dmg));
			Actor.TotalDamage += dmg.Damage;

			Events.Add(new SimEvent()
			{
				Time = CurrentTime + ap.BaseParams.TickDelay,
				Priority = EventPriority.Damage,
				Type = EventType.DotTick,
				Parameter = buff.Buff.Name,
				Callback = (t) => ApplyDotDamage(buff)
			});
		}

        public void ApplyDamage(ExtendedAbilityParams ap)
        {
            this.Events.Add(new SimEvent()
            {
				Time = CurrentTime + ap.BaseParams.DmgDelay,
                Priority = EventPriority.Damage,
                Type = EventType.AbilityDamage,
				Parameter = ap.BaseParams.Name,
                Callback = (t) =>
                    {
                        var dmg = calc.GetAbilityDmg(ap, this.TargetHpRatio, this.Actor);
                        TargetCurrentHp -= dmg.Damage;

                        var dmgStr = dmg.Damage.ToString("F0");
                        if (dmg.isCritical)
                            dmgStr = "*" + dmgStr + "*";
                        if (dmg.isCrushing)
                            dmgStr = "^" + dmgStr + "^";
                        if (dmg.isTestinessed)
                            dmgStr = "+" + dmgStr + "+";

						LogInfo(String.Format("({3}) ## {0} на {1}{4}. Hp:{2}", ap.BaseParams.Name, dmgStr, TargetCurrentHp.ToString("F0"), Actor.CurrentResource.ToString().PadLeft(3, '0'), dmg.isImpulse ? String.Format(" ({0:F0} + {1} импульс)", dmg.Damage - dmg.ImpulseDamage, dmg.ImpulseDamage) : ""));
						Actor.AddHistoryEvent(new LogEvent(CurrentTime, LogEventType.AbilityDamage, ap.BaseParams.Name, dmg));
                        Actor.TotalDamage += dmg.Damage;

                        if (dmg.isImpulse)
                        {
                            Actor.IsImpulseAvailable = false;
                            Events.Add(new SimEvent() 
                            {
                                Time = t + (int)(10000 * (1 - 0.01 * Actor.pStats.ImpulsePercent)),
                                Priority = EventPriority.RefreshImpulse,
                                Type = EventType.ImpulseRefresh,
                                Callback = RefreshImpulse
                            });
                        }
                    }
            });
        }

		public void ApplyMultiDamage(Ability a)
		{
			var currentParams = Actor.GetAbilityParams(a.Parameters.Name);

			double totalSumm = 0;
			for (int i = 0; i < currentParams.BaseParams.Ticks; i++)
			{
				var ind = i;
				var dmg = calc.GetAbilityDmg(currentParams, this.TargetHpRatio, this.Actor);
				var summ = totalSumm + dmg.Damage;
				totalSumm += dmg.Damage;

				this.Events.Add(new SimEvent()
				{
					Time = CurrentTime + currentParams.BaseParams.DmgDelay + i * currentParams.BaseParams.TickDelay,
					Priority = EventPriority.Damage,
					Type = EventType.AbilityDamage,
					Parameter = a.Parameters.Name,
					Callback = (t) =>
					{
						TargetCurrentHp -= dmg.Damage;
						var dmgStr = dmg.Damage.ToString("F0");
						if (dmg.isCritical)
							dmgStr = "*" + dmgStr + "*";
						if (dmg.isCrushing)
							dmgStr = "^" + dmgStr + "^";
						if (dmg.isTestinessed)
							dmgStr = "+" + dmgStr + "+";

						LogInfo(String.Format("({3}) ## {0}({5}) на {1}{4} (total: {6}). Hp:{2}", currentParams.BaseParams.Name, dmgStr, TargetCurrentHp.ToString("F0"), Actor.CurrentResource.ToString().PadLeft(3, '0'), dmg.isImpulse ? String.Format(" ({0:F0} + {1} импульс)", dmg.Damage - dmg.ImpulseDamage, dmg.ImpulseDamage) : "", ind, summ.ToString("F0")));
						Actor.AddHistoryEvent(new LogEvent(CurrentTime, LogEventType.AbilityDamage, currentParams.BaseParams.Name, dmg));
						Actor.TotalDamage += dmg.Damage;

						if (dmg.isImpulse)
						{
							Actor.IsImpulseAvailable = false;
							Events.Add(new SimEvent()
							{
								Time = t + (int)(10000 * (1 - 0.01 * Actor.pStats.ImpulsePercent)),
								Priority = EventPriority.RefreshImpulse,
								Type = EventType.ImpulseRefresh,
								Callback = RefreshImpulse
							});
						}
					}
				});
			}
		}

        void RefreshImpulse(int time)
        {
            Actor.IsImpulseAvailable = true;
            LogInfo("Импульс обновлен");
			Actor.AddHistoryEvent(new LogEvent(CurrentTime, LogEventType.ImpulseRefresh, "ImpulseRefresh", null));
        }

        #endregion

        string FormatTime(double time)
        {
            return String.Format("[{0:000.00}]", (double)time / 1000);
        }

        void LogInfo(string msg)
        {
            if (logger != null)
                logger.Log(FormatTime(CurrentTime) + " " + msg);
        }
    }
}
