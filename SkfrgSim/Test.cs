using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkfrgSimCommon.Model;
using SkfrgSimCommon;
using System.IO;
using SkfrgSimCommon.Classes;
using buffs = SkfrgSimCommon.Model.Buffs;

namespace SkfrgSim
{
	public class Test
	{
        //public void SimpleTest()
        //{
        //    bool ReadFromFile = true;
        //    string fileName = "TestData.txt";
        //    int maxIterationTime = 1200 * 1000; // 20 min.

        //    bool displaylog = false;

        //    IDependencyFactory dependencyFactory = displaylog ? 
        //        (IDependencyFactory)new DefaultDependencyFactory() : 
        //        (IDependencyFactory)new SilentDependencyFactory();

        //    // Load TestParams
        //    List<TestParameters> testParams = new List<TestParameters>();

        //    if (ReadFromFile)
        //    {
        //        using (StreamReader sr = new StreamReader(fileName))
        //        {
        //            while (!sr.EndOfStream)
        //            {
        //                var str = sr.ReadLine();

        //                if (!str.StartsWith("#"))
        //                {
        //                    var parts = str.Split();
        //                    if (parts.Length > 1)
        //                    {
        //                        testParams.Add(new TestParameters()
        //                        {
        //                            IterationCount = Int32.Parse(parts[1]),
        //                            TargetMaxHp = Int32.Parse(parts[2]),
        //                            Might = Int32.Parse(parts[3]),
        //                            Stamina = Int32.Parse(parts[4]),
        //                            Str = Int32.Parse(parts[5]),
        //                            Brave = Int32.Parse(parts[6]),
        //                            Luck = Int32.Parse(parts[7]),
        //                            Spirit = Int32.Parse(parts[8]),
        //                            CritChancePercent = Double.Parse(parts[9]),
        //                            ImpulsePercent = Double.Parse(parts[10]),
        //                            PrecisePercent = Double.Parse(parts[11]),
        //                            TestinessPercent = Double.Parse(parts[12]),
        //                            CrushingChancePercent = Double.Parse(parts[13]),
        //                            SolidityPercent = Double.Parse(parts[14]),
        //                            StrBonus = Double.Parse(parts[15]),
        //                            SpiritBonus = Double.Parse(parts[16]),
        //                            LuckBonus = Double.Parse(parts[17])
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        TestParameters testParam = new TestParameters()
        //        {
        //            IterationCount = 1000,
        //            TargetMaxHp = 5000,
        //            Might = 1058,
        //            Stamina = 1109,
        //            Str = 300,
        //            Brave = 074,
        //            Luck = 265,
        //            Spirit = 225,
        //            CritChancePercent = 21,
        //            ImpulsePercent = 7.2,
        //            PrecisePercent = 0.7,
        //            TestinessPercent = 0,
        //            CrushingChancePercent = 0,
        //            SolidityPercent = 0,
        //            LuckBonus = 0,
        //            StrBonus = 0,
        //            SpiritBonus = 0,
        //        };

        //        testParams.Add(testParam);
        //    }

        //    int paramCnt = 0;
        //    foreach (var testParam in testParams)
        //    {
        //        paramCnt++;
        //        Console.Write(String.Format("Processing parameter {0} of {1}:", paramCnt, testParams.Count));
        //        List<double> dps = new List<double>();

        //        for (int i = 0; i < testParam.IterationCount; i++)
        //        {
        //            List<IActor> actors = new List<IActor>();

        //            var eContext = new EnvironmentContext(null)
        //            {
        //                TargetCurrentHp = testParam.TargetMaxHp,
        //                TargetMaxHp = testParam.TargetMaxHp,
        //            };

        //            var stats = new ActorStats()
        //            {
        //                Might = testParam.Might,
        //                Stamina = testParam.Stamina,
        //                Str = testParam.Str,
        //                Brave = testParam.Brave,
        //                Lucky = testParam.Luck,
        //                Spirit = testParam.Spirit,
        //                CritChancePercent = testParam.CritChancePercent,
        //                ImpulsePercent = testParam.ImpulsePercent,
        //                PrecisePercent = testParam.PrecisePercent,
        //                TestinessPercent = testParam.TestinessPercent,
        //                CrushingChancePercent = testParam.CrushingChancePercent,
        //                SolidityPercent = testParam.SolidityPercent,
        //                StrBonus = testParam.StrBonus,
        //                LuckyBonus = testParam.LuckBonus,
        //                SpiritBonus = testParam.SpiritBonus
        //            };

        //            var calc = new Calculator();

        //            var player = new Paladin(eContext, stats, calc);

        //            actors.Add(player);

        //            bool exit = false;
        //            int time = 0;
        //            int timeDelta = 100;	// ms
        //            while (!exit)
        //            {
        //                if (time > maxIterationTime)
        //                    exit = true;

