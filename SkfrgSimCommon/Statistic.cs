using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkfrgSimCommon
{
	public class Statistic
	{
		public Statistic()
		{
			Statistics = new Dictionary<string, AbilityStatistic>();
			appendCounts = 0;
		}

		/// <summary>
		/// Calculates average statistic between existed and provided statistics
		/// </summary>
		public void AppendToAverage(Statistic statistic)
		{ 
			
			foreach (var sKey in Statistics.Keys)
			{
				// new statistic has this key
				if (statistic.Statistics.ContainsKey(sKey))
				{
					ApplyStatistic(Statistics[sKey], statistic.Statistics[sKey], statistic.AppendCounts);
				}
				else
				{
					ApplyStatistic(Statistics[sKey], new AbilityStatistic(), statistic.AppendCounts);
				}
			}

			foreach (var nsKey in statistic.Statistics.Keys)
			{
				// new statistic has new keys.
				if (!Statistics.ContainsKey(nsKey))
				{
					Statistics.Add(nsKey, statistic.Statistics[nsKey]);
					if (appendCounts > 0)
						ApplyStatistic(Statistics[nsKey], new AbilityStatistic(), statistic.AppendCounts);
				}
			}

			appendCounts += statistic.AppendCounts;
		}

		void ApplyStatistic(AbilityStatistic currentStat, AbilityStatistic statToAppend, int countToAppend)
		{ 
			currentStat.AvrgDmg = AppendAverages(currentStat.AvrgDmg, AppendCounts + 1, statToAppend.AvrgDmg, countToAppend + 1);
			currentStat.AvrgNonImpulseDmg = AppendAverages(currentStat.AvrgNonImpulseDmg, AppendCounts + 1, statToAppend.AvrgNonImpulseDmg, countToAppend + 1);
			currentStat.Crits = AppendAverages(currentStat.Crits, AppendCounts + 1, statToAppend.Crits, countToAppend + 1);
			currentStat.Crushes = AppendAverages(currentStat.Crushes, AppendCounts + 1, statToAppend.Crushes, countToAppend + 1);
			currentStat.Impulses = AppendAverages(currentStat.Impulses, AppendCounts + 1, statToAppend.Impulses, countToAppend + 1);
			currentStat.MaxDmg = Math.Max(currentStat.MaxDmg, statToAppend.MaxDmg);
			currentStat.MaxNonImpulseDmg = Math.Max(currentStat.MaxNonImpulseDmg, statToAppend.MaxNonImpulseDmg);
			currentStat.MinDmg = Math.Max(currentStat.MinDmg, statToAppend.MinDmg);
			currentStat.MinNonImpulseDmg = Math.Max(currentStat.MinNonImpulseDmg, statToAppend.MinNonImpulseDmg);
			currentStat.Testinesses = AppendAverages(currentStat.Testinesses, AppendCounts + 1, statToAppend.Testinesses, countToAppend + 1);
			currentStat.TotalAbilityDamage = AppendAverages(currentStat.TotalAbilityDamage, AppendCounts + 1, statToAppend.TotalAbilityDamage, countToAppend + 1);
			currentStat.TotalAbilityNonImpulseDamage = AppendAverages(currentStat.TotalAbilityNonImpulseDamage, AppendCounts + 1, statToAppend.TotalAbilityNonImpulseDamage, countToAppend + 1);
			currentStat.Uses = AppendAverages(currentStat.Uses, AppendCounts + 1, statToAppend.Uses, countToAppend + 1);
		}

		/// <summary>
		/// Calculates average of two average values
		/// </summary>
		double AppendAverages(double average1, int cnt1, double average2, int cnt2)
		{
			return (average1 * cnt1 + average2 * cnt2) / (cnt1 + cnt2);
		}

		public string GetReport()
		{
			StringBuilder sb = new StringBuilder();

			foreach (var s in Statistics)
			{
				var str = String.Format("{0}: Uses:{1:0.00};{2:0.00};{3:0.00};{4:0.00};{5:0.00}",
					s.Key,
					s.Value.Uses,
					s.Value.Crits,
					s.Value.Crushes,
					s.Value.Testinesses,
					s.Value.Impulses
					);

				var strDmg = s.Value.AvrgDmg == 0 && s.Value.TotalAbilityDamage == 0 ? " no damage" :
					String.Format("Dmg:[{0:0.00};{1:0.00}]~{2:0.00} DmgNImp:[{3:0.00};{4:0.00}]~{5:0.00} TotalDmg:{6:0.00}({7:0.00})",
					s.Value.MinDmg,
					s.Value.MaxDmg,
					s.Value.AvrgDmg,
					s.Value.MinNonImpulseDmg,
					s.Value.MaxNonImpulseDmg,
					s.Value.AvrgNonImpulseDmg,
					s.Value.TotalAbilityDamage,
					s.Value.TotalAbilityNonImpulseDamage);


				sb.AppendLine(str + " " + strDmg);
			}

			return sb.ToString();
		}

		public Dictionary<string, AbilityStatistic> Statistics { get; set; }
		
		int appendCounts = 0;
		/// <summary>
		/// Reperesent number of appended statistics to calculate average (if AppendCounts == 0 - its individual statistic, if AppendCounts == 5 it means 
		/// that current statistic aggregates average values of six individual statistics)
		/// </summary>
		public int AppendCounts 
		{
			get { return appendCounts; } 
		}
	}
}
