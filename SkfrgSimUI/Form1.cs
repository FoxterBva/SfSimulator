using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SkfrgSim;
using SkfrgSimCommon.Model;
using SkfrgSimCommon;
using SkfrgSimCommon.Classes;

namespace SkfrgSimUI
{
	public partial class Form1 : Form
	{
		string fileName = "SingleQueryTest.txt";
		EnvironmentContext eContext;

		public Form1()
		{
			InitializeComponent();
			ReadTestParameters();
		}

		void ReadTestParameters()
		{
			List<TestParameters> testParams = new List<TestParameters>();

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

			var testParam = testParams.First();

			eContext = new EnvironmentContext(new RtbLogger(rtbCombatLog));
			var stats = GetStats(testParam);
			var calc = new Calculator();
			var player = CreatePlayer(eContext, stats, calc);
			eContext.Actor = player;
			eContext.Target = new TargetActor() { MaxHp = testParam.TargetMaxHp, CurrentHp = testParam.TargetMaxHp };

			RefreshRtbEvents();
			RefreshBuffsList();
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

		static Actor CreatePlayer(EnvironmentContext context, ActorStats stats, Calculator calc)
		{
			//var player = new Paladin(context, stats, calc);
			//player.Buffs.Add(new ActorBuff(new buffs.Paladin.Inflexibility()));
			//player.Buffs.Add(new ActorBuff(new buffs.Paladin.Fanaticism()));
			//player.Buffs.Add(new ActorBuff(new buffs.Paladin.Detachment()));

			var player = new Archer(context, stats, calc);

			return player;
		}

		private void btnNextEvent_Click(object sender, EventArgs e)
		{
			var evt = eContext.ProcessNextEvent();

			lblProcessedEvent.Text = "Processed: " + evt.ToString();
			lblCurrentTime.Text = ((double)eContext.CurrentTime / 1000).ToString("0000.00");

			RefreshRtbEvents();

			RefreshBuffsList();
		}

		void RefreshRtbEvents()
		{
			rtbEvents.Clear();
			eContext.Events.Sort();
			eContext.Events.ForEach(evt => rtbEvents.AppendText(evt.ToString() + Environment.NewLine));
		}

		void RefreshBuffsList()
		{
			rtbBuffs.Clear();
			eContext.Actor.Buffs.ForEach(b => 
				{
 					var str = String.Format("[{0}]{1}  {2}", 
						b.Buff.Name, 
						b.Stacks > 1 ? " x " + b.Stacks.ToString() : "",
						b.EndTime > 0 ? ((double)(b.EndTime - eContext.CurrentTime) / 1000).ToString("00.0") + " sec" : ""
					);

					rtbBuffs.AppendText(str + Environment.NewLine);
				});
		}

		void RefreshStatistic()
		{ 

		}
	}

	public class RtbLogger : ILogger
	{
		RichTextBox rtb;

		public RtbLogger(RichTextBox output)
		{ 
			rtb = output;
		}

		public void Log(string message)
		{
			rtb.AppendText(message + Environment.NewLine);
			rtb.ScrollToCaret();
		}
	}
}
