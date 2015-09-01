using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkfrgSimCommon.Model;
using SkfrgSimCommon.Classes;

namespace SkfrgSimCommon
{
	public class ActorFactory
	{
		public Actor CreateActor(string actorClass, ActorStats stats, EnvironmentContext context)
		{
			if (actorClass == ClassNames.Paladin)
			{
				return new Paladin(context, stats);
			}
			else if (actorClass == ClassNames.Archer)
			{
				return new Archer(context, stats);
			}
			else if (actorClass == ClassNames.Priest)
			{
				return new GuardianOfLight(context, stats);
			}

			return null;
		}
	}
}
