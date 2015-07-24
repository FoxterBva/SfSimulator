using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
    /// <summary>
    /// Buff info with state
    /// </summary>
	public class ActorBuff
	{
		public ActorBuff(Buff buff)
		{
            this.buff = buff;
			Stacks = 1;
		}

		/// <summary>
		/// Buff name
		/// </summary>
		//public string Name { get; set; }

		/// <summary>
		/// Time of buff aplication
		/// </summary>
		public int StartTime { get; set;}

		/// <summary>
		/// If &lt; 0 => infinite
		/// </summary>
		public int EndTime { get; set; }

		/// <summary>
		/// Stacks
		/// </summary>
		public int Stacks { get; set; }

        Buff buff;
        public Buff Buff { get { return buff; } }

		/// <summary>
		/// State of ability parameters on buff application
		/// </summary>
		public ExtendedAbilityParams AbilityParams { get; set; }

		/// <summary>
		/// Actor stats
		/// </summary>
		public ActorStats ActorStats { get; set; }
	}
}
