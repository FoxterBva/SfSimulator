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
            //string fileName = "EvtBasedTest.txt";
            string fileName = "TestQuery.txt";
            int maxIterationTime = 1200 * 1000; // 20 min.

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
                            var parts = str.Split();
                            if (parts.Length > 1)
                            {
                                testParams.Add(new TestParameters()
                                {
                                    IterationCount = Int32.Parse(parts[1]),
                                    TargetMaxHp = Int32.Parse(parts[2]),
                                    Might = Int32.Parse(parts[3]),
                                    Stamina = Int32.Parse(parts[4]),
                                    Str = Int32.Parse(parts[5]),
                                    Brave = Int32.Parse(parts[6]),
                                    Luck = Int32.Parse(parts[7]),
                                    Spirit = Int32.Parse(parts[8]),
                                    CritChancePercent = Double.Parse(parts[9]),
                                    ImpulsePercent = Double.Parse(parts[10]),
                                    PrecisePercent = Double.Parse(parts[11]),
                                    TestinessPercent = Double.Parse(parts[12]),
                                    CrushingChancePercent = Double.Parse(parts[13]),
                                    SolidityPercent = Double.Parse(parts[14]),
                                    StrBonus = Double.Parse(parts[15]),
                                    SpiritBonus = Double.Parse(parts[16]),
                                    LuckBonus = Double.Parse(parts[17])
                                });
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
            ILogger logger = displayDetails ? new DefaultLogger() : null;

            foreach (var testParam in testParams)
            {
                paramCnt++;
                Console.Write(String.Format("Processing parameter {0} of {1}:", paramCnt, testParams.Count));
                List<double> dps = new List<double>();

                for (int i = 0; i < testParam.IterationCount; i++)
                {
                    List<IActor> actors = new List<IActor>();

                    var eContext = new EnvironmentContext(logger);
                    var eventProcessor = new EventProcessor(eContext);

                    var stats = new ActorStats()
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
                        SpiritBonus = testParam.SpiritBonus
                    };

                    var calc = new Calculator();

                    //var player = new Archer(eContext, stats, calc);
                    var player = CreatePlayer(eContext, stats, calc);

                    eContext.Actor = player;
                    eContext.Target = new TargetActor() { MaxHp = testParam.TargetMaxHp, CurrentHp = testParam.TargetMaxHp }; 

                    actors.Add(player);

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
                } // iterations
                Console.WriteLine();

                var dpsAverage = dps.Average();
                var dpsMin = dps.Min();
                var dpsMax = dps.Max();

                using (var sw = new StreamWriter("Result.txt", true))
                {
                    string result = String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18:0.00}\t{19:0.00}\t{20:0.00}",
                        DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                        testParam.IterationCount,
                        testParam.TargetMaxHp,
                        testParam.Might,
                        testParam.Stamina,
                        testParam.Str,
                        testParam.Brave,
                        testParam.Luck,
                        testParam.Spirit,
                        testParam.CritChancePercent,
                        testParam.ImpulsePercent,
                        testParam.PrecisePercent,
                        testParam.TestinessPercent,
                        testParam.CrushingChancePercent,
                        testParam.SolidityPercent,
                        testParam.StrBonus,
                        testParam.SpiritBonus,
                        testParam.LuckBonus,
                        dpsMin,
                        dpsMax,
                        dpsAverage
                    );

                    sw.WriteLine(result);
                }

                Console.WriteLine(String.Format("DPS: [{0:0.00}, {1:0.00}] ~ {2:0.00}", dpsMin, dpsMax, dpsAverage));

                //if (displayDetails)
                //{
                //    Console.WriteLine("Used Abilities:");
                //    var ab = 
                //}
            }
            Console.Write("Press enter to exit: ");
            Console.ReadLine();
        }

        static Actor CreatePlayer(EnvironmentContext context, ActorStats stats, Calculator calc)
        {
            var player = new Paladin(context, stats, calc);
            player.Buffs.Add(new ActorBuff(new buffs.Paladin.Inflexibility()));
            //player.Buffs.Add(new ActorBuff(new buffs.Paladin.Fanaticism()));
            player.Buffs.Add(new ActorBuff(new buffs.Paladin.Detachment()));

            return player;
        }
	}

	public class TestParameters
	{
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

		public int TargetMaxHp { get; set; }
		public int IterationCount { get; set; }
	}
}
