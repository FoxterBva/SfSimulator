using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Archer
{
	public class FireShelling : Ability
	{
		public FireShelling() : base()
		{
			Parameters = new AbilityParams();
			Parameters.Name = AbilityNames.Archer.FireShelling;
			Parameters.TotalCastTime = 3000;
			Parameters.CoolDown = 0;
			Parameters.DmgCoeff = 4;
			Parameters.ImpulseDmgCoeff = 0;
			Parameters.IsUseImpulse = false;
			Parameters.ResourceCost = 200;
			Parameters.IsMultihit = true;

			// TODO:
			Parameters.Ticks = 30;
			Parameters.TickDelay = 100;
		}

		public override void OnCastStart(EnvironmentContext context)
		{
			base.OnCastStart(context);

			// Снимаем бафф бесплатного обстрела после использования
			var removeShellBuffEvent = context.Events.FirstOrDefault(e => e.Parameter == BuffNames.Archer.FireShellingBuff && e.Type == EventType.RemoveBuff);
			if (removeShellBuffEvent != null)
			{
				removeShellBuffEvent.Time = context.CurrentTime;
			}
		}
	}
}
