using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	public class Buff
	{
        public Buff()
        {
            Effects = new List<BuffEffect>();
        }

		public string Name { get; set; }

        /// <summary>
        /// Buff durtion. 0 = infinity
        /// </summary>
		public int DurationSec { get; set; }

		/// <summary>
		/// For ex. ticks interval for debuffs
		/// </summary>
		public double ActionInterval { get; set; }

        /// <summary>
        /// Max Stacks
        /// </summary>
        public int MaxStack { get; set; }

        // TODO: affected abilities
        public List<BuffEffect> Effects { get; set; }

	}

    public class BuffEffect
    {
        public BuffEffect()
        {
            Condition = (ec) => true;
            IsAppliedTo = (name) => true;
        }

        /// <summary>
        /// Affects SkillDamage part
        /// </summary>
        public double SkillDamageModPercent { get; set; }

        /// <summary>
        /// Affects total damage part
        /// </summary>
        public double TotalDamageModPercent { get; set; }

        /// <summary>
        /// Affects resource cost of the skill
        /// </summary>
        public int ResourceCost { get; set; }

        public virtual Func<string, bool> IsAppliedTo { get; set; }

        /// <summary>
        /// Condition
        /// </summary>
        public virtual Func<EnvironmentContext, bool> Condition { get; set; }
    }
}
