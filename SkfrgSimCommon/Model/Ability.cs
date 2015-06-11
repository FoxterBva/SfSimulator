using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	/// <summary>
	/// Represents ability state of the actor
	/// </summary>
	public class Ability
	{
		public Ability()
		{
			LastExecutedAt = 0;
		}

		public int LastExecutedAt { get; set; }
		
		public AbilityParams Parameters { get; set; }

		public virtual void OnCast(EnvironmentContext context)
		{

		}
	}
}