        //                actors.ForEach(a => a.Tick(time, dependencyFactory));

        //                if (eContext.TargetCurrentHp <= 0)
        //                    break;

        //                time += timeDelta;
        //            }

        //            if (Math.Truncate((double)i * 10 / testParam.IterationCount) != Math.Truncate((double)(i + 1) * 10 / testParam.IterationCount))
        //            {
        //                //Console.WriteLine("Processed: " + Math.Round((double)i * 100 / testParam.IterationCount) + "%");
        //                Console.Write(".");
        //            }

        //            dps.Add(player.TotalDamage * 1000 / time);
        //        } // iterations
        //        Console.WriteLine();

        //        var dpsAverage = dps.Average();
        //        var dpsMin = dps.Min();
        //        var dpsMax = dps.Max();

        //        using (var sw = new StreamWriter("Result.txt", true))
        //        {
        //            string result = String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18:0.00}\t{19:0.00}\t{20:0.00}",
        //                DateTime.Now.ToString("yyyyMMdd_HHmmss"),
        //                testParam.IterationCount,
        //                testParam.TargetMaxHp,
        //                testParam.Might,
        //                testParam.Stamina,
        //                testParam.Str,
        //                testParam.Brave,
        //                testParam.Luck,
        //                testParam.Spirit,
        //                testParam.CritChancePercent,
        //                testParam.ImpulsePercent,
        //                testParam.PrecisePercent,
        //                testParam.TestinessPercent,
        //                testParam.CrushingChancePercent,
        //                testParam.SolidityPercent,
        //                testParam.StrBonus,
        //                testParam.SpiritBonus,
        //                testParam.LuckBonus,
        //                dpsMin,
        //                dpsMax,
        //                dpsAverage
        //            );

        //            sw.WriteLine(result);
        //        }

        //        Console.WriteLine(String.Format("DPS: [{0:0.00}, {1:0.00}] ~ {2:0.00}", dpsMin, dpsMax, dpsAverage));
        //    }
        //    Console.Write("Press enter to exit: ");
        //    Console.ReadLine();
        //}

