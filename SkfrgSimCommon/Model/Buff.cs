using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	public class Buff
	{
		public string Name { get; set; }
		public double DurationSec { get; set; }

		/// <summary>
		/// For ex. ticks interval for debuffs
		/// </summary>
		public double ActionInterval { get; set; }

		// TODO: affected abilities
	}
}
