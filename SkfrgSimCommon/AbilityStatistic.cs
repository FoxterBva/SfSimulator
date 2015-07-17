using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon
{
	public class AbilityStatistic
	{
		public AbilityStatistic()
		{
			Damage = new List<double>();
		}

		public double TotalAbilityDamage { get; set; }
		public double TotalAbilityNonImpulseDamage { get; set; }
		public double Uses { get; set; }
		public double Crits { get; set; }
		public double Crushes { get; set; }
		public double Testinesses { get; set; }
		public double Impulses { get; set; }
		public double MinDmg { get; set; }
		public double MaxDmg { get; set; }
		public double AvrgDmg { get; set; }
		public double MinNonImpulseDmg { get; set; }
		public double MaxNonImpulseDmg { get; set; }
		public double AvrgNonImpulseDmg { get; set; }
		public List<double> Damage { get; set; }
	}
}
