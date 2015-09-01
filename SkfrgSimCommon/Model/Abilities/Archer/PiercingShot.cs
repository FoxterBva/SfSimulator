using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model.Abilities.Archer
{
	public class PiercingShot : Ability
	{
		public PiercingShot()
			: base()
		{
            Parameters = new AbilityParams();
			Parameters.Name = AbilityNames.Archer.PiercingShot;
		}

		public override void OnCastStart(EnvironmentContext context)
		{
			base.OnCastStart(context);

			// Если талант + нет дебаффа + огнедот -> снять огнедот и увеличить урон.
			var fireDot = context.Actor.Buffs.FirstOrDefault(b => b.Buff.Name == BuffNames.Archer.BurningDot);
			if (fireDot != null && fireDot.EndTime - context.CurrentTime >= Parameters.DmgDelay)
			{
			    // context.ApplyBuff(null, Parameters.Name);	// бафф увеличения дамага пронзающего
			}
		}

		public override void OnDamage(EnvironmentContext context)
		{
			base.OnDamage(context);

			var fireDot = context.Actor.Buffs.FirstOrDefault(b => b.Buff.Name == BuffNames.Archer.BurningDot);
			if (fireDot != null)
			{
				context.RemoveBuff(fireDot);
				// context.ApplyBuff(null, Parameters.Name);	// дебафф

				// TODO:
				//context.RemoveBuff(null);	// бафф увеличения дамага пронзающего
			}
		}
	}
}
