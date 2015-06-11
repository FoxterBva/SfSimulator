using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon.Model
{
	public class EnvironmentContext
	{
		public EnvironmentContext()
		{
			
		}

		public void Reset()
		{
			TargetCurrentHp = TargetMaxHp;
		}

		public double TargetMaxHp { get; set; }
		public double TargetCurrentHp { get; set; }

		public double TargetHpRatio {
			get { return TargetMaxHp == 0 ? 0 : (TargetCurrentHp / TargetMaxHp); }
		}

		public Actor Actor { get; set; }
		public Actor Target { get; set; }

		public void ApplyBuffToSource(object buff)
		{
			
		}

		public void ApplyBuffToTarget(object buff)
		{

		}

		public void DamageTarget(Ability ability)
		{ }
	}
}
