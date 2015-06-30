using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	public class AbilityParams
	{
        public AbilityParams()
        {
            AbilityBonusDmgCoeff = 1;
            TotalBonusDmgCoeff = 1;
        }

		public string Name { get; set; }
		public double DmgCoeff { get; set; }
		public double ImpulseDmgCoeff { get; set; }
		public double ResourceCost { get; set; }

        /// <summary>
        /// Ability cooldown (in sec) 
        /// </summary>
		public int CoolDown { get; set; }

        /// <summary>
        /// Total cast time in MS
        /// </summary>
		public int TotalCastTime { get; set; }  // Total cast time of the ability
		public bool IsUseImpulse { get; set; }

        /// <summary>
        /// Delay between ability cast and dmg apply (in MS)
        /// </summary>
        public int DmgDelay { get; set; }

		// TODO: some additional effects
        public double AbilityBonusDmgCoeff { get; set; }
        public double TotalBonusDmgCoeff { get; set; }
	}
}