        public void EventBasedTest()
        {
            bool ReadFromFile = true;
			bool WriteLogToFile = true;
            
			//string fileName = "EvtBasedTest.txt";
            string fileName = "SingleQueryTest.txt";
			//string fileName = "PaladinTest.txt";

            bool displaylog = true;

            IDependencyFactory dependencyFactory = displaylog ?
                (IDependencyFactory)new DefaultDependencyFactory() :
                (IDependencyFactory)new SilentDependencyFactory();

            // Load TestParams
            List<TestParameters> testParams = new List<TestParameters>();

            if (ReadFromFile)
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while (!sr.EndOfStream)
                    {
                        var str = sr.ReadLine();

                        if (!str.StartsWith("#"))
                        {
                            var parts = str.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length > 1)
                            {
								testParams.Add(new TestParameters(parts));
                            }
                        }
                    }
                }
            }
            else
            {
                TestParameters testParam = new TestParameters()
                {
                    IterationCount = 1000,
                    TargetMaxHp = 5000,
                    Might = 1058,
                    Stamina = 1109,
                    Str = 300,
                    Brave = 074,
                    Luck = 265,
                    Spirit = 225,
                    CritChancePercent = 21,
                    ImpulsePercent = 7.2,
                    PrecisePercent = 0.7,
                    TestinessPercent = 0,
                    CrushingChancePercent = 0,
                    SolidityPercent = 0,
                    LuckBonus = 0,
                    StrBonus = 0,
                    SpiritBonus = 0,
                };

                testParams.Add(testParam);
            }

            int paramCnt = 0;

            bool displayDetails = testParams.Count == 1 && testParams[0].IterationCount == 1;
            ILogger logger = displayDetails ?  new DefaultLogger() : null;
			StreamWriter logFile = null;
			TextWriter defaultConsole = null;
			if (WriteLogToFile)
			{
				Console.WriteLine("Changed output to 'Combalog.txt'");
				defaultConsole = Console.Out;
				logFile = new StreamWriter("Combatlog.txt", false, Encoding.UTF8);
				Console.SetOut(logFile);
			}

			// TODO: statistics
			var paramResults = new List<List<List<LogEvent>>>();

            foreach (var testParam in testParams)
            {
				if (testParams.Count > 1)
					Console.Write(String.Format("Processing parameter {0} of {1}:", ++paramCnt, testParams.Count));

                List<double> dps = new List<double>();

				paramResults.Add(new List<List<LogEvent>>());
				
				Statistic parameterStatistic = new Statistic();

                for (int i = 0; i < testParam.IterationCount; i++)
                {
                    var eContext = new EnvironmentContext(logger);
                    var eventProcessor = new EventProcessor(eContext);

                    var stats = GetStats(testParam);

                    var calc = new Calculator();

                    var player = CreatePlayer(eContext, stats, calc);

                    eContext.Actor = player;
                    eContext.Target = new TargetActor() { MaxHp = testParam.TargetMaxHp, CurrentHp = testParam.TargetMaxHp }; 

                    bool exit = false;
                    
                    while (!exit)
                    {
                        if (eContext.Events.Count == 0)
                        {
                            exit = true;
                            break;
                        }

                        eContext.ProcessNextEvent();

                        if (eContext.TargetCurrentHp <= 0)
                            break;
                    }

                    if (Math.Truncate((double)i * 10 / testParam.IterationCount) != Math.Truncate((double)(i + 1) * 10 / testParam.IterationCount))
                    {
                        //Console.WriteLine("Processed: " + Math.Round((double)i * 100 / testParam.IterationCount) + "%");
                        Console.Write(".");
                    }

                    dps.Add(player.TotalDamage * 1000 / eContext.CurrentTime);

					if (displayDetails)
					{
						ProcessCombatlog(player);
					}

					//paramResults.Last().Add(player.CombatlogHistory);

					Statistic localStatistic = ProcessCombatlog(player);

					parameterStatistic.AppendToAverage(localStatistic);

					player.Reset();

                } // iterations
                Console.WriteLine();

                var dpsAverage = dps.Average();
                var dpsMin = dps.Min();
                var dpsMax = dps.Max();

                using (var sw = new StreamWriter("Result.txt", true))
                {
                    string result = String.Format("{0}\t{1}\t{2:0.00}\t{3:0.00}\t{4:0.00}\t{5}",
                        DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                        testParam.GetCSVString(),
                        dpsMin,
                        dpsMax,
                        dpsAverage,
						testParam.Comment
                    );

                    sw.WriteLine(result);
                }

                Console.WriteLine(String.Format("DPS: [{0:0.00}, {1:0.00}] ~ {2:0.00}", dpsMin, dpsMax, dpsAverage));

                // TODO: statistic
				Console.WriteLine(parameterStatistic.GetReport());

            }	// foreach testParams

			if (logFile != null)
			{
				Console.SetOut(defaultConsole);
				logFile.Close();
			}

            Console.Write("Press enter to exit: ");
			paramResults.Clear();
            Console.ReadLine();
        }

        static Actor CreatePlayer(EnvironmentContext context, ActorStats stats, Calculator calc)
        {
			//var player = new Paladin(context, stats, calc);
			//player.Buffs.Add(new ActorBuff(new buffs.Paladin.Inflexibility()));
			//player.Buffs.Add(new ActorBuff(new buffs.Paladin.Fanaticism()));
			//player.Buffs.Add(new ActorBuff(new buffs.Paladin.Detachment()));

			var player = new Archer(context, stats, calc);

            return player;
        }

		static Statistic ProcessCombatlog(Actor actor)
		{
			var log = actor.CombatlogHistory;

			var statistic = new Statistic();

			foreach (var evt in log)
			{
				if (evt.Type == LogEventType.AbilityCast)
					continue;

				if (!statistic.Statistics.ContainsKey(evt.AbilityName))
				{
					statistic.Statistics.Add(evt.AbilityName, new AbilityStatistic() { MinDmg = double.MaxValue, MinNonImpulseDmg = double.MaxValue });
				}
				
				statistic.Statistics[evt.AbilityName].Uses++;
				var det = evt.Details as AbilityDmg;
				if (det != null)
				{
					statistic.Statistics[evt.AbilityName].TotalAbilityDamage += det.Damage;
					statistic.Statistics[evt.AbilityName].TotalAbilityNonImpulseDamage += det.Damage - det.ImpulseDamage;
					statistic.Statistics[evt.AbilityName].MaxDmg = Math.Max(statistic.Statistics[evt.AbilityName].MaxDmg, det.Damage);
					statistic.Statistics[evt.AbilityName].MinDmg = Math.Min(statistic.Statistics[evt.AbilityName].MinDmg, det.Damage);
					statistic.Statistics[evt.AbilityName].AvrgDmg = (statistic.Statistics[evt.AbilityName].AvrgDmg * (statistic.Statistics[evt.AbilityName].Uses - 1) + det.Damage) / statistic.Statistics[evt.AbilityName].Uses;

					statistic.Statistics[evt.AbilityName].MaxNonImpulseDmg = Math.Max(statistic.Statistics[evt.AbilityName].MaxNonImpulseDmg, det.Damage - det.ImpulseDamage);
					statistic.Statistics[evt.AbilityName].MinNonImpulseDmg = Math.Min(statistic.Statistics[evt.AbilityName].MinNonImpulseDmg, det.Damage - det.ImpulseDamage);
					statistic.Statistics[evt.AbilityName].AvrgNonImpulseDmg = (statistic.Statistics[evt.AbilityName].AvrgNonImpulseDmg * (statistic.Statistics[evt.AbilityName].Uses - 1) + det.Damage - det.ImpulseDamage) / statistic.Statistics[evt.AbilityName].Uses;

					if (det.isCritical)
						statistic.Statistics[evt.AbilityName].Crits++;

					if (det.isImpulse)
						statistic.Statistics[evt.AbilityName].Impulses++;

					if (det.isCrushing)
						statistic.Statistics[evt.AbilityName].Crushes++;

					if (det.isTestinessed)
						statistic.Statistics[evt.AbilityName].Testinesses++;

					statistic.Statistics[evt.AbilityName].Damage.Add(det.Damage);
				}
			}

			return statistic;
		}

		static ActorStats GetStats(TestParameters testParam)
		{
			return new ActorStats()
			{
				Might = testParam.Might,
				Stamina = testParam.Stamina,
				Str = testParam.Str,
				Brave = testParam.Brave,
				Lucky = testParam.Luck,
				Spirit = testParam.Spirit,
				CritChancePercent = testParam.CritChancePercent,
				ImpulsePercent = testParam.ImpulsePercent,
				PrecisePercent = testParam.PrecisePercent,
				TestinessPercent = testParam.TestinessPercent,
				CrushingChancePercent = testParam.CrushingChancePercent,
				SolidityPercent = testParam.SolidityPercent,
				StrBonus = testParam.StrBonus,
				LuckyBonus = testParam.LuckBonus,
				SpiritBonus = testParam.SpiritBonus,
				BraveBonus = testParam.BraveBonus
			};
		}
	}

	public class TestParameters
	{
		public TestParameters()
		{ }

		public TestParameters(string[] parts)
		{
			IterationCount = Int32.Parse(parts[1].Trim());
			TargetMaxHp = Int32.Parse(parts[2].Trim());
			Might = Int32.Parse(parts[3].Trim());
			Stamina = Int32.Parse(parts[4].Trim());
			Str = Int32.Parse(parts[5].Trim());
			Brave = Int32.Parse(parts[6].Trim());
			Luck = Int32.Parse(parts[7].Trim());
			Spirit = Int32.Parse(parts[8].Trim());
			CritChancePercent = Double.Parse(parts[9].Trim());
			ImpulsePercent = Double.Parse(parts[10].Trim());
			PrecisePercent = Double.Parse(parts[11].Trim());
			TestinessPercent = Double.Parse(parts[12].Trim());
			CrushingChancePercent = Double.Parse(parts[13].Trim());
			SolidityPercent = Double.Parse(parts[14].Trim());
			StrBonus = Double.Parse(parts[15].Trim());
			SpiritBonus = Double.Parse(parts[16].Trim());
			LuckBonus = Double.Parse(parts[17].Trim());
			BraveBonus = Double.Parse(parts[18].Trim());
			Comment = parts[19];
		}

		public string GetCSVString()
		{
			string result = String.Format("{0}\t{1}\t{2,5}\t{3,5}\t{4,4}\t{5,4}\t{6,4}\t{7,4}\t{8:00.0}\t{9:00.0}\t{10:00.0}\t{11:00.0}\t{12:00.0}\t{13:00.0}\t{14:00.0}\t{15:00.0}\t{16:00.0}\t{17:00.0}",
						IterationCount,
						TargetMaxHp,
						Might,
						Stamina,
						Str,
						Brave,
						Luck,
						Spirit,
						CritChancePercent,
						ImpulsePercent,
						PrecisePercent,
						TestinessPercent,
						CrushingChancePercent,
						SolidityPercent,
						StrBonus,
						SpiritBonus,
						LuckBonus,
						BraveBonus
					);

			return result;
		}

		public double Might { get; set; }
		public double Stamina { get; set; }
		public double Str { get; set; }
		public double Luck { get; set; }
		public double Brave { get; set; }
		public double Spirit { get; set; }
		public double CritChancePercent { get; set; }
		public double CrushingChancePercent { get; set; }
		public double TestinessPercent { get; set; }
		public double ImpulsePercent { get; set; }
		public double PrecisePercent { get; set; }
		public double SolidityPercent { get; set; }
		public double StrBonus { get; set; }
		public double SpiritBonus { get; set; }
		public double LuckBonus { get; set; }
		public double BraveBonus { get; set; }

		public string Comment { get; set; }

		public int TargetMaxHp { get; set; }
		public int IterationCount { get; set; }
	}
}
