using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	public class ActorBuff
	{
		public ActorBuff()
		{
			Stacks = 1;
		}

		/// <summary>
		/// Buff name
		/// </summary>
		public string Name { get; set; }

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
	}
}
